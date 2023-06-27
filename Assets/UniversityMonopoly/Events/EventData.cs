using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventData
{
    public string description;
    public List<EventAbilityData> abilities = new List<EventAbilityData>();

    public EventData(string description, EventAbilityData abilityOne, EventAbilityData abilityTwo)
    {
        this.description = description;
        this.abilities.Add(abilityOne);
        this.abilities.Add(abilityTwo);
    }
}
