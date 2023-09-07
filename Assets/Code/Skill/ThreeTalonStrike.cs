using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Three Talon Strike", menuName = "Skill/Three Talon Strike")]
public class ThreeTalonStrike : BaseSkill
{
    float DamageDealt_1Waves;
    int Waves;
    public override void UpgradeSkill_Effect()
    {
        switch (Level)
        {
            case 1:
                DamageDealt_1Waves = 0.3f;
                Waves = 4;
                break;

            case 2:
                DamageDealt_1Waves = 0.35f;
                Waves = 4;
                break;

            case 3:
                DamageDealt_1Waves = 0.4f;
                Waves = 5;
                break;

            case 4:
                DamageDealt_1Waves = 0.45f;
                Waves = 5;
                break;

            case 5:
                DamageDealt_1Waves = 0.5f;
                Waves = 6;
                break;
            default:
                break;
        }
    }

    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        IEnumerator c = AllAttackWaves(currentUnit, ChosenUnit);
        TurnManager.Instance.SkillTimes(c);
    }

    public IEnumerator AllAttackWaves(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        for (int i = 0; i < Waves; i++)
        {
            AttackWaves(currentUnit, ChosenUnit);
            yield return new WaitForSeconds(0.2f);
        }
        yield break;
    }

    void AttackWaves(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        Attack currentUnitAttack = currentUnit.attack();

        currentUnitAttack.DamageCause *= DamageDealt_1Waves;//deal 4 times

        foreach (FightingUnit targetUnit in ChosenUnit)
            if (targetUnit.stateFighting != FightingUnit.StateFighting.Death)
            {
                Defense targetUnitDefense = targetUnit.defense();
                targetUnitDefense.IsCounter = false;

                TurnManager.Instance.AttackOneEnemy(currentUnit, targetUnit, currentUnitAttack, targetUnitDefense);

            }
    }
}
