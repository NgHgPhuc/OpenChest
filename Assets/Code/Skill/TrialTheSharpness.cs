using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trial The Sharpness", menuName = "Skill/Trial The Sharpness")]
public class TrialTheSharpness : BaseSkill
{
    float DamageCause;
    int LeechDuration;
    int LeechValue;
    int Penetration;
    public override void UpgradeSkill_Effect()
    {
        switch(Level)
        {
            case 1:
                DamageCause = 0.4f;
                LeechDuration = 1;
                LeechValue = 8;
                Penetration = 0;
                break;

            case 2:
                DamageCause = 0.4f;
                LeechDuration = 1;
                LeechValue = 9;
                Penetration = 10;
                break;

            case 3:
                DamageCause = 0.4f;
                LeechDuration = 2;
                LeechValue = 10;
                Penetration = 20;
                break;

            case 4:
                DamageCause = 0.4f;
                LeechDuration = 2;
                LeechValue = 10;
                Penetration = 35;
                break;

            case 5:
                DamageCause = 0.4f;
                LeechDuration = 2;
                LeechValue = 10;
                Penetration = 50;
                break;

            default:
                break;
        }
    }

    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        Attack currentUnitAttack = currentUnit.attack();

        currentUnitAttack.DamageCause *= DamageCause;

        foreach (FightingUnit targetUnit in ChosenUnit)
            if (targetUnit.stateFighting != FightingUnit.StateFighting.Death)
            {
                Defense targetUnitDefense = targetUnit.defense();
                targetUnitDefense.IsCounter = false;

                TurnManager.Instance.AttackOneEnemy(currentUnit, targetUnit, currentUnitAttack, targetUnitDefense);

                Leech_Debuff(targetUnit, LeechDuration, LeechValue);

            }

    }

    public void Leech_Debuff(FightingUnit targetUnit,int duration,int value)
    {
        Buff Leech = new Buff();

        Leech.type = Buff.Type.Leech;
        Leech.duration = duration;

        Leech.SetIcon();

        Leech.Activation = () =>
        {
            targetUnit.Unit_BeAttacked += EffectLeech;
        };

        Leech.Deactivation = () =>
        {
            targetUnit.Unit_BeAttacked -= EffectLeech;
        };

        Leech.Onactivation = () =>
        {
            targetUnit.OnlyTakenDamage(targetUnit.GetPercentMaxHP(value), Penetration);
        };

        targetUnit.AddBuff(Leech);
    }

    void EffectLeech(FightingUnit currentUnit, FightingUnit targetUnit, Attack HaveNothing, Defense HaveNothing2)
    {
        //call "CalculateDamage" because dont need Defense
        float DamageTaken = currentUnit.GetPercentMaxHP(LeechValue);
        currentUnit.OnlyTakenDamage(DamageTaken, Penetration);
    }
}
