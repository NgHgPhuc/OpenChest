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

                targetUnit.AddBuff(Leech_Debuff(targetUnit));

            }

    }

    public Buff Leech_Debuff(FightingUnit targetUnit)
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

        return Leech;
    }

    void EffectLeech(FightingUnit currentUnit, FightingUnit targetUnit, Attack HaveNothing, Defense HaveNothing2)
    {
        float DamageTaken = currentUnit.GetPercentMaxHP(10) * currentUnit.CalculateDamage(0);
        currentUnit.BeingAttacked(DamageTaken);
    }
}
