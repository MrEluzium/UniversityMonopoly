using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventData
{
    public Material material;
    public string description;
    public List<EventAbilityData> abilities = new List<EventAbilityData>();

    public EventData(Material material, string description, EventAbilityData abilityOne, EventAbilityData abilityTwo)
    {
        this.material = material;
        this.description = description;
        this.abilities.Add(abilityOne);
        this.abilities.Add(abilityTwo);
    }
}
