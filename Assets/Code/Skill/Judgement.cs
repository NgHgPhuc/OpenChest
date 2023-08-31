using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Judgement", menuName = "Skill/Judgement")]
public class Judgement : BaseSkill
{
    float DamageCause;
    float DecreaseDefPercent;
    int DecreaseDefDuration;

    float DamageSpread;

    public override void UpgradeSkill_Effect()
    {
        switch(Level)
        {
            case 1:
                DamageCause = 2f;
                DecreaseDefDuration = 1;
                DecreaseDefPercent = 0.4f;

                DamageSpread = 0f;
                break;

            case 2:
                DamageCause = 2.25f;
                DecreaseDefDuration = 1;
                DecreaseDefPercent = 0.45f;

                DamageSpread = 0f;
                break;

            case 3:
                DamageCause = 2.5f;
                DecreaseDefDuration = 2;
                DecreaseDefPercent = 0.5f;

                DamageSpread = 0.5f;
                break;

            case 4:
                DamageCause = 2.75f;
                DecreaseDefDuration = 2;
                DecreaseDefPercent = 0.55f;

                DamageSpread = 0.75f;
                break;

            case 5:
                DamageCause = 3f;
                DecreaseDefDuration = 2;
                DecreaseDefPercent = 0.6f;

                DamageSpread = 1f;
                break;

            default:
                break;
        }
    }

    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        Attack currentUnitAttack = currentUnit.attack();
        currentUnitAttack.DamageCause *= DamageCause;

        foreach (FightingUnit targetUnit in ChosenUnit)
            if (targetUnit.stateFighting != FightingUnit.StateFighting.Death)
            {
                Defense targetUnitDefense = targetUnit.defense(100);
                TurnManager.Instance.AttackOneEnemy(currentUnit, targetUnit, currentUnitAttack, targetUnitDefense);

                if (targetUnit.IsBlock == true)
                    targetUnit.EndShield();

                DecreaseDEF_Buff(targetUnit, DecreaseDefPercent,DecreaseDefDuration);
            }


        //Spread Damaged to all enemy
        Attack currentUnitAttackSpread = currentUnit.attack();
        currentUnitAttackSpread.DamageCause *= DamageSpread;
        foreach (FightingUnit targetUnit in ChosenUnit)
            if (targetUnit.stateFighting != FightingUnit.StateFighting.Death)
            {
                Defense targetUnitDefense = targetUnit.defense();
                TurnManager.Instance.AttackOneEnemy(currentUnit, targetUnit, currentUnitAttackSpread, targetUnitDefense);
            }

    }

    public void DecreaseDEF_Buff(FightingUnit targetUnit, float percent,int duration)
    {
        Buff DecreaseDEF = new Buff();

        DecreaseDEF.type = Buff.Type.IncreaseDef;
        DecreaseDEF.duration = duration;

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

        targetUnit.AddBuff(DecreaseDEF);
    }

    public void Broken_Debuff(FightingUnit targetUnit)
    {
        Buff Broken = new Buff();

        Broken.type = Buff.Type.Broken;
        Broken.duration = 2;

        Broken.SetIcon();
        Broken.ValueChange = targetUnit.basicStatsCharacter.DefensePoint;

        Broken.Activation = () =>
        {
            targetUnit.CharacterClone.DefensePoint -= Broken.ValueChange;
        };

        Broken.Deactivation = () =>
        {
            targetUnit.CharacterClone.DefensePoint += Broken.ValueChange;
        };

        Broken.Onactivation = () =>
        {
        };

        targetUnit.AddBuff(Broken);
    }
}
