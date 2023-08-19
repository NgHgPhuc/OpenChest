using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Darkness Balde", menuName = "Skill/Darkness Blade")]
public class DarknessBlade : BaseSkill
{
    float percentHpLost;

    int duration_IncreaseDamage;
    float value_IncreaseDamage;

    int duration_IncreaseDef;
    float value_IncreaseDef;
    public override void UpgradeSkill_Effect()
    {
        switch (Level)
        {
            case 1:
                percentHpLost = 20;

                duration_IncreaseDamage = 1;
                value_IncreaseDamage = 0.6f;

                duration_IncreaseDef = 0;
                value_IncreaseDef = 0;
                break;

            case 2:
                percentHpLost = 15;

                duration_IncreaseDamage = 1;
                value_IncreaseDamage = 0.7f;

                duration_IncreaseDef = 0;
                value_IncreaseDef = 0;
                break;

            case 3:
                percentHpLost = 10;

                duration_IncreaseDamage = 1;
                value_IncreaseDamage = 0.85f;

                duration_IncreaseDef = 1;
                value_IncreaseDef = 0.2f;
                break;

            case 4:
                percentHpLost = 5;

                duration_IncreaseDamage = 1;
                value_IncreaseDamage = 1f;

                duration_IncreaseDef = 1;
                value_IncreaseDef = 0.4f;
                break;

            case 5:
                percentHpLost = 0;

                duration_IncreaseDamage = 2;
                value_IncreaseDamage = 1f;

                duration_IncreaseDef = 2;
                value_IncreaseDef = 0.4f;
                break;

            default:
                break;
        }
    }

    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        currentUnit.UsingPercentCurrentHP(percentHpLost);
        foreach(FightingUnit AllyUnit in ChosenUnit)
        {
            AllyUnit.InitializeEffect(EffectPos.InMiddle, EffectManager.EffectName.TheDarknessBlade, 0.85f);

            InitBuff_IncreaseDamage(AllyUnit, duration_IncreaseDamage, value_IncreaseDamage);
            InitBuff_IncreaseDef(AllyUnit, duration_IncreaseDef, value_IncreaseDef);
        }
        
    }

    void InitBuff_IncreaseDamage(FightingUnit targetUnit, int duration, float value)
    {
        targetUnit.InitializeEffect(EffectPos.InMiddle, EffectManager.EffectName.TheDarknessBlade, 0.85f);
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

        IncreaseDamaged.Onactivation = () =>
        {
        };

        targetUnit.AddBuff(IncreaseDamaged);
    }

    void InitBuff_IncreaseDef(FightingUnit targetUnit, int duration, float value)
    {
        Buff IncreaseDef = new Buff();

        IncreaseDef.type = Buff.Type.IncreaseDef;
        IncreaseDef.duration = duration;

        IncreaseDef.SetIcon();

        IncreaseDef.ValueChange = targetUnit.basicStatsCharacter.DefensePoint * value;

        IncreaseDef.Activation = () =>
        {
            targetUnit.CharacterClone.DefensePoint += IncreaseDef.ValueChange;
        };

        IncreaseDef.Deactivation = () =>
        {
            targetUnit.CharacterClone.DefensePoint -= IncreaseDef.ValueChange;
        };

        targetUnit.AddBuff(IncreaseDef);
    }
}