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
    public Dictionary<BaseStats.Passive, float> PassiveList = new Dictionary<BaseStats.Passive, float>();

    public float PowerPoint;

    public float calPowerPoint()
    {
        if (this == null)
            return 0;

        float powerPoint = this.AttackDamage * 3 + this.HealthPoint * 1.5f + this.DefensePoint * 5 + this.Speed * 8;
        foreach (KeyValuePair<BaseStats.Passive, float> kvp in this.PassiveList)
            powerPoint += kvp.Value * 30;

        return powerPoint;
    }

    public BaseStats CloneStats()
    {
        BaseStats baseStats = new BaseStats();

        baseStats.AttackDamage = this.AttackDamage;
        baseStats.HealthPoint = this.HealthPoint;
        baseStats.DefensePoint = this.DefensePoint;
        baseStats.Speed = this.Speed;
        foreach (KeyValuePair<Passive, float> kvp in PassiveList)
            baseStats.PassiveList[kvp.Key] = kvp.Value;
        baseStats.PowerPoint = this.PowerPoint;

        return baseStats;
    }

}