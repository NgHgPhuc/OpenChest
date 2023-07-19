using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mocking Blade", menuName = "Skill/Mocking Blade")]
public class MockingBlade : BaseSkill
{
    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        Attack currentUnitAttack = currentUnit.attack();

        currentUnitAttack.DamageCause *= 1f;

        foreach (FightingUnit targetUnit in ChosenUnit)
            if (targetUnit.stateFighting != FightingUnit.StateFighting.Death)
            {
                Defense targetUnitDefense = targetUnit.defense();
                targetUnitDefense.IsCounter = false;

                TurnManager.Instance.AttackOneEnemy(currentUnit, targetUnit, currentUnitAttack, targetUnitDefense);

                float SlowRate = UnityEngine.Random.Range(0f, 100f);
                if (SlowRate < 70)
                    targetUnit.AddBuff(DecreaseDMG_Buff(targetUnit, 0.2f));

            }

    }

    public Buff DecreaseDMG_Buff(FightingUnit targetUnit, float percent)
    {
        Buff DecreaseAttack = new Buff();

        DecreaseAttack.type = Buff.Type.IncreaseDamage;
        DecreaseAttack.duration = 2;

        DecreaseAttack.SetIcon();
        DecreaseAttack.ValueChange = targetUnit.basicStatsCharacter.AttackDamage * percent;

        DecreaseAttack.Activation = () =>
        {
            targetUnit.CharacterClone.AttackDamage += DecreaseAttack.ValueChange;
        };

        DecreaseAttack.Deactivation = () =>
        {
            targetUnit.CharacterClone.AttackDamage -= DecreaseAttack.ValueChange;
        };

        DecreaseAttack.Onactivation = () =>
        {
        };

        return DecreaseAttack;
    }
}
