using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Equipment;

[System.Serializable]
public class Character
{
    public int Level;

    public Sprite Icon;

    public float AttackDamage;
    public float HealthPoint;
    public float DefensePoint;
    public float Speed;

    public Dictionary<Equipment.Passive, float> PassiveList = new Dictionary<Equipment.Passive, float>();

    public float PowerPoint;

    public float calPowerPoint()
    {
        float powerPoint = this.AttackDamage * 3 + this.HealthPoint * 1.5f + this.DefensePoint * 5 + this.Speed * 8;
        foreach (KeyValuePair<Equipment.Passive, float> kvp in this.PassiveList)
            powerPoint += kvp.Value * 30;

        return powerPoint;
    }

    public Character Clone()
    {
        Character character = new Character();

        character.Icon = this.Icon;
        character.AttackDamage = this.AttackDamage;
        character.HealthPoint = this.HealthPoint;
        character.DefensePoint = this.DefensePoint;
        character.Speed = this.Speed;
        foreach (KeyValuePair<Passive, float> kvp in PassiveList)
            character.PassiveList[kvp.Key] = kvp.Value;
        character.PowerPoint = this.PowerPoint;

        return character;
    }

}
