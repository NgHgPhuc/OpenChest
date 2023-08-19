using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Savagery", menuName = "Skill/Savagery")]
public class Savagery : BaseSkill
{
    public override void UpgradeSkill_Effect()
    {
        throw new System.NotImplementedException();
    }

    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        Attack currentUnitAttack = currentUnit.attack();

        currentUnitAttack.DamageCause *= 4f;

        foreach (FightingUnit targetUnit in ChosenUnit)
            if (targetUnit.stateFighting != FightingUnit.StateFighting.Death)
            {
                Defense targetUnitDefense = targetUnit.defense();
                targetUnitDefense.IsCounter = false;

                TurnManager.Instance.AttackOneEnemy(currentUnit, targetUnit, currentUnitAttack, targetUnitDefense);

            }

    }
}
