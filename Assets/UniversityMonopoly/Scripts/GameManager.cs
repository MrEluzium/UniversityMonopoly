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

        Animation cameraAnchorAnim = cameraAnchor.GetComponent<Animation>();
        cameraAnchorAnim.Play();

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

    private IEnumerator RollTheDice()
    {
        isBusy = true;
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
            diceText.text = (randomDiceSide + 1).ToString();
            yield return new WaitForSeconds(delay);
        }
        finalSide = randomDiceSide + 1;

        StartCoroutine(currentPawn.Move(currentPawn, route, finalSide));
        isBusy = false;
		yield return null;
    }

    private IEnumerator MoveCamera(Vector3 endPosition)
    {
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
    }
}
