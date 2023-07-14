using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mocking Blade", menuName = "Skill/Mocking Blade")]
public class MockingBlade : BaseSkill
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
