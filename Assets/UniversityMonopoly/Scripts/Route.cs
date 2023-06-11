using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    Transform[] childObjects;
    Queue<Stone> stones;
    bool isBusy = false;

    public List<Transform> childNodeList;

    void Start() {
        Stone[] stonesInScene = FindObjectsOfType<Stone>();
        stones = new Queue<Stone>(stonesInScene);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && !isBusy)
        {
            Next();
        }    
    }

    void Next(){
        Stone currentStone = stones.Dequeue();
        stones.Enqueue(currentStone);
        StartCoroutine(currentStone.Move(currentStone, this, 3));
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        FillNodes();

        for (int i = 0; i < childNodeList.Count; i++)
        {
            Vector3 currentPos = childNodeList[i].position;
            if (i > 0)
            {
                Vector3 prevPos = childNodeList[i - 1].position;
                Gizmos.DrawLine(prevPos, currentPos);
            }
        }
    }

    void FillNodes()
    {
        childNodeList.Clear();

        childObjects = GetComponentsInChildren<Transform>();

        foreach (Transform child in childObjects)
        {
            if (child != this.transform)
            {
                childNodeList.Add(child);
            }
        }
    }



}