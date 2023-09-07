using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

[CreateAssetMenu(fileName = "Unbreakable", menuName = "Skill/Unbreakable")]
public class Unbreakable : BaseSkill
{
    float DefenseValue;
    float HealingValue;

    float SharingDamage;
    public override void UpgradeSkill_Effect()
    {
        switch(Level)
        {
            case 1:
                DefenseValue = 0.4f;
                HealingValue = 0.1f;
                SharingDamage = 0.6f;
                break;

            case 2:
                DefenseValue = 0.45f;
                HealingValue = 0.15f;
                SharingDamage = 0.55f;
                break;

            case 3:
                DefenseValue = 0.5f;
                HealingValue = 0.2f;
                SharingDamage = 0.5f;
                break;

            case 4:
                DefenseValue = 0.55f;
                HealingValue = 0.25f;
                SharingDamage = 0.45f;
                break;

            case 5:
                DefenseValue = 0.6f;
                HealingValue = 0.3f;
                SharingDamage = 0.4f;
                break;
            default:
                break;
        }
    }


    FightingUnit currentUsing;
    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        this.currentUsing = currentUnit;
        IncreaseDEF_Buff(currentUnit, DefenseValue);
        Healing_Buff(currentUnit, HealingValue);
        foreach (FightingUnit f in FightManager.Instance.PlayerTeam)
            if (f != currentUnit)
                Ally_Buff(f);

    }

    public void IncreaseDEF_Buff(FightingUnit currentUnit, float percent)
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

        currentUnit.AddBuff(IncreaseDEF);
    }

    public void Healing_Buff(FightingUnit currentUnit, float percent)
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

        currentUnit.AddBuff(Healing);
    }

    public void Ally_Buff(FightingUnit currentUnit)
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

        currentUnit.AddBuff(AllyProtection);
    }
    public void ShareDamage(FightingUnit HaveNothing, FightingUnit HaveNothing2, Attack EnemyAttack, Defense HaveNothing3)
    {
        Debug.Log(EnemyAttack.DamageCause);
        float DamageCause = EnemyAttack.DamageCause * (1 - SharingDamage);
        this.currentUsing.OnlyTakenDamage(DamageCause, 0);

        EnemyAttack.DamageCause *= SharingDamage;

        Debug.Log(EnemyAttack.DamageCause);

    }
}
