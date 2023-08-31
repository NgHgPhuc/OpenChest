using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mocking Blade", menuName = "Skill/Mocking Blade")]
public class MockingBlade : BaseSkill
{
    float DamageCause;
    int DecreaseAttackRate;
    float DecreaseAttackValue;

    public override void UpgradeSkill_Effect()
    {
        switch(Level)
        {
            case 1:
                DamageCause = 1f;
                DecreaseAttackRate = 70;
                DecreaseAttackValue = 0.2f;
                break;

            case 2:
                DamageCause = 1.05f;
                DecreaseAttackRate = 100;
                DecreaseAttackValue = 0.2f;
                break;

            case 3:
                DamageCause = 1.1f;
                DecreaseAttackRate = 100;
                DecreaseAttackValue = 0.3f;
                break;

            case 4:
                DamageCause = 1.15f;
                DecreaseAttackRate = 100;
                DecreaseAttackValue = 0.35f;
                break;

            case 5:
                DamageCause = 1.2f;
                DecreaseAttackRate = 70;
                DecreaseAttackValue = 0.4f;
                break;

        }
    }

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

                DecreaseDMG_Buff(targetUnit, DecreaseAttackValue, DecreaseAttackRate);

            }

    }

    public void DecreaseDMG_Buff(FightingUnit targetUnit, float percent, float luck)
    {
        float r = UnityEngine.Random.Range(0f, 100f);
        if (r > luck)
            return;

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

        targetUnit.AddBuff(DecreaseAttack);
    }
}
