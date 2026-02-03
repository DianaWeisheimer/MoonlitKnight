using UnityEngine;

[System.Serializable]
public class LevelStatBonus
{
    public StatBonus vitality;
    public StatBonus arcane;
    public StatBonus endurance;
    public StatBonus strength;
    public StatBonus dexterity;
    public StatBonus wisdom;
    public StatBonus intellect;
    public StatBonus faith;

    public LevelStatBonus()
    {
        vitality = new StatBonus(0, 0);
        arcane = new StatBonus(0, 0);
        endurance = new StatBonus(0, 0);
        strength = new StatBonus(0, 0);
        dexterity = new StatBonus(0, 0);
        wisdom = new StatBonus(0, 0);
        intellect = new StatBonus(0, 0);
        faith = new StatBonus(0, 0);
    }

    public void AddBonus(LevelStatBonus bonus)
    {
        vitality.Add(bonus.vitality);
        arcane.Add(bonus.arcane);
        endurance.Add(bonus.endurance);
        strength.Add(bonus.strength);
        dexterity.Add(bonus.dexterity);
        wisdom.Add(bonus.wisdom);
        intellect.Add(bonus.intellect);
        faith.Add(bonus.intellect);
    }   
}
