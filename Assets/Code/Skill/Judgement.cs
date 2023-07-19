using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Judgement", menuName = "Skill/Judgement")]
public class Judgement : BaseSkill
{
    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        Attack currentUnitAttack = currentUnit.attack();

        currentUnitAttack.DamageCause *= 1.5f;

        foreach (FightingUnit targetUnit in ChosenUnit)
            if (targetUnit.stateFighting != FightingUnit.StateFighting.Death)
            {
                Defense targetUnitDefense = targetUnit.defense(100);
                TurnManager.Instance.AttackOneEnemy(currentUnit, targetUnit, currentUnitAttack, targetUnitDefense);

                if (targetUnit.IsBlock == true)
                    targetUnit.EndShield();

                targetUnit.AddBuff(DecreaseDEF_Buff(targetUnit, 0.4f));
            }
    }

    public Buff DecreaseDEF_Buff(FightingUnit targetUnit, float percent)
    {
        Buff DecreaseDEF = new Buff();

        DecreaseDEF.type = Buff.Type.IncreaseDef;
        DecreaseDEF.duration = 1;

        DecreaseDEF.SetIcon();
        DecreaseDEF.ValueChange = targetUnit.basicStatsCharacter.DefensePoint * percent;

        DecreaseDEF.Activation = () =>
        {
            targetUnit.CharacterClone.DefensePoint += DecreaseDEF.ValueChange;
        };

        DecreaseDEF.Deactivation = () =>
        {
            targetUnit.CharacterClone.DefensePoint -= DecreaseDEF.ValueChange;
        };

        DecreaseDEF.Onactivation = () =>
        {
        };

        return DecreaseDEF;
    }
}
