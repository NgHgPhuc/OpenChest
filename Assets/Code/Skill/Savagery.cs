using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Savagery", menuName = "Skill/Savagery")]
public class Savagery : BaseSkill
{
    float DamageCause;
    float DamageExtra;
    public override void UpgradeSkill_Effect()
    {
        switch(Level)
        {
            case 1:
                DamageCause = 4f;
                DamageExtra = 0;
                break;

            case 2:
                DamageCause = 4.5f;
                DamageExtra = 0.2f;
                break;

            case 3:
                DamageCause = 5f;
                DamageExtra = 0.3f;
                break;

            case 4:
                DamageCause = 5.5f;
                DamageExtra = 0.4f;
                break;

            case 5:
                DamageCause = 6f;
                DamageExtra = 0.5f;
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
                if (targetUnit.GetPercentHP() < 50)
                    currentUnitAttack.DamageCause *= (1 + DamageExtra);

                Defense targetUnitDefense = targetUnit.defense();
                targetUnitDefense.IsCounter = false;

                TurnManager.Instance.AttackOneEnemy(currentUnit, targetUnit, currentUnitAttack, targetUnitDefense);

            }

    }
}
