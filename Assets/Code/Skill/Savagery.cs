using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Savagery", menuName = "Skill/Savagery")]
public class Savagery : BaseSkill
{
    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        Attack currentUnitAttack = currentUnit.attack();

        currentUnitAttack.DamageCause *= 1.5f;

        foreach (FightingUnit targetUnit in ChosenUnit) if (targetUnit.stateFighting != FightingUnit.StateFighting.Death)
            {
                Defense targetUnitDefense = targetUnit.defense();
                float targetGetDamage = currentUnitAttack.DamageCause * targetUnitDefense.TakenDmgPercent;

                if (targetUnitDefense.IsDogde)
                {
                    targetUnit.DogdeUI();
                    continue;
                }

                targetUnit.BeingAttacked(targetGetDamage);
                currentUnit.LifeSteal(targetGetDamage);

                if (targetUnit.stateFighting == FightingUnit.StateFighting.Death)
                {
                    continue;
                }

                if (currentUnitAttack.IsStun)
                {
                    targetUnit.StunUI();
                    continue;
                }

                if (targetUnitDefense.IsCounter)
                {
                    Attack targetUnitAttack = targetUnit.attack();
                    Defense currentUnitDefense = currentUnit.defense();
                    float tarGetDmg = currentUnitAttack.DamageCause * targetUnitDefense.TakenDmgPercent;

                    if (targetUnitDefense.IsDogde)
                    {
                        targetUnit.DogdeUI();
                        continue;
                    }

                    currentUnit.BeingAttacked(tarGetDmg);

                    if (targetUnit.stateFighting == FightingUnit.StateFighting.Death)
                    {
                        continue;
                    }
                }

            }

    }
}
