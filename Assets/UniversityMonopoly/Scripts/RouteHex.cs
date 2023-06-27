using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    Gain,
    Debuff,
    Guile,
    Rest,
    Class,
    Exam
}

public class RouteHex : MonoBehaviour
{
    public EventManager eventManager;
    public bool isOpen = false;
    public EventType eventType;

    Animator animator;
    MeshRenderer meshRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        meshRenderer = GetComponent<MeshRenderer>();
        eventManager = GameObject.Find("Events Control").GetComponent<EventManager>();
        
        switch (eventType)
        {
            case EventType.Gain:
                setSideMaterial(eventManager.gainMaterial);
                break;
            case EventType.Debuff:
                setSideMaterial(eventManager.debuffMaterial);
                break;
            case EventType.Guile:
                setSideMaterial(eventManager.guileMaterial);
                break;
            case EventType.Rest:
                setSideMaterial(eventManager.restMaterial);
                break;
            case EventType.Class:
                setSideMaterial(eventManager.classMaterial);
                break;
            case EventType.Exam:
                setSideMaterial(eventManager.examMaterial);
                break;
            default:
                setSideMaterial(eventManager.restMaterial);
                break;
        }
    }

    public void FlipToOpen()
    {
        if (!isOpen)
        {
            isOpen = true;
            animator.Play("HexFlipOpen");
        }
    }

    public void setSideMaterial(Material material)
    {
        Material[] materials = meshRenderer.materials;
        materials[1] = material;
        meshRenderer.SetMaterials(new List<Material>(materials));
    }
}
