using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Equipment
{
    public enum Type
    {
        None,
        Weapon,
        Helmet,
        Pauldron,
        Necklace,
        Vambrace,
        Gloves,
        Shield,
        Mask,
        Clothes,
        Pants,
        Belt,
        Shoes,
        Ring,

    }
    public Type type;

    public Sprite Icon;

    public enum Quality
    {
        None,
        Common, //White
        Uncommon, //Green
        Rare, //Blue
        Epic, //Violet
        Exotic, //Yellow
        Legendary, //Orange
        Mythic //red
    }
    public Quality quality;

    public int Level;

    public float AttackDamage;
    public float HealthPoint;
    public float DefensePoint;
    public float Speed;

    public enum Passive
    {
        None,
        Stun,
        Dodge,
        LifeSteal,
        CounterAttack,
        CriticalChance,
        CriticalDamage
    }

    public Dictionary<Passive, float> PassiveList = new Dictionary<Passive, float>();

    public float PowerPoint;

    public float calPowerPoint()
    {
        float powerPoint = this.AttackDamage * 3 + this.HealthPoint * 1.5f + this.DefensePoint * 5 + this.Speed * 8;
        foreach (KeyValuePair<Passive, float> kvp in this.PassiveList)
            powerPoint += kvp.Value * 30;

        return powerPoint;
    }

    public Equipment Clone()
    {
        Equipment e = new Equipment();

        e.type = this.type;
        e.Icon = this.Icon;
        e.quality = this.quality;
        e.Level = this.Level;
        e.AttackDamage = this.AttackDamage;
        e.HealthPoint = this.HealthPoint;
        e.DefensePoint = this.DefensePoint;
        e.Speed = this.Speed;
        foreach (KeyValuePair<Passive, float> kvp in PassiveList)
            e.PassiveList[kvp.Key] = kvp.Value;
        e.PowerPoint = this.PowerPoint;

        return e;
    }
}
