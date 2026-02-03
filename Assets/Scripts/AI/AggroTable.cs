using System;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class AggroTable
{
    public List<AggroEntry> aggroList = new();

    public void AddAggro(Character source, float amount)
    {
        AggroEntry entry = aggroList.Find(e => e.character == source);
        if (entry != null)
            entry.aggroValue += amount;
        else
            aggroList.Add(new AggroEntry { character = source, aggroValue = amount });
    }

    public Character GetHighestAggroTarget(Func<Character, bool> validTarget = null)
    {
        var validEntries = aggroList
            .Where(e => e.character != null && (validTarget == null || validTarget(e.character)))
            .OrderByDescending(e => e.aggroValue)
            .ToList();

        return validEntries.FirstOrDefault()?.character;
    }

    public AggroEntry GetHighestAggro()
    {
        CleanAggroList();
        float amount = 0;
        AggroEntry aggroEntry = null;

        foreach (var entry in aggroList)
        {
            if (entry.aggroValue > amount)
            {
                amount = entry.aggroValue;
                aggroEntry = entry;
            }
        }

        return aggroEntry;
    }

    public void CleanAggroList()
    {
        for(int i = 0; i < aggroList.Count; i++)
        {
            if (aggroList[i].character == null)
            {
                aggroList.RemoveAt(i);
            }
        }
    }

    public void DecayAggro(float deltaTime, float decayRate)
    {
        for (int i = aggroList.Count - 1; i >= 0; i--)
        {
            aggroList[i].aggroValue -= decayRate * deltaTime;
            if (aggroList[i].aggroValue <= 0)
                aggroList.RemoveAt(i);
        }
    }
}
