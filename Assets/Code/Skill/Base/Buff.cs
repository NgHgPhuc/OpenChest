using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Buff
{
    public enum Type
    {
        IncreaseDamage,
        IncreaseDef,
        IncreaseSpeed,
        IncreaseCritDamage,
        IncreaseCritChance,
        IncreaseCounterAttack,
        Healing,
        DecreaseAttack,
        DecreaseDef,
        DecreaseSpeed,
        Leech,
        Silience,
        Stun,
        Broken, //get true damage
        Taunt,
        AttackDamageOnSpeed,
        IncreaseMaxHP

    }
    public Type type;

    public Sprite Icon;
    public int duration;
    public float ValueChange;

    public delegate void Mode();
    public Mode Activation;
    public Mode Deactivation;
    public Mode Onactivation;

    public void SetIcon()
    {
        this.Icon = BuffIconManager.Instance.GetIcon(this.type);
    }
}
