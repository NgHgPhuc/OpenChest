using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public List<FightingUnit> MyTeam = new List<FightingUnit>();
    public List<FightingUnit> EnemyTeam = new List<FightingUnit>();

    FightingUnit currentUnit;

    public static TurnManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    public void SetTeam(List<FightingUnit> MyTeam,List<FightingUnit> EnemyTeam)
    {
        this.MyTeam = MyTeam;
        this.EnemyTeam = EnemyTeam;
    }


    public void AttackOneEnemy(FightingUnit currentUnit, FightingUnit targetUnit)
    {
        this.currentUnit = currentUnit;
        Attack currentUnitAttack = currentUnit.attack();
        Defense targetUnitDefense = targetUnit.defense();
        float targetGetDamage = currentUnitAttack.DamageCause * targetUnitDefense.TakenDmgPercent;

        if (targetUnitDefense.IsDogde)
        {
            targetUnit.DogdeUI();
            EndCurrentTurn();
            return;
        }

        targetUnit.BeingAttacked(targetGetDamage);
        currentUnit.LifeSteal(targetGetDamage);

        if (targetUnit.stateFighting == FightingUnit.StateFighting.Death)
        {
            EndCurrentTurn();
            return;
        }

        if(currentUnitAttack.IsStun)
        {
            targetUnit.StunUI();
            EndCurrentTurn();
            return;
        }

        if(targetUnitDefense.IsCounter)
        {
            Attack targetUnitAttack = targetUnit.attack();
            Defense currentUnitDefense = currentUnit.defense();
            float tarGetDmg = currentUnitAttack.DamageCause * targetUnitDefense.TakenDmgPercent;

            if (targetUnitDefense.IsDogde)
            {
                targetUnit.DogdeUI();
                EndCurrentTurn();
                return;
            }

            currentUnit.BeingAttacked(tarGetDmg);
            //currentUnit.LifeSteal(tarGetDmg);
            //Counter will be without lifesteal

            if (targetUnit.stateFighting == FightingUnit.StateFighting.Death)
            {
                EndCurrentTurn();
                return;
            }
        }

        EndCurrentTurn();

    }

    public void UsingSkillDamage(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        this.currentUnit = currentUnit;
        currentUnit.character.skill[0].UsingSkill(currentUnit, ChosenUnit);
        EndCurrentTurn();
    }

    void EndCurrentTurn()
    {
        Invoke("EndTurnUI", 1f);
    }

    void EndTurnUI()
    {
        this.currentUnit.EndMyTurn();
        FightManager.Instance.Endturn();
    }
}
