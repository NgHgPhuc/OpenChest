using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Encouring Shout", menuName = "Skill/Encouring Shout")]
public class EncouringShout : BaseSkill
{
    float DamageIncreaseValue;
    float DefenseIncreaseValue;
    float SpeedIncreaseValue;
    float MaxHPIncreaseValue;

    public override void UpgradeSkill_Effect()
    {
        switch(Level)
        {
            case 1:
                DamageIncreaseValue = 0.2f;
                DefenseIncreaseValue = 0.2f;
                SpeedIncreaseValue = 0.2f;
                MaxHPIncreaseValue = 0.2f;
                break;

            case 2:
                DamageIncreaseValue = 0.25f;
                DefenseIncreaseValue = 0.25f;
                SpeedIncreaseValue = 0.25f;
                MaxHPIncreaseValue = 0.25f;
                break;

            case 3:
                DamageIncreaseValue = 0.3f;
                DefenseIncreaseValue = 0.3f;
                SpeedIncreaseValue = 0.3f;
                MaxHPIncreaseValue = 0.3f;
                break;

            case 4:
                DamageIncreaseValue = 0.35f;
                DefenseIncreaseValue = 0.35f;
                SpeedIncreaseValue = 0.35f;
                MaxHPIncreaseValue = 0.35f;
                break;

            case 5:
                DamageIncreaseValue = 0.4f;
                DefenseIncreaseValue = 0.4f;
                SpeedIncreaseValue = 0.4f;
                MaxHPIncreaseValue = 0.4f;
                break;

            default:
                break;
        }
    }

    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        foreach(FightingUnit f in ChosenUnit)
        {
            IncreaseDMG_Buff(f, DamageIncreaseValue);
            IncreaseDEF_Buff(f, DefenseIncreaseValue);
            IncreaseSPD_Buff(f, SpeedIncreaseValue);
            IncreaseMaxHP_Buff(f, MaxHPIncreaseValue);
        }
    }

    public void IncreaseDMG_Buff(FightingUnit currentUnit, float percent)
    {
        Buff IncreaseAttack = new Buff();

        IncreaseAttack.type = Buff.Type.IncreaseDamage;
        IncreaseAttack.duration = 1;

        IncreaseAttack.SetIcon();
        IncreaseAttack.ValueChange = currentUnit.basicStatsCharacter.AttackDamage * percent;

        IncreaseAttack.Activation = () =>
        {
            currentUnit.CharacterClone.AttackDamage += IncreaseAttack.ValueChange;
        };

        IncreaseAttack.Deactivation = () =>
        {
            currentUnit.CharacterClone.AttackDamage -= IncreaseAttack.ValueChange;
        };

        IncreaseAttack.Onactivation = () =>
        {
        };

        currentUnit.AddBuff(IncreaseAttack);
    }

    public void IncreaseDEF_Buff(FightingUnit currentUnit, float percent)
    {
        Buff IncreaseDEF = new Buff();

        IncreaseDEF.type = Buff.Type.IncreaseDef;
        IncreaseDEF.duration = 1;

        IncreaseDEF.SetIcon();
        IncreaseDEF.ValueChange = currentUnit.basicStatsCharacter.DefensePoint * percent;

        IncreaseDEF.Activation = () =>
        {
            currentUnit.CharacterClone.DefensePoint += IncreaseDEF.ValueChange;
        };

        IncreaseDEF.Deactivation = () =>
        {
            currentUnit.CharacterClone.DefensePoint -= IncreaseDEF.ValueChange;
        };

        IncreaseDEF.Onactivation = () =>
        {
        };

        currentUnit.AddBuff(IncreaseDEF);
    }

    public void IncreaseSPD_Buff(FightingUnit currentUnit, float percent)
    {
        Buff IncreaseSPD = new Buff();

        IncreaseSPD.type = Buff.Type.IncreaseSpeed;
        IncreaseSPD.duration = 1;

        IncreaseSPD.SetIcon();
        IncreaseSPD.ValueChange = currentUnit.basicStatsCharacter.Speed * percent;

        IncreaseSPD.Activation = () =>
        {
            currentUnit.CharacterClone.Speed += IncreaseSPD.ValueChange;
        };

        IncreaseSPD.Deactivation = () =>
        {
            currentUnit.CharacterClone.Speed -= IncreaseSPD.ValueChange;
        };

        IncreaseSPD.Onactivation = () =>
        {
        };

        currentUnit.AddBuff(IncreaseSPD);
    }

    public void IncreaseMaxHP_Buff(FightingUnit currentUnit, float percent)
    {
        Buff IncreaseMaxHP = new Buff();

        IncreaseMaxHP.type = Buff.Type.IncreaseMaxHP;
        IncreaseMaxHP.duration = 1;

        IncreaseMaxHP.SetIcon();
        IncreaseMaxHP.ValueChange = currentUnit.GetPercentMaxHP(percent);

        IncreaseMaxHP.Activation = () =>
        {
            currentUnit.IncreaseMaxHP(IncreaseMaxHP.ValueChange);
        };

        IncreaseMaxHP.Deactivation = () =>
        {
            currentUnit.DecreaseMaxHP(IncreaseMaxHP.ValueChange);
        };

        IncreaseMaxHP.Onactivation = () =>
        {
        };

        currentUnit.AddBuff(IncreaseMaxHP);
    }
}
