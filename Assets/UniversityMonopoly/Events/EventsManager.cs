using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public Material gainMaterial;
    public Material debuffMaterial;
    public Material guileMaterial;
    public Material restMaterial;
    public Material classMaterial;
    public Material examMaterial;

    public List<EventData> events = new List<EventData>();

    void Start()
    {
        EventAbilityData GainOneOne = new EventAbilityData();
        GainOneOne.text = "Использовать момент (можешь кинуть кубик 2 раза, -мана)";
        GainOneOne.manaV = -1;

        EventAbilityData GainOneTwo = new EventAbilityData();
        GainOneTwo.text = "Забить";

        events.Add(new EventData(
            gainMaterial, 
            "Наступил вечер, у тебя появился прилив мотивации", 
            GainOneOne, 
            GainOneTwo)
        );

        EventAbilityData GainTowOne = new EventAbilityData();
        GainTowOne.text = "Пойти (+2 к ходу на 3 хода, -мана)";
        GainTowOne.diceRollBuff = 2;
        GainTowOne.diceRollLenth = 3;
        GainTowOne.manaV = -1;

        EventAbilityData GainTowTwo = new EventAbilityData();
        GainTowTwo.text = "Забить";

        events.Add(new EventData(
            gainMaterial, 
            "Вы получили возможность пойти на марафон", 
            GainOneOne, 
            GainOneTwo)
        );
    }
}
