using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vanguard Edge", menuName = "Skill/Vanguard Edge")]
public class VanguardEdge : BaseSkill
{
    float ReflectValue;
    int ReflectDuration;
    public override void UpgradeSkill_Effect()
    {
        switch(Level)
        {
            case 1:
                ReflectValue = 0.4f;
                ReflectDuration = 1;
                break;

            case 2:
                ReflectValue = 0.45f;
                ReflectDuration = 1;
                break;

            case 3:
                ReflectValue = 0.5f;
                ReflectDuration = 2;
                break;

            case 4:
                ReflectValue = 0.55f;
                ReflectDuration = 2;
                break;

            case 5:
                ReflectValue = 0.60f;
                ReflectDuration = 3;
                break;

            default:
                break;
        }
    }

    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        currentUnit.AddBuff(ReflectDamage_Buff(currentUnit, ReflectDuration));
    }

    public Buff ReflectDamage_Buff(FightingUnit currentUnit,int duration)
    {
        Buff ReflectDamage = new Buff();

        ReflectDamage.type = Buff.Type.ReflectDamage;
        ReflectDamage.duration = duration;

        ReflectDamage.SetIcon();

        ReflectDamage.Activation = () =>
        {
            currentUnit.Unit_BeAttacked += this.ReflectDamage;
        };

        ReflectDamage.Deactivation = () =>
        {
            currentUnit.Unit_BeAttacked -= this.ReflectDamage;
        };

        return ReflectDamage;
    }
    public void ReflectDamage(FightingUnit currentUnit, FightingUnit targetUnit, Attack currentUnitAttack, Defense targetUnitDefense)
    {
        float DamageCause = currentUnitAttack.DamageCause * ReflectValue;
        targetUnit.OnlyTakenDamage(DamageCause,0);
    }
}
