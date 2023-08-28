using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public List<FightingUnit> MyTeam = new List<FightingUnit>();
    public List<FightingUnit> EnemyTeam = new List<FightingUnit>();

    FightingUnit currentUnit;
    FightingUnit targetUnit;

    bool IsDoneTurn;
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
        this.targetUnit = targetUnit;

        if (this.currentUnit.animator.runtimeAnimatorController == null)
        {
            Attack currentUnitAttack = currentUnit.attack();
            Defense targetUnitDefense = targetUnit.defense();
            AttackOneEnemy(this.currentUnit, this.targetUnit, currentUnitAttack, targetUnitDefense);
            EndCurrentTurn();
        }
        else
            this.currentUnit.animator.Play("Attack " + this.currentUnit.CharacterClone.Name);
    }

    public void AttackInAnimation(int Times = 1)
    {
        Attack currentUnitAttack = currentUnit.attack();
        currentUnitAttack.DamageCause /= Times;
        Defense targetUnitDefense = targetUnit.defense();
        AttackOneEnemy(this.currentUnit, this.targetUnit, currentUnitAttack, targetUnitDefense);
    }

    public void AttackOneEnemy(FightingUnit currentUnit, FightingUnit targetUnit,Attack currentUnitAttack,Defense targetUnitDefense)
    {
        if (currentUnitAttack.IsHaveEffect == true)
            currentUnit.Unit_Attack?.Invoke(currentUnit, targetUnit, currentUnitAttack, targetUnitDefense);

        if (targetUnitDefense.IsDodge)
        {
            targetUnit.DodgeUI();
            return;
        }

        if (targetUnitDefense.IsHaveEffect == true)
            targetUnit.Unit_BeAttacked?.Invoke(targetUnit, currentUnit, currentUnitAttack, targetUnitDefense);

        float targetGetDamage = currentUnitAttack.DamageCause * targetUnitDefense.TakenDmgPercent;

        targetUnit.BeingAttacked(targetGetDamage);

        currentUnit.LifeSteal(targetGetDamage);

        if (targetUnit.stateFighting == FightingUnit.StateFighting.Death)
            return;

        //if (targetUnitDefense.IsCounter)
        //{
        //    Attack targetUnitAttack = targetUnit.attack();
        //    Defense currentUnitDefense = currentUnit.defense();
        //    currentUnitDefense.IsCounter = false;
        //    AttackOneEnemy(targetUnit, currentUnit, targetUnitAttack, currentUnitDefense);
        //}
    }

    public void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit,int skillCount)
    {
        this.currentUnit = currentUnit;
        currentUnit.CharacterClone.skills[skillCount].UsingSkill(currentUnit, ChosenUnit);
        EndCurrentTurn();
    }

    //Use attack much times - in baseskill dont have monobehaviour so cant use Start Coroutine
    public void SkillTimes(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

    public void EndCurrentTurn()
    {
        if (this.currentUnit.IsActioned == true)
            Invoke("EndTurnUI", 1f / FightManager.Instance.GameSpeed);
        else
            FightManager.Instance.AnotherTurn();
    }

    void EndTurnUI()
    {
        this.currentUnit.EndMyTurn();
        FightManager.Instance.Endturn();
    }
}
