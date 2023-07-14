using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "Bola Strike", menuName = "Skill/Bola Strike")]
public class BolaStrike : BaseSkill
{

    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        Attack currentUnitAttack = currentUnit.attack();

        currentUnitAttack.DamageCause *= 1.2f;

        foreach (FightingUnit targetUnit in ChosenUnit)
            if (targetUnit.stateFighting != FightingUnit.StateFighting.Death)
            {
                Defense targetUnitDefense = targetUnit.defense();
                float targetGetDamage = currentUnitAttack.DamageCause * targetUnitDefense.TakenDmgPercent;

                if (targetUnitDefense.IsDogde)
                {
                    targetUnit.DogdeUI();
                    continue;
                }

                targetUnit.BeingAttacked(targetGetDamage);
                currentUnit.LifeSteal(targetGetDamage);

                float SlowRate = UnityEngine.Random.Range(0f, 100f);
                if (SlowRate < 40)
                    InitBuff(currentUnit, targetUnit);

            }



    }

    public void InitBuff(FightingUnit currentUnit, FightingUnit ChosenUnit)
    {
        Buff Slowing = new Buff();

        Slowing.type = Buff.Type.DecreaseSpeed;
        Slowing.duration = 1;

        Slowing.SetIcon();

        Slowing.ValueChange = ChosenUnit.basicStatsCharacter.Speed * 0.3f;

        Slowing.Activation = () =>
        {
            ChosenUnit.character.Speed -= Slowing.ValueChange;
            FightManager.Instance.SortSpeed();
        };

        Slowing.Deactivation = () =>
        {
            ChosenUnit.character.Speed += Slowing.ValueChange;
            FightManager.Instance.SortSpeed();
        };

        Slowing.Onactivation = () =>
        {
        };


        ChosenUnit.AddBuff(Slowing);
    }
}

