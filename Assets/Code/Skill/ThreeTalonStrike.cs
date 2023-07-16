using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Three Talon Strike", menuName = "Skill/Three Talon Strike")]
public class ThreeTalonStrike : BaseSkill
{
    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        IEnumerator c = AllAttackWaves(currentUnit, ChosenUnit);
        TurnManager.Instance.SkillTimes(c);
    }

    public IEnumerator AllAttackWaves(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        for (int i = 0; i < 4; i++)
        {
            AttackWaves(currentUnit, ChosenUnit);
            yield return new WaitForSeconds(0.2f);
        }
        yield break;
    }

    void AttackWaves(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        Attack currentUnitAttack = currentUnit.attack();

        currentUnitAttack.DamageCause *= 0.4f;

        foreach (FightingUnit targetUnit in ChosenUnit)
            if (targetUnit.stateFighting != FightingUnit.StateFighting.Death)
            {
                Defense targetUnitDefense = targetUnit.defense();
                targetUnitDefense.IsCounter = false;

                TurnManager.Instance.AttackOneEnemy(currentUnit, targetUnit, currentUnitAttack, targetUnitDefense);

            }
    }
}
