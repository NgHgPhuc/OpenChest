using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vanguard Edge", menuName = "Skill/Vanguard Edge")]
public class VanguardEdge : BaseSkill
{
    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        currentUnit.AddBuff(ReflectDamage_Buff(currentUnit));
    }

    public Buff ReflectDamage_Buff(FightingUnit currentUnit)
    {
        Buff ReflectDamage = new Buff();

        ReflectDamage.type = Buff.Type.ReflectDamage;
        ReflectDamage.duration = 3;

        ReflectDamage.SetIcon();

        ReflectDamage.Activation = () =>
        {
            currentUnit.Unit_BeAttacked += ShareDamage;
        };

        ReflectDamage.Deactivation = () =>
        {
            currentUnit.Unit_BeAttacked -= ShareDamage;
        };

        ReflectDamage.Onactivation = () =>
        {
        };

        return ReflectDamage;
    }
    public void ShareDamage(FightingUnit currentUnit, FightingUnit targetUnit, Attack currentUnitAttack, Defense targetUnitDefense)
    {
        float DamageCause = currentUnitAttack.DamageCause * 0.4f;
        targetUnit.OnlyTakenDamage(DamageCause,0);
    }
}
