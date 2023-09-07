using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "The Last Lighting", menuName = "Skill/The Last Lighting")]
public class TheLastLighting : BaseSkill
{
    int HpUsing;

    float IncreaseAtkValue;
    int IncreaseAtkDuration;

    int HealingValue;
    int HealingDuration;
    int HealingLuck;

    public override void UpgradeSkill_Effect()
    {
        switch(Level)
        {
            case 1:
                HpUsing = 20;
                IncreaseAtkValue = 0.3f;
                IncreaseAtkDuration = 1;
                break;

            case 2:
                HpUsing = 15;
                IncreaseAtkValue = 0.35f;
                IncreaseAtkDuration = 1;
                break;

            case 3:
                HpUsing = 10;
                IncreaseAtkValue = 0.4f;
                IncreaseAtkDuration = 1;
                break;

            case 4:
                HpUsing = 10;
                IncreaseAtkValue = 0.4f;
                IncreaseAtkDuration = 2;

                HealingValue = 10;
                HealingDuration = 1;
                HealingLuck = 80;
                break;

            case 5:
                HpUsing = 10;
                IncreaseAtkValue = 0.4f;
                IncreaseAtkDuration = 2;

                HealingValue = 10;
                HealingDuration = 2;
                HealingLuck = 100;
                break;

            default:
                break;
        }
    }

    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        currentUnit.UsingPercentMaxHP(HpUsing);

        foreach (FightingUnit targetUnit in ChosenUnit)
            if (targetUnit.stateFighting != FightingUnit.StateFighting.Death)
            {
                IncreaseAtk(targetUnit, IncreaseAtkDuration, IncreaseAtkValue);
                Healing(targetUnit, HealingLuck, HealingDuration, HealingValue);
            }
    }

    void IncreaseAtk(FightingUnit targetUnit, int duration, float value)
    {
        Buff IncreaseDamaged = new Buff();

        IncreaseDamaged.type = Buff.Type.IncreaseDamage;
        IncreaseDamaged.duration = duration;

        IncreaseDamaged.SetIcon();

        IncreaseDamaged.ValueChange = targetUnit.basicStatsCharacter.AttackDamage * value;

        IncreaseDamaged.Activation = () =>
        {
            targetUnit.CharacterClone.AttackDamage += IncreaseDamaged.ValueChange;
        };

        IncreaseDamaged.Deactivation = () =>
        {
            targetUnit.CharacterClone.AttackDamage -= IncreaseDamaged.ValueChange;
        };

        targetUnit.AddBuff(IncreaseDamaged);
    }

    public void Healing(FightingUnit targetUnit, float luck, int duration, float value)
    {
        float r = UnityEngine.Random.Range(0, 100);
        if (r > luck)
            return;

        Buff Healing = new Buff();

        Healing.type = Buff.Type.Healing;
        Healing.duration = duration;

        Healing.SetIcon();
        Healing.Onactivation = () =>
        {
            targetUnit.HealingPercentMaxHp(value);
        };


        targetUnit.AddBuff(Healing);
    }
}