using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trial The Sharpness", menuName = "Skill/Trial The Sharpness")]
public class TrialTheSharpness : BaseSkill
{

    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        Attack currentUnitAttack = currentUnit.attack();

        currentUnitAttack.DamageCause *= 0.6f;

        foreach (FightingUnit targetUnit in ChosenUnit)
            if (targetUnit.stateFighting != FightingUnit.StateFighting.Death)
            {
                Defense targetUnitDefense = targetUnit.defense();
                targetUnitDefense.IsCounter = false;

                TurnManager.Instance.AttackOneEnemy(currentUnit, targetUnit, currentUnitAttack, targetUnitDefense);

                Leech_Debuff(targetUnit);

            }

    }

    public void Leech_Debuff(FightingUnit targetUnit)
    {
        Buff Leech = new Buff();

        Leech.type = Buff.Type.Leech;
        Leech.duration = 2;

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
            targetUnit.BeingAttacked(targetUnit.GetPercentMaxHP(8));
        };

        targetUnit.AddBuff(Leech);
    }

    void EffectLeech(FightingUnit currentUnit, FightingUnit targetUnit, Attack HaveNothing, Defense HaveNothing2)
    {
        //call "CalculateDamage" because dont need Defense
        float DamageTaken = currentUnit.GetPercentMaxHP(8) * currentUnit.CalculateDamage(0);
        currentUnit.BeingAttacked(DamageTaken);
    }

    public override void UpgradeSkill_Effect()
    {
        throw new System.NotImplementedException();
    }
}
