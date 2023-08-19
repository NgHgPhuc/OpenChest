using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Encouring Shout", menuName = "Skill/Encouring Shout")]
public class EncouringShout : BaseSkill
{
    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        foreach(FightingUnit f in ChosenUnit)
        {
            f.AddBuff(IncreaseDMG_Buff(f, 0.2f));
            f.AddBuff(IncreaseDEF_Buff(f, 0.2f));
            f.AddBuff(IncreaseSPD_Buff(f, 0.2f));
            f.AddBuff(IncreaseMaxHP_Buff(f, 0.2f));
        }
    }

    public Buff IncreaseDMG_Buff(FightingUnit currentUnit, float percent)
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

        return IncreaseAttack;
    }

    public Buff IncreaseDEF_Buff(FightingUnit currentUnit, float percent)
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

        return IncreaseDEF;
    }

    public Buff IncreaseSPD_Buff(FightingUnit currentUnit, float percent)
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

        return IncreaseSPD;
    }

    public Buff IncreaseMaxHP_Buff(FightingUnit currentUnit, float percent)
    {
        Buff IncreaseMaxHP = new Buff();

        IncreaseMaxHP.type = Buff.Type.IncreaseMaxHP;
        IncreaseMaxHP.duration = 1;

        IncreaseMaxHP.SetIcon();
        IncreaseMaxHP.ValueChange = currentUnit.GetPercentMaxHP(percent * 100);

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

        return IncreaseMaxHP;
    }

    public override void UpgradeSkill_Effect()
    {
        throw new System.NotImplementedException();
    }
}
