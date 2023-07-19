using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Astral Infution", menuName = "Skill/Astral Infution")]
public class AstralInfusion : BaseSkill
{
    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        foreach (FightingUnit targetUnit in ChosenUnit)
            if (targetUnit.stateFighting != FightingUnit.StateFighting.Death)
                targetUnit.Heal(currentUnit.CharacterClone.AttackDamage * 0.5f);
    }
}
