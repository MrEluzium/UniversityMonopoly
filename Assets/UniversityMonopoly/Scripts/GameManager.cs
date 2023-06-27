using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using UnityEngine;
using TMPro;

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
    bool isCameraMoving = false;
    bool isDiceRolling = false;

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
            if (gameState == GameState.GameStarted && !currentPawn.isMoving && !isDiceRolling && !isCameraMoving)
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

        hud.ShowPlayersPanel();

        Vector3 newCameraPos = new Vector3(currentPawn.cameraPoint.x, camera.transform.position.y, currentPawn.cameraPoint.z);
        StartCoroutine(MoveCamera(newCameraPos));
    }

    void Next()
    {
        currentPawn = pawns.Dequeue();
        pawns.Enqueue(currentPawn);
        Vector3 newCameraPos = new Vector3(currentPawn.cameraPoint.x, camera.transform.position.y, currentPawn.cameraPoint.z);
        StartCoroutine(MoveCamera(newCameraPos));
        StartCoroutine(RollTheDice());
    }

    void OnMovementDone(Pawn pawn)
    {
        if (!pawn.currentHex.isOpen)
        {
            pawn.animator.Play("PawnJumpOnSpot");
            pawn.currentHex.FlipToOpen();
        }

        
    }

    private IEnumerator RollTheDice()
    {
        isDiceRolling = true;
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
            randomDiceSide = Random.Range(0, 5);
            hud.SetDiceText((randomDiceSide + 1).ToString());
            yield return new WaitForSeconds(delay);
        }
        finalSide = randomDiceSide + 1;

        StartCoroutine(currentPawn.Move(currentPawn, route, finalSide));
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

        if(hud.mainText.text == "")
        {
            hud.SetMainText("Нажмите пробел, чтобы сделать ход");
        }

        isCameraMoving = false;
    }
}
