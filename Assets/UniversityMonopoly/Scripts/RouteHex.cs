using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteHex : MonoBehaviour
{
    public bool isOpen = false;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    public void FlipToOpen()
    {
        if (!isOpen)
        {
            isOpen = true;
            animator.Play("HexFlipOpen");
        }
    }
}
