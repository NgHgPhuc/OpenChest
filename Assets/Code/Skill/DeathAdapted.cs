using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Death Adapted", menuName = "Skill/Death Adapted")]
public class DeathAdapted : BaseSkill
{
    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        //if user have more than 50% hp or not
        if(currentUnit.CurrentHP > currentUnit.character.HealthPoint / 2)
            currentUnit.IncreaseMaxHP(currentUnit.GetPercentMaxHP(40)); //increase 40% max hp and heal the increasing
        else
        {
            currentUnit.AddBuff(IncreaseDMG_Buff(currentUnit,0.3f));//increase 30% atk
            currentUnit.AddBuff(IncreaseDEF_Buff(currentUnit, 0.3f));//increase 30% def
        }

        //if player >= enemy -> increase 10% atk for all ally
        //if player < enemy -> heal 60% and increase 20% atk for user;
        int enemyCount = FightManager.Instance.EnemyTeam.Count;
        int playerCount = FightManager.Instance.PlayerTeam.Count;
        if (enemyCount <= playerCount)
        {
            foreach(FightingUnit f in FightManager.Instance.PlayerTeam)
                f.AddBuff(IncreaseDMG_Buff(currentUnit, 0.1f));//increase 10% atk
        }
        else
        {
            float LosingHP = (currentUnit.character.HealthPoint - currentUnit.CurrentHP);
            currentUnit.Heal(0.5f * LosingHP);
            currentUnit.AddBuff(IncreaseDMG_Buff(currentUnit, 0.2f));
        }

    }

    public Buff IncreaseDMG_Buff(FightingUnit currentUnit, float percent)
    {
        Buff IncreaseAttack = new Buff();

        IncreaseAttack.type = Buff.Type.IncreaseDamage;
        IncreaseAttack.duration = 999;

        IncreaseAttack.SetIcon();
        IncreaseAttack.ValueChange = currentUnit.basicStatsCharacter.AttackDamage * percent;

        IncreaseAttack.Activation = () =>
        {
            currentUnit.character.AttackDamage += IncreaseAttack.ValueChange;
        };

        IncreaseAttack.Deactivation = () =>
        {
            currentUnit.character.AttackDamage -= IncreaseAttack.ValueChange;
        };

        IncreaseAttack.Onactivation = () =>
        {
        };

        return IncreaseAttack;
    }

    public Buff IncreaseDEF_Buff(FightingUnit currentUnit,float percent)
    {
        Buff IncreaseDEF = new Buff();

        IncreaseDEF.type = Buff.Type.IncreaseDef;
        IncreaseDEF.duration = 999;

        IncreaseDEF.SetIcon();
        IncreaseDEF.ValueChange = currentUnit.basicStatsCharacter.DefensePoint * percent;

        IncreaseDEF.Activation = () =>
        {
            currentUnit.character.DefensePoint += IncreaseDEF.ValueChange;
        };

        IncreaseDEF.Deactivation = () =>
        {
            currentUnit.character.DefensePoint -= IncreaseDEF.ValueChange;
        };

        IncreaseDEF.Onactivation = () =>
        {
        };

        return IncreaseDEF;
    }
}