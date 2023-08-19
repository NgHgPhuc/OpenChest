using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unbreakable", menuName = "Skill/Unbreakable")]
public class Unbreakable : BaseSkill
{
    FightingUnit currentUsing;
    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        this.currentUsing = currentUnit;
        currentUnit.AddBuff(IncreaseDEF_Buff(currentUnit, 0.4f));
        currentUnit.AddBuff(Healing_Buff(currentUnit, 0.1f));
        foreach (FightingUnit f in FightManager.Instance.PlayerTeam)
            if (f != currentUnit)
                f.AddBuff(Ally_Buff(f));

    }

    public Buff IncreaseDEF_Buff(FightingUnit currentUnit, float percent)
    {
        Buff IncreaseDEF = new Buff();

        IncreaseDEF.type = Buff.Type.IncreaseDef;
        IncreaseDEF.duration = 3;

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

    public Buff Healing_Buff(FightingUnit currentUnit, float percent)
    {
        Buff Healing = new Buff();

        Healing.type = Buff.Type.Healing;
        Healing.duration = 3;

        Healing.SetIcon();

        Healing.Activation = () =>
        {
        };

        Healing.Deactivation = () =>
        {

        };

        Healing.Onactivation = () =>
        {
            currentUnit.Heal(currentUnit.GetPercentMaxHP(percent*100));
        };

        return Healing;
    }

    public Buff Ally_Buff(FightingUnit currentUnit)
    {
        Buff AllyProtection = new Buff();

        AllyProtection.type = Buff.Type.AllyProtection;
        AllyProtection.duration = 2;

        AllyProtection.SetIcon();

        AllyProtection.Activation = () =>
        {
            currentUnit.Unit_BeAttacked += ShareDamage;
        };

        AllyProtection.Deactivation = () =>
        {
            currentUnit.Unit_BeAttacked -= ShareDamage;
        };

        AllyProtection.Onactivation = () =>
        {
        };

        return AllyProtection;
    }
    public void ShareDamage(FightingUnit HaveNothing, FightingUnit HaveNothing2, Attack EnemyAttack, Defense HaveNothing3)
    {
        Debug.Log(EnemyAttack.DamageCause);
        float DamageCause = EnemyAttack.DamageCause * 0.4f;
        this.currentUsing.OnlyTakenDamage(DamageCause, 0);

        EnemyAttack.DamageCause *= 0.6f;

        Debug.Log(EnemyAttack.DamageCause);

    }

    public override void UpgradeSkill_Effect()
    {
        throw new System.NotImplementedException();
    }
}
