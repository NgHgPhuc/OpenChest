using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//use in fight unit - return a skill
public abstract class BaseSkill : ScriptableObject
{
    public string Name;
    public int Cooldown;
    public Sprite Icon;
    public string Description;

    public enum Range
    {
        OnMySelf,
        //OnAllEnemy,
        OnEnemy,
        OnEnemyTeam,
        OnAlly,
        OnAllyTeam,
    }
    public Range range;

    public float DamagePercent;
    public float DamagePlus;

    public enum Type
    {
        Passive,
        Active
    }
    public Type type;


    public abstract void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit);
}
