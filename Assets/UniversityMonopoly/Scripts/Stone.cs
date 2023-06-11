using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{

    public int routePosition;
    bool isMoving;

    public IEnumerator Move(Stone stone, Route route, int steps)
    {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;

        while (steps > 0)
        {

            stone.routePosition++;
            stone.routePosition %= route.childNodeList.Count;

            Vector3 nextPos = route.childNodeList[stone.routePosition].position;
            while (MoveToNextNode(nextPos)) { yield return null; }

            yield return new WaitForSeconds(0.1f);
            steps--;

        }

        isMoving = false;
    }
    
    bool MoveToNextNode(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 2f * Time.deltaTime));
    }
}