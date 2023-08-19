using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Astral Infution", menuName = "Skill/Astral Infution")]
public class AstralInfusion : BaseSkill
{
    float luck;
    int duration;
    float value;
    float coefficient;

    public override void UpgradeSkill_Effect()
    {
        coefficient = 0.4f + Level / 10f;
        Cooldown = 3;
        switch (Level)
        {
            case 3://60% add healing buff heal 10% max hp/1turn in 1 turn
                luck = 60;
                duration = 1;
                value = 10;
                break;
            case 4:
                luck = 100;
                duration = 1;
                value = 10;
                break;
            case 5:
                luck = 100;
                duration = 2;
                value = 10;
                Cooldown = 2;
                break;
            default:
                break;
        }
    }

    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        foreach (FightingUnit targetUnit in ChosenUnit)
            if (targetUnit.stateFighting != FightingUnit.StateFighting.Death)
            {
                targetUnit.Heal(currentUnit.CharacterClone.AttackDamage * coefficient);
                InitBuff(targetUnit, luck, duration, value);
            }
    }

    public void InitBuff(FightingUnit targetUnit, float luck, int duration, float value)
    {
        float r = UnityEngine.Random.Range(0, 100);
        if (r > luck)
            return;

        Buff Healing = new Buff();

        Healing.type = Buff.Type.Healing;
        Healing.duration = duration;

        Healing.SetIcon();
        Healing.Onactivation = () =>
        {
            targetUnit.HealingPercentMaxHp(value);
        };


        targetUnit.AddBuff(Healing);
    }
}
