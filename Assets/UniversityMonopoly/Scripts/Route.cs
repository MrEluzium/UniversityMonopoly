using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Route : MonoBehaviour
{
    public List<Transform> childNodeList;

    void Start()
    {
        FillNodes();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        if (childNodeList.Count == 0) { FillNodes(); }

        for (int i = 0; i < childNodeList.Count; i++)
        {
            Vector3 currentPos = childNodeList[i].position;
            currentPos.y += .1f;
            if (i > 0)
            {
                Vector3 prevPos = childNodeList[i - 1].position;
                prevPos.y += .1f;
                Gizmos.DrawLine(prevPos, currentPos);
            }
        }
    }

    void FillNodes()
    {
        childNodeList.Clear();
        Transform[] childObjects = GetComponentsInChildren<Transform>();

        foreach (Transform child in childObjects)
        {
            if (child != this.transform && child.parent.gameObject == gameObject)
            {
                childNodeList.Add(child);
            }
        }
    }



}