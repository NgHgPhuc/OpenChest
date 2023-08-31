using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Devastating Charge", menuName = "Skill/Devastating Charge")]
public class DevastatingCharge : BaseSkill
{
    float IncreaseSpeedActiveValue;
    float IncreaseSpeedOnturnValue;

    float AttackDamageValueEffect;
    float SpeedDamageValueEffect;

    public override void UpgradeSkill_Effect()
    {
        switch(Level)
        {
            case 1:
                IncreaseSpeedActiveValue = 0.3f;
                IncreaseSpeedOnturnValue = 0.1f;

                AttackDamageValueEffect = 0.1f;
                SpeedDamageValueEffect = 0.3f;
                break;

            case 2:
                IncreaseSpeedActiveValue = 0.4f;
                IncreaseSpeedOnturnValue = 0.125f;

                AttackDamageValueEffect = 0.1f;
                SpeedDamageValueEffect = 0.325f;
                break;

            case 3:
                IncreaseSpeedActiveValue = 0.5f;
                IncreaseSpeedOnturnValue = 0.15f;

                AttackDamageValueEffect = 0.1f;
                SpeedDamageValueEffect = 0.35f;
                break;

            case 4:
                IncreaseSpeedActiveValue = 0.6f;
                IncreaseSpeedOnturnValue = 0.175f;

                AttackDamageValueEffect = 0.1f;
                SpeedDamageValueEffect = 0.375f;
                break;

            case 5:
                IncreaseSpeedActiveValue = 0.3f;
                IncreaseSpeedOnturnValue = 0.2f;

                AttackDamageValueEffect = 0.1f;
                SpeedDamageValueEffect = 0.4f;
                break;

            default:
                break;
        }
    }

    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        //first add 30% spd and 10% on each turn - at least 3 turn
        IncreaseSpeed_Buff(currentUnit, IncreaseSpeedActiveValue, IncreaseSpeedOnturnValue);
        IncreaseAtkOnSpd_Buff(currentUnit);
    }

    public void IncreaseSpeed_Buff(FightingUnit currentUnit, float activePercent,float onturnPercent)
    {
        Buff IncreaseSpeed = new Buff();

        IncreaseSpeed.type = Buff.Type.IncreaseDamage;
        IncreaseSpeed.duration = 3;

        IncreaseSpeed.SetIcon();
        //first add 30% spd and 10% on each turn - at least 3 turn
        IncreaseSpeed.ValueChange = currentUnit.basicStatsCharacter.Speed * activePercent;

        IncreaseSpeed.Activation = () =>
        {
            currentUnit.CharacterClone.Speed += IncreaseSpeed.ValueChange;
            FightManager.Instance.SortSpeed();
        };

        IncreaseSpeed.Deactivation = () =>
        {
            currentUnit.CharacterClone.Speed -= IncreaseSpeed.ValueChange;
            FightManager.Instance.SortSpeed();
        };

        IncreaseSpeed.Onactivation = () =>
        {
            IncreaseSpeed.ValueChange += currentUnit.basicStatsCharacter.Speed * onturnPercent;
            currentUnit.CharacterClone.Speed += IncreaseSpeed.ValueChange;
            FightManager.Instance.SortSpeed();
        };

        currentUnit.AddBuff(IncreaseSpeed);
    }
    public void IncreaseAtkOnSpd_Buff(FightingUnit currentUnit)
    {
        Buff AttackDamageOnSpeed = new Buff();

        AttackDamageOnSpeed.type = Buff.Type.AttackDamageOnSpeed;
        AttackDamageOnSpeed.duration = 1;

        AttackDamageOnSpeed.SetIcon();

        AttackDamageOnSpeed.Activation = () =>
        {
            currentUnit.Unit_Attack += EffectAtkOnSpd;
        };

        AttackDamageOnSpeed.Deactivation = () =>
        {
            currentUnit.Unit_Attack -= EffectAtkOnSpd;
        };

        currentUnit.AddBuff(AttackDamageOnSpeed);
    }

    void EffectAtkOnSpd(FightingUnit currentUnit,FightingUnit targetUnit,Attack HaveNothing,Defense HaveNothing2)
    {
        float DamageCause = currentUnit.CharacterClone.AttackDamage * AttackDamageValueEffect + currentUnit.CharacterClone.Speed * SpeedDamageValueEffect;

        if (targetUnit.stateFighting != FightingUnit.StateFighting.Death)
            targetUnit.OnlyTakenDamage(DamageCause, 100);//only get damage, not effect like attack

    }
}
