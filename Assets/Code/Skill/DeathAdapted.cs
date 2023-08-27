using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Death Adapted", menuName = "Skill/Death Adapted")]
public class DeathAdapted : BaseSkill
{
    int IncreaseMaxHP_Myself;
    float IncreaseDMG_Myself;
    float IncreaseDEF_Myself;

    float IncreaseDMG_Ally;
    public override void UpgradeSkill_Effect()
    {
        switch (Level)
        {
            case 1:
                IncreaseMaxHP_Myself = 40;
                IncreaseDMG_Myself = 0.30f;
                IncreaseDEF_Myself = 0.30f;

                IncreaseDMG_Ally = 0.1f;
                break;

            case 2:
                IncreaseMaxHP_Myself = 45;
                IncreaseDMG_Myself = 0.35f;
                IncreaseDEF_Myself = 0.35f;

                IncreaseDMG_Ally = 0.1f;
                break;

            case 3:
                IncreaseMaxHP_Myself = 50;
                IncreaseDMG_Myself = 0.4f;
                IncreaseDEF_Myself = 0.4f;

                IncreaseDMG_Ally = 0.15f;
                break;

            case 4:
                IncreaseMaxHP_Myself = 55;
                IncreaseDMG_Myself = 0.45f;
                IncreaseDEF_Myself = 0.45f;

                IncreaseDMG_Ally = 0.15f;
                break;

            case 5:
                IncreaseMaxHP_Myself = 60;
                IncreaseDMG_Myself = 0.5f;
                IncreaseDEF_Myself = 0.5f;

                IncreaseDMG_Ally = 0.2f;
                break;

            default:
                break;
        }
    }

    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        //if user have more than 50% hp or not
        if(currentUnit.CurrentHP > currentUnit.CharacterClone.HealthPoint / 2)
            currentUnit.IncreaseMaxHP(currentUnit.GetPercentMaxHP(IncreaseMaxHP_Myself)); //increase 40% max hp and heal the increasing
        else
        {
            IncreaseDMG_Buff(currentUnit, IncreaseDMG_Myself);//increase 30% atk
            IncreaseDEF_Buff(currentUnit, IncreaseDEF_Myself);//increase 30% def
        }

        //if player >= enemy -> increase 10% atk for all ally
        //if player < enemy -> heal 60% and increase 20% atk for user;
        int enemyCount = FightManager.Instance.EnemyTeam.Count;
        int playerCount = FightManager.Instance.PlayerTeam.Count;
        if (enemyCount <= playerCount)
        {
            foreach(FightingUnit f in FightManager.Instance.PlayerTeam)
                IncreaseDMG_Buff(f, IncreaseDMG_Ally);//increase 10% atk
        }
        else
        {
            float LosingHP = (currentUnit.CharacterClone.HealthPoint - currentUnit.CurrentHP);
            currentUnit.Heal(0.5f * LosingHP);
            IncreaseDMG_Buff(currentUnit, 0.2f);
        }

    }

    public void IncreaseDMG_Buff(FightingUnit currentUnit, float percent)
    {
        Buff IncreaseAttack = new Buff();

        IncreaseAttack.type = Buff.Type.IncreaseDamage;
        IncreaseAttack.duration = 999;

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

        currentUnit.AddBuff(IncreaseAttack);
    }

    public void IncreaseDEF_Buff(FightingUnit currentUnit,float percent)
    {
        Buff IncreaseDEF = new Buff();

        IncreaseDEF.type = Buff.Type.IncreaseDef;
        IncreaseDEF.duration = 999;

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

        currentUnit.AddBuff(IncreaseDEF);
    }
}