using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using UnityEngine;
using TMPro;

using Random = System.Random;

public enum EventType
{
    Gain,
    Debuff,
    Guile,
    Rest,
    Class,
    Exam
}

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Menu,
        GameStarted
    }

    public Pawn[] players;
    public GameState gameState;
    public EventManager eventManager;
    public HUD hud;
    public Route route;
    public Camera camera;
    public GameObject cameraAnchor;
    public GameObject centerAnchor;

    Queue<Pawn> pawns;
    Pawn currentPawn;
    EventData currentEventData;
    bool isCameraMoving = false;
    bool isDiceRolling = false;
    bool isEventActionsPending = false;

    static Random random = new System.Random();

    void OnEnable() 
    {
        Pawn.OnMovementDone += OnMovementDone;
    }

    void Start()
    {
        pawns = new Queue<Pawn>(players);
        currentPawn = pawns.Peek();
    }

    void Update()
    {   
        
        switch (gameState)
        {
            case GameState.Menu:
                camera.transform.Translate(Vector3.right * Time.deltaTime);
                break;
            case GameState.GameStarted:
                if (currentPawn.isMoving)
                {
                    Vector3 newCameraPos = new Vector3(currentPawn.cameraPoint.x, camera.transform.position.y , currentPawn.cameraPoint.z);
                    camera.transform.position = newCameraPos;
                }
                break;
        }
        camera.transform.LookAt(cameraAnchor.transform);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gameState == GameState.GameStarted && !currentPawn.isMoving && !isDiceRolling && !isCameraMoving && !isEventActionsPending)
            {
                Next();
            }
        }  

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void StartGame()
    {
        gameState = GameState.GameStarted;

        Animation cameraAnchorAnim = cameraAnchor.GetComponent<Animation>();
        cameraAnchorAnim.Play();

        hud.SetMainText("Нажмите пробел, чтобы сделать ход");
        hud.ShowPlayersPanel();

        Vector3 newCameraPos = new Vector3(currentPawn.cameraPoint.x, camera.transform.position.y, currentPawn.cameraPoint.z);
        StartCoroutine(MoveCamera(newCameraPos));
    }

    void Next()
    {
        currentPawn = pawns.Dequeue();
        pawns.Enqueue(currentPawn);
        if (currentPawn.turnsToPass != 0) {
            currentPawn.turnsToPass -= 1;
            Next();
        } else {
            Vector3 newCameraPos = new Vector3(currentPawn.cameraPoint.x, camera.transform.position.y, currentPawn.cameraPoint.z);
            StartCoroutine(MoveCamera(newCameraPos));
            StartCoroutine(RollTheDice());
        }
    }

    void OnMovementDone(Pawn pawn)
    {
        currentPawn = pawn;
        if (!pawn.currentHex.isOpen)
        {
            pawn.animator.Play("PawnJumpOnSpot");
            pawn.currentHex.FlipToOpen();
        }

        switch (pawn.currentHex.eventType)
        {
            case EventType.Rest:
                hud.SetMainText("Отдых (+мана)");
                pawn.incrementMana(1);
                break;
            case EventType.Class:
                hud.SetMainText("Пара (+знания)");
                pawn.incrementKnowledge(1);
                break;
            case EventType.Exam:
                int examKnowledgeRequirements = 10;
                if (pawn.splitExamRequirements) { examKnowledgeRequirements = 5; }

                if (pawn.knowledge >= examKnowledgeRequirements)
                {
                    pawn.knowledge -= examKnowledgeRequirements;
                    pawn.passedExams += 1;
                    if (pawn.passedExams == 3)
                    {
                        hud.SetMainText("Экзамен! Ты сдал свой " + pawn.passedExams.ToString() + " экзамен. Это победа!");
                        gameState = GameState.Menu;
                    }
                    else 
                    {
                        hud.SetMainText("Экзамен! Ты сдал свой " + pawn.passedExams.ToString() + " экзамен. Осталось еще " + (3 - pawn.passedExams).ToString());
                    }
                }
                else
                {
                    hud.SetMainText("Экзамен! Тебе не хватает " + (examKnowledgeRequirements - pawn.knowledge).ToString() + " очков знаний");
                }
                break;
            default:
                StartEventOfType(pawn.currentHex.eventType);
                break;
            
        }
    }

    void StartEventOfType(EventType type)
    {
        List<EventData> eventsList = new List<EventData>();

        switch (type)
        {
            case EventType.Gain:
                eventsList = eventManager.gainEvents;
                break;
            case EventType.Debuff:
                eventsList = eventManager.debuffEvents;
                break;
            case EventType.Guile:
                eventsList = eventManager.guileEvents;
                break;
        }

        if(eventsList.Count > 0)
        {
            isEventActionsPending = true;
            EventData eventData = eventsList[random.Next(eventsList.Count)];
            currentEventData = eventData;

            if (currentPawn.mana + eventData.abilities[0].manaV < 0) 
            { 
                hud.DisableFirstButton(); 
            } else { 
                hud.EnableFirstButton(); 
            }
            
            if (currentPawn.mana + eventData.abilities[1].manaV < 0) 
            { 
                hud.DisableSecondButton();
            } else { 
                hud.EnableSecondButton(); 
            }

            hud.SetMainText(eventData.description);
            hud.SetFirstButtonText(eventData.abilities[0].text);
            hud.SetSecondButtonText(eventData.abilities[1].text);
            hud.ShowButtons();
        }
    }

    public void OnActionButtonAnswer(int buttonIndex)
    {
        EventAbilityData eventAbility = currentEventData.abilities[buttonIndex];

        if (eventAbility.diceRerollAmount == 1)
        {
            StartCoroutine(RollTheDice());
        }
        currentPawn.ApplyEventAbility(eventAbility);
        
        hud.DisableFirstButton();
        hud.DisableSecondButton();
        hud.SetMainText("");
        hud.HideButtons();
        isEventActionsPending = false;
    }

    private IEnumerator RollTheDice()
    {
        isDiceRolling = true;

        hud.SetMainText("");
        int randomDiceSide = 0;
        int finalSide = 0;
        float delay = 0;
        double totalTime = 0;

        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();

        while (totalTime < 2.5f)
        {
            totalTime = stopWatch.Elapsed.TotalSeconds;
            delay += .005f;
            randomDiceSide = random.Next(6) + 1;
            hud.SetDiceText(randomDiceSide.ToString());
            yield return new WaitForSeconds(delay);
        }

        int finalSteps = randomDiceSide + currentPawn.diceRollBuff;
        hud.SetDiceText(finalSteps.ToString());

        if (currentPawn.diceRollLenth > 1)
        {
            currentPawn.diceRollLenth -= 1;
        }
        else if (currentPawn.diceRollLenth == 1)
        {
            currentPawn.diceRollLenth = 0;
            currentPawn.diceRollBuff = 0;
        }

        StartCoroutine(currentPawn.Move(currentPawn, route, finalSteps));
        isDiceRolling = false;
		yield return null;
    }

    private IEnumerator MoveCamera(Vector3 endPosition)
    {
        isCameraMoving = true;
        Vector3 startPosition = camera.transform.position;
        double elipsedTime = 0;
        float alpha = 0;

        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();

        while (alpha < 1)
        {
            elipsedTime = stopWatch.Elapsed.TotalSeconds;
            alpha = (float)elipsedTime / 2.0f;
            camera.transform.position = Vector3.Lerp(startPosition, endPosition, Mathf.SmoothStep(0, 1, alpha));
            camera.transform.LookAt(cameraAnchor.transform);
            yield return null;
        }
        stopWatch.Stop();

        isCameraMoving = false;
    }
}
