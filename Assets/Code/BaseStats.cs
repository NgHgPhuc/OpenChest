using System.Collections;
using System.Collections.Generic;
using static Equipment;
using UnityEngine;

[System.Serializable]
public class BaseStats
{
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

    public float PowerPoint()
    {
        if (this == null)
            return 0;

        float powerPoint = this.AttackDamage * 3 + this.HealthPoint * 1.5f + this.DefensePoint * 5 + this.Speed * 8;
        foreach (KeyValuePair<Passive, float> kvp in this.PassiveList)
            powerPoint += kvp.Value * 30;

        return powerPoint;
    }

}