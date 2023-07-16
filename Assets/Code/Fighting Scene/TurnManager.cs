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


    public void Attack(FightingUnit currentUnit, FightingUnit targetUnit)
    {
        this.currentUnit = currentUnit;

        Attack currentUnitAttack = currentUnit.attack();
        Defense targetUnitDefense = targetUnit.defense();

        AttackOneEnemy(currentUnit, targetUnit, currentUnitAttack, targetUnitDefense);

        EndCurrentTurn();
    }

    public void AttackOneEnemy(FightingUnit currentUnit, FightingUnit targetUnit,Attack currentUnitAttack,Defense targetUnitDefense)
    {
        if(currentUnitAttack.IsHaveEffect == true)
        this.currentUnit.Unit_Attack?.Invoke(currentUnit, targetUnit, currentUnitAttack, targetUnitDefense);

        if (targetUnitDefense.IsDogde)
        {
            targetUnit.DogdeUI();
            return;
        }

        if (targetUnitDefense.IsHaveEffect == true)
            targetUnit.Unit_BeAttacked?.Invoke(targetUnit, currentUnit, currentUnitAttack, targetUnitDefense);

        float targetGetDamage = currentUnitAttack.DamageCause * targetUnitDefense.TakenDmgPercent;


        targetUnit.BeingAttacked(targetGetDamage);

        currentUnit.LifeSteal(targetGetDamage);

        if (targetUnit.stateFighting == FightingUnit.StateFighting.Death)
            return;

        //if (currentUnitAttack.IsStun)
        //{
        //    targetUnit.StunUI();
        //    return;
        //}

        if (targetUnitDefense.IsCounter)
        {
            Attack targetUnitAttack = targetUnit.attack();
            Defense currentUnitDefense = currentUnit.defense();
            currentUnitDefense.IsCounter = false;
            AttackOneEnemy(targetUnit, currentUnit, targetUnitAttack, currentUnitDefense);
        }
    }

    public void UsingSkillDamage(FightingUnit currentUnit, List<FightingUnit> ChosenUnit,int skillCount)
    {
        this.currentUnit = currentUnit;
        currentUnit.character.skill[skillCount].UsingSkill(currentUnit, ChosenUnit);
        EndCurrentTurn();
    }

    public void SkillTimes(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
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
