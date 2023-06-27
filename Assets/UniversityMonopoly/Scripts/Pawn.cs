using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pawn : MonoBehaviour
{
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI knowledgeText;
    public int routePosition = 1;
    public bool isMoving;
    public Vector2 offset;
    public GameObject anchor;
    public Vector3 cameraPoint;
    public RouteHex currentHex;

    public int mana;
    public float knowledge;
    public int passedExams;
    public int diceRollBuff;
    public int diceRollLenth;
    public int turnsToPass;
    public bool splitExamRequirements = false;
    // public float knowledgeMultiplaier = 1f;

    public Animator animator;

    public delegate void MovementDone(Pawn pawn);
    public static event MovementDone OnMovementDone;

    void Start()
    {
        animator = GetComponentsInChildren<Animator>()[0];    
    }

    void Update()
    {
        transform.LookAt(anchor.transform);
        cameraPoint = transform.TransformPoint(new Vector3(0, .2f, -2f));
    }

    public void incrementMana(int addMana)
    {
        mana += addMana;
        if (mana < 0) { mana = 0; }
        SetManaText(mana);
    }

    public void incrementKnowledge(float addKnowledge)
    {
        knowledge += addKnowledge;
        if (knowledge < 0f) { knowledge = 0f; }
        SetKnowledgeText(knowledge);
    }

    public void ApplyEventAbility(EventAbilityData eventAbility)
    {
        incrementMana(eventAbility.manaV);

        if (eventAbility.diceRollBuff != 0) { diceRollBuff = eventAbility.diceRollBuff; }
        
        if (eventAbility.diceRollLenth != 0) { diceRollLenth = eventAbility.diceRollLenth; }
        
        if (eventAbility.turnsToPass != 0) { turnsToPass = eventAbility.turnsToPass; }
        
        if (eventAbility.examRequirementsMultiplier != 0f) { splitExamRequirements = true; }
        
        // if (eventAbility.knowledgeMultiplaier != 0f) { knowledgeMultiplaier = eventAbility.knowledgeMultiplaier; }
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
            currentHex = route.childNodeList[pawn.routePosition].gameObject.GetComponentsInChildren<RouteHex>()[0];
            steps--;
            if (currentHex.eventType == EventType.Exam) { steps = 0; }
        }

        isMoving = false;
        OnMovementDone(this);
    }
    
    bool MoveToNextNode(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 2f * Time.deltaTime));
    }

    void SetManaText(int newManaAmount)
    {
        manaText.text = "Мана: " + newManaAmount.ToString();
    }

    void SetKnowledgeText(float newKnowledgeAmount)
    {
        knowledgeText.text = "Знания: " + newKnowledgeAmount.ToString();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, cameraPoint);
    }
}