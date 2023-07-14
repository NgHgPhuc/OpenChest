using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Devastating Charge", menuName = "Skill/Devastating Charge")]
public class DevastatingCharge : BaseSkill
{
    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        //first add 30% spd and 10% on each turn - at least 3 turn
        currentUnit.AddBuff(IncreaseSpeed_Buff(currentUnit,0.1f));
        currentUnit.AddBuff(IncreaseAtkOnSpd_Buff(currentUnit));
    }

    public Buff IncreaseSpeed_Buff(FightingUnit currentUnit, float percent)
    {
        Buff IncreaseSpeed = new Buff();

        IncreaseSpeed.type = Buff.Type.IncreaseDamage;
        IncreaseSpeed.duration = 3;

        IncreaseSpeed.SetIcon();
        IncreaseSpeed.ValueChange = currentUnit.basicStatsCharacter.Speed * 3 * percent;

        IncreaseSpeed.Activation = () =>
        {
            currentUnit.character.Speed += IncreaseSpeed.ValueChange;
            FightManager.Instance.SortSpeed();
        };

        IncreaseSpeed.Deactivation = () =>
        {
            currentUnit.character.Speed -= IncreaseSpeed.ValueChange;
            FightManager.Instance.SortSpeed();
        };

        IncreaseSpeed.Onactivation = () =>
        {
            IncreaseSpeed.ValueChange += currentUnit.basicStatsCharacter.Speed * percent;
            currentUnit.character.Speed += IncreaseSpeed.ValueChange;
            FightManager.Instance.SortSpeed();
        };

        return IncreaseSpeed;
    }
    public Buff IncreaseAtkOnSpd_Buff(FightingUnit currentUnit)
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

        AttackDamageOnSpeed.Onactivation = () =>
        {
        };

        return AttackDamageOnSpeed;
    }

    void EffectAtkOnSpd(FightingUnit currentUnit,FightingUnit targetUnit)
    {
        Attack currentUnitAttack = new Attack();
        currentUnitAttack.DamageCause = currentUnit.character.AttackDamage*0.1f + currentUnit.character.Speed*0.4f;
        currentUnitAttack.IsStun = false;
        currentUnitAttack.IsCrit = false;
        currentUnitAttack.IsHaveEffect = false;

        Defense targetUnitDefense = targetUnit.defense(100);
        targetUnitDefense.IsCounter = false;
        targetUnitDefense.IsDogde = false;
        targetUnitDefense.IsHaveEffect = false;

        if (targetUnit.stateFighting != FightingUnit.StateFighting.Death)
            TurnManager.Instance.AttackOneEnemy(currentUnit, targetUnit, currentUnitAttack, targetUnitDefense);
    }
}
