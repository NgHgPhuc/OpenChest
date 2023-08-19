using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Concussive Blows", menuName = "Skill/Concussive Blows")]
public class ConcussiveBlows : BaseSkill
{
    float coefficientDamage;

    float luck_Stun;
    int duration_Stun;

    float luck_DecreaseDef;
    int duration_DecreaseDef;
    float value_DecreaseDef;
    public override void UpgradeSkill_Effect()
    {
        switch (Level)
        {
            case 1:
                coefficientDamage = 0.8f;
                luck_Stun = 40;
                duration_Stun = 1;

                luck_DecreaseDef = 0;
                duration_DecreaseDef = 0;
                value_DecreaseDef = 0;
                break;

            case 2:
                coefficientDamage = 0.85f;
                luck_Stun = 50;
                duration_Stun = 1;

                luck_DecreaseDef = 0;
                duration_DecreaseDef = 0;
                value_DecreaseDef = 0;
                break;

            case 3:
                coefficientDamage = 0.9f;
                luck_Stun = 50;
                duration_Stun = 1;

                luck_DecreaseDef = 20;
                duration_DecreaseDef = 1;
                value_DecreaseDef = 0.2f;
                break;

            case 4:
                coefficientDamage = 0.95f;
                luck_Stun = 60;
                duration_Stun = 1;

                luck_DecreaseDef = 30;
                duration_DecreaseDef = 1;
                value_DecreaseDef = 0.2f;
                break;

            case 5:
                coefficientDamage = 1f;
                luck_Stun = 60;
                duration_Stun = 1;

                luck_DecreaseDef = 40;
                duration_DecreaseDef = 1;
                value_DecreaseDef = 0.3f;
                break;

            default:
                break;
        }
    }

    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        Attack currentUnitAttack = currentUnit.attack();

        currentUnitAttack.DamageCause *= coefficientDamage;

        foreach (FightingUnit targetUnit in ChosenUnit)
            if (targetUnit.stateFighting != FightingUnit.StateFighting.Death)
            {
                Defense targetUnitDefense = targetUnit.defense();
                targetUnitDefense.IsCounter = false;

                TurnManager.Instance.AttackOneEnemy(currentUnit, targetUnit, currentUnitAttack, targetUnitDefense);

                InitBuff_Stun(targetUnit,luck_Stun,duration_Stun);
                InitBuff_DecreaseDef(targetUnit, luck_DecreaseDef, duration_DecreaseDef, value_DecreaseDef);
            }
    }

    void InitBuff_Stun(FightingUnit targetUnit, float luck, int duration)
    {
        float r = UnityEngine.Random.Range(0, 100);
        if (r > luck)
            return;

        Buff Stuning = new Buff();

        Stuning.type = Buff.Type.Stun;
        Stuning.duration = duration;

        Stuning.SetIcon();

        Stuning.Activation = () =>
        {
            targetUnit.IsStuning = true;
        };

        Stuning.Deactivation = () =>
        {
            targetUnit.IsStuning = false;
        };

        Stuning.Onactivation = () =>
        {
        };


        targetUnit.AddBuff(Stuning);
    }

    void InitBuff_DecreaseDef(FightingUnit targetUnit, float luck, int duration,float value)
    {
        float r = UnityEngine.Random.Range(0, 100);
        if (r > luck)
            return;

        Buff DecreaseDef = new Buff();

        DecreaseDef.type = Buff.Type.DecreaseDef;
        DecreaseDef.duration = duration;

        DecreaseDef.SetIcon();

        DecreaseDef.ValueChange = targetUnit.basicStatsCharacter.DefensePoint * value;

        DecreaseDef.Activation = () =>
        {
            targetUnit.CharacterClone.Speed -= DecreaseDef.ValueChange;
        };

        DecreaseDef.Deactivation = () =>
        {
            targetUnit.CharacterClone.Speed += DecreaseDef.ValueChange;
        };

        targetUnit.AddBuff(DecreaseDef);
    }
}