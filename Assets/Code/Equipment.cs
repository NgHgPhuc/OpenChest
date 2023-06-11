using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment
{
    public enum Type
    {
        None,
        Weapon,
        Helmet,
        Paudlron,
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
    public enum Quality
    {
        None,
        Common, //White
        UnCommon, //Green
        Rare, //Blue
        Epic, //Violet
        Exotic, //Purple
        Legendary, //Orange
        Mythic //red
    }

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
}
