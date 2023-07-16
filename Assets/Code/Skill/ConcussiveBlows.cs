using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Concussive Blows", menuName = "Skill/Concussive Blows")]
public class ConcussiveBlows : BaseSkill
{
    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        Attack currentUnitAttack = currentUnit.attack();

        currentUnitAttack.DamageCause *= 0.8f;

        foreach (FightingUnit targetUnit in ChosenUnit)
            if (targetUnit.stateFighting != FightingUnit.StateFighting.Death)
            {
                Defense targetUnitDefense = targetUnit.defense();
                targetUnitDefense.IsCounter = false;

                TurnManager.Instance.AttackOneEnemy(currentUnit, targetUnit, currentUnitAttack, targetUnitDefense);

                float StunRate = UnityEngine.Random.Range(0f, 100f);
                if (StunRate < 30)
                {
                    InitBuff(currentUnit, targetUnit);
                    targetUnit.StunUI();
                    continue;
                }


            }
    }

    public void InitBuff(FightingUnit currentUnit, FightingUnit ChosenUnit)
    {
        Buff Stuning = new Buff();

        Stuning.type = Buff.Type.Stun;
        Stuning.duration = 1;

        Stuning.SetIcon();

        Stuning.Activation = () =>
        {
            ChosenUnit.IsStuning = true;
        };

        Stuning.Deactivation = () =>
        {
            ChosenUnit.IsStuning = false;
        };

        Stuning.Onactivation = () =>
        {
        };


        ChosenUnit.AddBuff(Stuning);
    }
}