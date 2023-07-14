using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unbreakable", menuName = "Skill/Unbreakable")]
public class Unbreakable : BaseSkill
{
    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        Attack currentUnitAttack = currentUnit.attack();

        currentUnitAttack.DamageCause *= 2.5f;

        foreach (FightingUnit targetUnit in ChosenUnit)
            if (targetUnit.stateFighting != FightingUnit.StateFighting.Death)
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

                //if (currentUnitAttack.IsStun)
                if (true)
                {
                    Debug.Log("Stun");
                    targetUnit.StunUI();
                    continue;
                }

            }

    }
}
