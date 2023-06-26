using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Equipment : BaseStats
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
