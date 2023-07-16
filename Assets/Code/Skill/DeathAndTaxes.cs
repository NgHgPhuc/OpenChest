using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Death And Taxes", menuName = "Skill/Death And Taxes")]
public class DeathAndTaxes : BaseSkill
{
    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        Attack currentUnitAttack = currentUnit.attack();

        currentUnitAttack.DamageCause *= 2.5f;

        foreach (FightingUnit targetUnit in ChosenUnit)
            if (targetUnit.stateFighting != FightingUnit.StateFighting.Death)
            {
                Defense targetUnitDefense = targetUnit.defense(20);//Armor Petrenation 20%
                targetUnitDefense.IsCounter = false;

                TurnManager.Instance.AttackOneEnemy(currentUnit, targetUnit, currentUnitAttack, targetUnitDefense);

                if (targetUnit.CurrentHP < targetUnit.GetPercentMaxHP(10))
                {
                    targetUnit.BeingAttacked(999999);
                    currentUnit.Heal(currentUnit.GetPercentMaxHP(10));
                }

                if (targetUnit.stateFighting == FightingUnit.StateFighting.Death)
                {
                    continue;
                }

            }

    }
}
