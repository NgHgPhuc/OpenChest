using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Blade Surge", menuName = "Skill/Blade Surge")]
public class BladeSurge : BaseSkill
{
    float luck;
    int duration;
    float value;
    float coefficientDamage;
    float coefficientHealing;

    //use ok but cann't see all of skill
    //void SetAttrLevel(float coefficientDamage, float coefficientHealing, float luck, int duration, float value)
    //{
    //    this.coefficientDamage = coefficientDamage;
    //    this.coefficientHealing = coefficientHealing;
    //    this.luck = luck;
    //    this.duration = duration;
    //    this.value = value;
    //}
    public override void UpgradeSkill_Effect()
    {
        switch (Level)
        {
            case 1:
                coefficientDamage = 1.5f;
                coefficientHealing = 0.2f;
                luck = 0;
                duration = 0;
                value = 0;
                break;
            case 2:
                coefficientDamage = 1.6f;
                coefficientHealing = 0.25f;
                luck = 0;
                duration = 0;
                value = 0;
                break;
            case 3://60% effect "leech" to target - duration 1 turn, deal 5% max hp
                coefficientDamage = 1.8f;
                coefficientHealing = 0.3f;
                luck = 60;
                duration = 1;
                value = 5;
                break;
            case 4:
                coefficientDamage = 2.1f;
                coefficientHealing = 0.3f;
                luck = 100;
                duration = 1;
                value = 5;
                break;
            case 5:
                coefficientDamage = 2.1f;
                coefficientHealing = 0.3f;
                luck = 100;
                duration = 2;
                value = 8;
                break;
            default:
                break;
        }
    }

    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        Attack currentUnitAttack = currentUnit.attack();

        currentUnitAttack.DamageCause *= coefficientDamage;

        float LosingHP = (currentUnit.CharacterClone.HealthPoint - currentUnit.CurrentHP);
        currentUnit.Heal(coefficientHealing * LosingHP);

        foreach (FightingUnit targetUnit in ChosenUnit)
            if (targetUnit.stateFighting != FightingUnit.StateFighting.Death)
            {
                Defense targetUnitDefense = targetUnit.defense();
                if (targetUnitDefense.IsDodge)
                {
                    targetUnit.DogdeUI();
                    continue;
                }

                float targetGetDamage = currentUnitAttack.DamageCause * targetUnitDefense.TakenDmgPercent;

                targetUnit.BeingAttacked(targetGetDamage);
                InitBuff(targetUnit, luck, duration, value);
                currentUnit.LifeSteal(targetGetDamage);

                if (targetUnit.stateFighting == FightingUnit.StateFighting.Death)
                {
                    continue;
                }
            }
    }

    public void InitBuff(FightingUnit targetUnit, float luck, int duration, float value)
    {
        float r = UnityEngine.Random.Range(0, 100);
        if (r > luck)
            return;

        Buff Leech = new Buff();

        Leech.type = Buff.Type.Leech;
        Leech.duration = duration;

        Leech.SetIcon();

        Leech.Onactivation = () =>
        {
            targetUnit.BeingAttacked(value);
        };


        targetUnit.AddBuff(Leech);
    }
}
