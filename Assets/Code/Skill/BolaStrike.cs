using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "Bola Strike", menuName = "Skill/Bola Strike")]
public class BolaStrike : BaseSkill
{

    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        Attack currentUnitAttack = currentUnit.attack();

        currentUnitAttack.DamageCause *= 1.2f;

        foreach (FightingUnit targetUnit in ChosenUnit)
            if (targetUnit.stateFighting != FightingUnit.StateFighting.Death)
            {
                Defense targetUnitDefense = targetUnit.defense();
                targetUnitDefense.IsCounter = false;

                Transform FromPos = currentUnit.GetIconImage().transform;
                Transform ToPos = targetUnit.GetIconImage().transform;
                float width = currentUnit.GetIconImage().rectTransform.rect.width;
                float height = currentUnit.GetIconImage().rectTransform.rect.height / 2;
                Debug.Log("Inside Skill:" + FromPos.position + "->" + ToPos.position);
                EffectManager.Instance.InitializeShooting(FromPos, ToPos, EffectManager.EffectName.BolaStrikes, 3f, width, height);

                TurnManager.Instance.AttackOneEnemy(currentUnit, targetUnit, currentUnitAttack, targetUnitDefense);

                float SlowRate = UnityEngine.Random.Range(0f, 100f);
                if (SlowRate < 40)
                    InitBuff(currentUnit, targetUnit);
            }
    }

    public void InitBuff(FightingUnit currentUnit, FightingUnit ChosenUnit)
    {
        Buff Slowing = new Buff();

        Slowing.type = Buff.Type.DecreaseSpeed;
        Slowing.duration = 1;

        Slowing.SetIcon();

        Slowing.ValueChange = ChosenUnit.basicStatsCharacter.Speed * 0.3f;

        Slowing.Activation = () =>
        {
            ChosenUnit.CharacterClone.Speed -= Slowing.ValueChange;
            FightManager.Instance.SortSpeed();
        };

        Slowing.Deactivation = () =>
        {
            ChosenUnit.CharacterClone.Speed += Slowing.ValueChange;
            FightManager.Instance.SortSpeed();
        };

        Slowing.Onactivation = () =>
        {
        };


        ChosenUnit.AddBuff(Slowing);
    }
}

