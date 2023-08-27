using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "Bola Strike", menuName = "Skill/Bola Strike")]
public class BolaStrike : BaseSkill
{
    float luck;
    int duration;
    float value;
    float coefficientDamage;
    float selfLuck; // to init buff for my self
    int selfDuration;
    float selfValue;
    public override void UpgradeSkill_Effect()
    {
        switch (Level)
        {
            case 1:
                coefficientDamage = 0.8f;
                luck = 40;
                duration = 1;
                value = 0.3f;

                selfLuck = 0;
                selfDuration = 0;
                selfValue = 0;
                break;
            case 2:
                coefficientDamage = 0.85f;
                luck = 50;
                duration = 1;
                value = 0.3f;

                selfLuck = 0;
                selfDuration = 0;
                selfValue = 0;
                break;
            case 3://60% effect "leech" to target - duration 1 turn, deal 5% max hp
                coefficientDamage = 0.9f;
                luck = 60;
                duration = 1;
                value = 0.35f;

                selfLuck = 10;
                selfDuration = 1;
                selfValue = 0.1f;
                break;
            case 4:
                coefficientDamage = 0.95f;
                luck = 80;
                duration = 2;
                value = 0.4f;

                selfLuck = 20;
                selfDuration = 1;
                selfValue = 0.15f;
                break;
            case 5:
                coefficientDamage = 1f;
                luck = 100;
                duration = 3;
                value = 0.5f;

                selfLuck = 30;
                selfDuration = 1;
                selfValue = 0.2f;
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
                TurnManager.Instance.AttackOneEnemy(currentUnit, targetUnit, currentUnitAttack, targetUnitDefense);
                if (targetUnitDefense.IsDodge)
                {
                    targetUnit.DodgeUI();
                    continue;
                }

                //Transform FromPos = currentUnit.GetIconImage().transform;
                //Transform ToPos = targetUnit.GetIconImage().transform;
                //float width = currentUnit.GetIconImage().rectTransform.rect.width;
                //float height = currentUnit.GetIconImage().rectTransform.rect.height / 2;
                //Debug.Log("Inside Skill:" + FromPos.position + "->" + ToPos.position);
                //EffectManager.Instance.InitializeShooting(FromPos, ToPos, EffectManager.EffectName.BolaStrikes, 3f, width, height);

                InitBuff_DecreaseSpeed(targetUnit, luck, duration, value);
            }

        InitBuff_IncreaseSpeed(currentUnit, selfLuck, selfDuration, selfValue);
    }

    public void InitBuff_DecreaseSpeed(FightingUnit targetUnit, float luck, int duration, float value)
    {
        float r = UnityEngine.Random.Range(0, 100);
        if (r > luck)
            return;

        Buff Slowing = new Buff();

        Slowing.type = Buff.Type.DecreaseSpeed;
        Slowing.duration = duration;

        Slowing.SetIcon();

        Slowing.ValueChange = targetUnit.basicStatsCharacter.Speed * value;//0.3f

        Slowing.Activation = () =>
        {
            targetUnit.CharacterClone.Speed -= Slowing.ValueChange;
            FightManager.Instance.SortSpeed();
        };

        Slowing.Deactivation = () =>
        {
            targetUnit.CharacterClone.Speed += Slowing.ValueChange;
            FightManager.Instance.SortSpeed();
        };

        targetUnit.AddBuff(Slowing);
    }

    public void InitBuff_IncreaseSpeed(FightingUnit targetUnit, float luck, int duration, float value)
    {
        float r = UnityEngine.Random.Range(0, 100);
        if (r > luck)
            return;

        Buff Fasting = new Buff();

        Fasting.type = Buff.Type.IncreaseSpeed;
        Fasting.duration = duration;

        Fasting.SetIcon();

        Fasting.ValueChange = targetUnit.basicStatsCharacter.Speed * value;//0.3f

        Fasting.Activation = () =>
        {
            targetUnit.CharacterClone.Speed += Fasting.ValueChange;
            FightManager.Instance.SortSpeed();
        };

        Fasting.Deactivation = () =>
        {
            targetUnit.CharacterClone.Speed -= Fasting.ValueChange;
            FightManager.Instance.SortSpeed();
        };

        targetUnit.AddBuff(Fasting);
    }
}

