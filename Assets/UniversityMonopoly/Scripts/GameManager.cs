using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    public Route route;
    public Camera camera;
    public GameState gameState;
    public TextMeshProUGUI diceText;
    public GameObject cameraAnchor;
    public GameObject centerAnchor;

    Queue<Pawn> pawns;
    Pawn currentPawn;
    bool isBusy = false;

    void Start()
    {
        pawns = new Queue<Pawn>(players);
        currentPawn = pawns.Peek();

        diceText.enabled = false;
    }

    void Update()
    {   
        
        switch (gameState)
        {
            case GameState.Menu:
                camera.transform.LookAt(centerAnchor.transform);
                camera.transform.Translate(Vector3.right * Time.deltaTime);
                break;
            case GameState.GameStarted:
                camera.transform.LookAt(cameraAnchor.transform);
                Vector3 newCameraPos = new Vector3(currentPawn.cameraPoint.x, camera.transform.position.y , currentPawn.cameraPoint.z);
                camera.transform.position = newCameraPos;
                break;
        }

        if (Input.GetKeyDown(KeyCode.Space) && gameState == GameState.GameStarted && !currentPawn.isMoving && !isBusy)
        {
            Next();
        }  
    }

    public void StartGame()
    {
        diceText.enabled = true;
        diceText.text = "";
        gameState = GameState.GameStarted;
    }

    void Next(){
        currentPawn = pawns.Dequeue();
        pawns.Enqueue(currentPawn);
        StartCoroutine(RollTheDice());
    }

    private IEnumerator RollTheDice()
     {
        isBusy = true;
        int randomDiceSide = 0;
        int finalSide = 0;
        float delay = 0;
        float totalTime = 0;

        while (totalTime < 2.5f)
        {
            delay += Time.deltaTime;
            randomDiceSide = Random.Range(0, 5);
            diceText.text = (randomDiceSide + 1).ToString();
            totalTime += Time.deltaTime + delay;
            yield return new WaitForSeconds(delay);
        }
        finalSide = randomDiceSide + 1;

        StartCoroutine(currentPawn.Move(currentPawn, route, finalSide));
        isBusy = false;
		yield return null;
    }
}
