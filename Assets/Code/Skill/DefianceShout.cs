using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Defiance Shout", menuName = "Skill/Defiance Shout")]
public class DefianceShout : BaseSkill
{

    float DamageCause;
    float IncreaseDefRate;
    float IncreaseDefValue;

    float TauntRate;

    float HealingValue;
    float HealingRate;

    public override void UpgradeSkill_Effect()
    {
        switch (Level)
        {
            case 1:
                DamageCause = 0.4f;
                IncreaseDefRate = 80;
                IncreaseDefValue = 30;

                TauntRate = 30;

                HealingRate = 0;
                HealingValue = 0;
                break;

            case 2:
                DamageCause = 0.4f;
                IncreaseDefRate = 100;
                IncreaseDefValue = 35;

                TauntRate = 35;

                HealingRate = 0;
                HealingValue = 0;

                break;

            case 3:
                DamageCause = 0.4f;
                IncreaseDefRate = 100;
                IncreaseDefValue = 40;

                TauntRate = 40;

                HealingRate = 0;
                HealingValue = 0;

                break;

            case 4:
                DamageCause = 0.4f;
                IncreaseDefRate = 100;
                IncreaseDefValue = 45;

                TauntRate = 45;

                HealingRate = 60;
                HealingValue = 10;

                break;

            case 5:
                DamageCause = 0.4f;
                IncreaseDefRate = 100;
                IncreaseDefValue = 50;

                TauntRate = 50;

                HealingRate = 100;
                HealingValue = 15;


                break;

            default:
                break;
        }
    }

    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        Attack currentUnitAttack = currentUnit.attack();

        currentUnitAttack.DamageCause *= DamageCause;

        IncreaseDEF_Buff(currentUnit, IncreaseDefRate, IncreaseDefValue);
        Healing_Buff(currentUnit, HealingRate, HealingValue);
        foreach (FightingUnit targetUnit in ChosenUnit)
            if (targetUnit.stateFighting != FightingUnit.StateFighting.Death)
            {
                Defense targetUnitDefense = targetUnit.defense();
                targetUnitDefense.IsCounter = false;

                TurnManager.Instance.AttackOneEnemy(currentUnit, targetUnit, currentUnitAttack, targetUnitDefense);

                float TauntRate = UnityEngine.Random.Range(0f, 100f);
                if (TauntRate < 30)
                    Taunt_Debuff(currentUnit, targetUnit,TauntRate);
            }
    }

    public void Taunt_Debuff(FightingUnit currentUnit, FightingUnit targetUnit, float luck)
    {

        float r = UnityEngine.Random.Range(0, 100);
        if (r > luck)
            return;

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

        targetUnit.AddBuff(Taunt);
    }

    public void IncreaseDEF_Buff(FightingUnit currentUnit, float luck, float percent)
    {
        float r = UnityEngine.Random.Range(0, 100);
        if (r > luck)
            return;

        Buff IncreaseDEF = new Buff();

        IncreaseDEF.type = Buff.Type.IncreaseDef;
        IncreaseDEF.duration = 1;
        IncreaseDEF.ValueChange = currentUnit.basicStatsCharacter.DefensePoint * percent;
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

        currentUnit.AddBuff(IncreaseDEF);
    }

    public void Healing_Buff(FightingUnit targetUnit, float luck, float value)
    {
        float r = UnityEngine.Random.Range(0, 100);
        if (r > luck)
            return;

        Buff Healing = new Buff();

        Healing.type = Buff.Type.Healing;
        Healing.duration = 2;

        Healing.SetIcon();
        Healing.Onactivation = () =>
        {
            targetUnit.HealingPercentMaxHp(value);
        };


        targetUnit.AddBuff(Healing);
    }
}
