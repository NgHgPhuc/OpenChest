using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Defiance Shout", menuName = "Skill/Defiance Shout")]
public class DefianceShout: BaseSkill
{
    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        Attack currentUnitAttack = currentUnit.attack();

        currentUnitAttack.DamageCause *= 0.4f;

        IncreaseDEF_Buff(currentUnit, 30);

        foreach (FightingUnit targetUnit in ChosenUnit)
            if (targetUnit.stateFighting != FightingUnit.StateFighting.Death)
            {
                Defense targetUnitDefense = targetUnit.defense();
                targetUnitDefense.IsCounter = false;

                TurnManager.Instance.AttackOneEnemy(currentUnit, targetUnit, currentUnitAttack, targetUnitDefense);

                float TauntRate = UnityEngine.Random.Range(0f, 100f);
                if (TauntRate < 30)
                    targetUnit.AddBuff(Taunt_Debuff(currentUnit, targetUnit));
            }
    }

    public Buff Taunt_Debuff(FightingUnit currentUnit, FightingUnit targetUnit)
    {
        Buff Taunt = new Buff();

        Taunt.type = Buff.Type.Taunt;
        Taunt.duration = 1;

        Taunt.SetIcon();

        Taunt.Activation = () =>
        {
            targetUnit.IsTaunted = true;
        };

        Taunt.Deactivation = () =>
        {
            FightManager.Instance.SetPlayerTargeted(null);
            targetUnit.IsTaunted = false;
        };

        Taunt.Onactivation = () =>
        {
            FightManager.Instance.SetPlayerTargeted(currentUnit);
        };

        return Taunt;
    }

    public Buff IncreaseDEF_Buff(FightingUnit currentUnit, float percent)
    {
        Buff IncreaseDEF = new Buff();

        IncreaseDEF.type = Buff.Type.IncreaseDef;
        IncreaseDEF.duration = 999;
        IncreaseDEF.ValueChange = currentUnit.basicStatsCharacter.AttackDamage * percent;
        IncreaseDEF.SetIcon();

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

        return IncreaseDEF;
    }
}
