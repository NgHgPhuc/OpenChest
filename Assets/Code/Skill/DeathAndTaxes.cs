using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Death And Taxes", menuName = "Skill/Death And Taxes")]
public class DeathAndTaxes : BaseSkill
{
    float DamageCause;
    float PetrenationArmor;

    float MaxHpThreshold;
    float MaxHpHealing;

    public override void UpgradeSkill_Effect()
    {
        switch (Level)
        {
            case 1:
                DamageCause = 2.5f;
                PetrenationArmor = 20f;

                MaxHpThreshold = 10;
                MaxHpHealing = 10;

                break;

            case 2:
                DamageCause = 2.75f;
                PetrenationArmor = 22.5f;

                MaxHpThreshold = 11;
                MaxHpHealing = 12.5f;

                break;

            case 3:
                DamageCause = 3f;
                PetrenationArmor = 25f;

                MaxHpThreshold = 12;
                MaxHpHealing = 15f;

                break;

            case 4:
                DamageCause = 3.25f;
                PetrenationArmor = 27.5f;

                MaxHpThreshold = 13;
                MaxHpHealing = 17.5f;

                break;

            case 5:
                DamageCause = 3.5f;
                PetrenationArmor = 30f;

                MaxHpThreshold = 14;
                MaxHpHealing = 20;


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
                Defense targetUnitDefense = targetUnit.defense(PetrenationArmor);//Armor Petrenation 20%
                targetUnitDefense.IsCounter = false;

                TurnManager.Instance.AttackOneEnemy(currentUnit, targetUnit, currentUnitAttack, targetUnitDefense);

                if (targetUnit.CurrentHP < targetUnit.GetPercentMaxHP(MaxHpThreshold))
                {
                    targetUnit.BeingAttacked(99999999);
                    currentUnit.Heal(currentUnit.GetPercentMaxHP(MaxHpHealing));
                    FightManager.Instance.AnotherActionInTurn();
                }

            }

    }
}
