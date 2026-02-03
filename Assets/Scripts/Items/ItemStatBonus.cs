using UnityEngine;

[System.Serializable]
public class ItemStatBonus
{
    public StatBonus health;
    public StatBonus mana;
    public StatBonus stamina;
    public StatBonus vitality;
    public StatBonus arcane;
    public StatBonus endurance;
    public StatBonus strength;
    public StatBonus dexterity;
    public StatBonus wisdom;
    public StatBonus intellect;
    public StatBonus faith;
    public StatBonus physicalDefense;
    public StatBonus magicalDefense;
    public StatBonus fireDefense;
    public StatBonus lightningDefense;
    public StatBonus divineDefense;
    public StatBonus occultDefense;
    public StatBonus poise;
    public StatBonus poiseRecovery;

    public ItemStatBonus()
    {
        health = new StatBonus(0, 0);
        mana = new StatBonus(0, 0);
        stamina = new StatBonus(0, 0);
        vitality = new StatBonus(0, 0);
        arcane = new StatBonus(0, 0);
        endurance = new StatBonus(0, 0);
        strength = new StatBonus(0, 0);
        dexterity = new StatBonus(0, 0);
        wisdom = new StatBonus(0, 0);
        intellect = new StatBonus(0, 0);
        faith = new StatBonus(0, 0);
        physicalDefense = new StatBonus(0, 0);
        magicalDefense = new StatBonus(0, 0);
        fireDefense = new StatBonus(0, 0);
        lightningDefense = new StatBonus(0, 0);
        divineDefense = new StatBonus(0, 0);
        occultDefense = new StatBonus(0, 0);
        poise = new StatBonus(0, 0);
        poiseRecovery = new StatBonus(0, 0);
    }

    public void AddBonus(ItemStatBonus bonus)
    {
        health.Add(bonus.health);
        mana.Add(bonus.mana);
        stamina.Add(bonus.stamina);
        vitality.Add(bonus.vitality);
        arcane.Add(bonus.arcane);
        endurance.Add(bonus.endurance);
        strength.Add(bonus.strength);
        dexterity.Add(bonus.dexterity);
        wisdom.Add(bonus.wisdom);
        intellect.Add(bonus.intellect);
        faith.Add(bonus.intellect);
        physicalDefense.Add(bonus.physicalDefense);
        magicalDefense.Add(bonus.magicalDefense);
        fireDefense.Add(bonus.fireDefense);
        lightningDefense.Add(bonus.lightningDefense);
        divineDefense.Add(bonus.divineDefense);
        occultDefense.Add(bonus.occultDefense);
        poise.Add(bonus.poise);
        poiseRecovery.Add(bonus.poiseRecovery);
    }   
}
