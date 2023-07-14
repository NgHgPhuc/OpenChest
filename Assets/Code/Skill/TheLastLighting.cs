using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "The Last Lighting", menuName = "Skill/The Last Lighting")]
public class TheLastLighting : BaseSkill
{
    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        foreach (FightingUnit targetUnit in ChosenUnit)
            if (targetUnit.stateFighting != FightingUnit.StateFighting.Death)
            {
                currentUnit.UsingPercentHP(30);

                Buff IncreaseDamaged = new Buff();

                IncreaseDamaged.type = Buff.Type.IncreaseDamage;
                IncreaseDamaged.duration = 1;

                IncreaseDamaged.SetIcon();

                IncreaseDamaged.ValueChange = targetUnit.basicStatsCharacter.AttackDamage * 0.3f;

                IncreaseDamaged.Activation = () =>
                {
                    targetUnit.character.AttackDamage += IncreaseDamaged.ValueChange;
                };

                IncreaseDamaged.Deactivation = () =>
                {
                    targetUnit.character.AttackDamage -= IncreaseDamaged.ValueChange;
                };

                IncreaseDamaged.Onactivation = () =>
                {
                };

                targetUnit.AddBuff(IncreaseDamaged);
            }
    }
}