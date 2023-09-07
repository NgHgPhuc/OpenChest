using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vital Dance", menuName = "Skill/Vital Dance")]
public class VitalDance : BaseSkill
{
    float DamageCause;
    int BrokenDuration;
    public override void UpgradeSkill_Effect()
    {
        switch(Level)
        {
            case 1:
                DamageCause = 1f;
                BrokenDuration = 1;
                break;

            case 2:
                DamageCause = 1.1f;
                BrokenDuration = 1;
                break;

            case 3:
                DamageCause = 1.2f;
                BrokenDuration = 2;
                break;

            case 4:
                DamageCause = 1.4f;
                BrokenDuration = 2;
                break;

            case 5:
                DamageCause = 1.5f;
                BrokenDuration = 2;
                range = Range.OnEnemyTeam;
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
                Broken_Debuff(targetUnit, BrokenDuration);
            }
    }

    public void Broken_Debuff(FightingUnit targetUnit,int duration)
    {
        Buff Broken = new Buff();

        Broken.type = Buff.Type.Broken;
        Broken.duration = duration;

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

        targetUnit.AddBuff(Broken);
    }
}
