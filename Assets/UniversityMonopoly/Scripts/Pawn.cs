using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{

    public int routePosition = 1;
    public Vector2 offset;
    public GameObject anchor;
    public Vector3 cameraPoint;
    public bool isMoving;

    void Update() {
        transform.LookAt(anchor.transform);
        cameraPoint = transform.TransformPoint(new Vector3(0, .2f, -2f));
    }

    public IEnumerator Move(Pawn pawn, Route route, int steps)
    {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;

        while (steps > 0)
        {

            pawn.routePosition++;
            // pawn.routePosition %= route.childNodeList.Count;

            Vector3 nextPos = new Vector3();

            // pawn must continue the path after reaching last hex
            if (pawn.routePosition >= route.childNodeList.Count)
            {
                pawn.routePosition -= route.childNodeList.Count;
            }

            nextPos.x = route.childNodeList[pawn.routePosition].position.x + pawn.offset.x;
            nextPos.y = 2.12f;
            nextPos.z = route.childNodeList[pawn.routePosition].position.z + pawn.offset.y;

            // Vector3 nextPos = route.childNodeList[pawn.routePosition].position;
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, cameraPoint);
    }
}