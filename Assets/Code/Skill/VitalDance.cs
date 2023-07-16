using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vital Dance", menuName = "Skill/Vital Dance")]
public class VitalDance : BaseSkill
{
    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        Attack currentUnitAttack = currentUnit.attack();

        currentUnitAttack.DamageCause *= 1.8f;

        foreach (FightingUnit targetUnit in ChosenUnit)
            if (targetUnit.stateFighting != FightingUnit.StateFighting.Death)
            {
                Defense targetUnitDefense = targetUnit.defense(100);
                TurnManager.Instance.AttackOneEnemy(currentUnit, targetUnit, currentUnitAttack, targetUnitDefense);
                targetUnit.AddBuff(Broken_Debuff(targetUnit));
            }
    }

    public Buff Broken_Debuff(FightingUnit targetUnit)
    {
        Buff Broken = new Buff();

        Broken.type = Buff.Type.Broken;
        Broken.duration = 2;

        Broken.SetIcon();
        Broken.ValueChange = targetUnit.basicStatsCharacter.DefensePoint;

        Broken.Activation = () =>
        {
            targetUnit.character.DefensePoint -= Broken.ValueChange;
        };

        Broken.Deactivation = () =>
        {
            targetUnit.character.DefensePoint += Broken.ValueChange;
        };

        Broken.Onactivation = () =>
        {
        };

        return Broken;
    }
}
