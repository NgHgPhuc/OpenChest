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
}
