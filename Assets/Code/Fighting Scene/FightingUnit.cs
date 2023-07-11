using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;
using Unity.VisualScripting.Antlr3.Runtime;
using System.Security.Cryptography;
using static FightingUnit;
using static UnityEngine.GraphicsBuffer;

public class Attack
{
    public FightingUnit Causer;
    public float DamageCause;
    public bool IsCrit = false;
    public bool IsStun = false;
}

public class Defense
{
    public float TakenDmgPercent;
    public bool IsDogde = false;
    public bool IsCounter = false;

}


public class FightingUnit : MonoBehaviour, IPointerClickHandler
{
    public Character character;
    public Character basicStatsCharacter;

    public enum StateData
    {
        None,
        NotHaveChamp,
        HaveChamp
    }
    public StateData stateData = StateData.NotHaveChamp;

    public enum Position
    {
        TopBack,
        MiddleFront,
        DownBack
    }
    public Position position;

    public enum Team
    {
        Player,
        Enemy
    }
    public Team team;

    public enum StateFighting
    {
        Alive,
        Death,
        Block,
    }
    public StateFighting stateFighting = StateFighting.Alive;

    public enum EffectFighting
    {
        None,
        Stunned
    }
    public EffectFighting effectFighting = EffectFighting.None;

    public int Range;

    public bool IsInTurn;

    public Animator animator;
    GameObject Shield;

    public bool IsTarget;
    public List<int> CoolDown;
    //UI
    Slider healthBar;
    TextMeshProUGUI IndexHealthBar;
    float MaxHP;
    public float CurrentHP;
    Image CharacterIcon;
    Image TargetSignal;
    Image InTurnSignal;
    Transform FloatingPoint;
    FloatingObject floatingObject;
    Transform EffectPanel;
    Dictionary<EffectPos, Transform> EffectDict = new Dictionary<EffectPos, Transform>();
    BuffOfUnit buffOfUnit;
    EffectIcon effectIcon;

    private void Awake()
    {
        if (healthBar == null)
        {
            healthBar = transform.Find("Health Bar").GetComponent<Slider>();
            IndexHealthBar = healthBar.transform.Find("Index").GetComponent<TextMeshProUGUI>();
        }

        if (TargetSignal == null)
            TargetSignal = transform.Find("Target Signal").GetComponent<Image>();
        TargetSignal.gameObject.SetActive(false);

        if (FloatingPoint == null)
            FloatingPoint = transform.Find("Floating Point");

        if (Shield == null)
            Shield = transform.Find("Shield Effect").gameObject;

        if (EffectPanel == null)
            EffectPanel = transform.Find("Effect Panel");

        if (EffectDict.Count == 0)
            for (int i = 0; i < EffectPanel.childCount; i++)
                EffectDict.Add((EffectPos)i, EffectPanel.GetChild(i));

        if (buffOfUnit == null)
            buffOfUnit = transform.Find("Buff Of Unit").GetComponent<BuffOfUnit>();

        if (effectIcon == null)
            effectIcon = transform.Find("Effect Icon").GetComponent<EffectIcon>();

        if (InTurnSignal == null)
            InTurnSignal = transform.Find("In Turn Signal").GetComponent<Image>();
        InTurnSignal.gameObject.SetActive(false);
    }



    //INSTANTIATE VALUE OF THIS
    public void Instantiate()
    {
        if (healthBar == null)
        {
            healthBar = transform.Find("Health Bar").GetComponent<Slider>();
            IndexHealthBar = healthBar.transform.Find("Index").GetComponent<TextMeshProUGUI>();
        }

        MaxHP = character.HealthPoint;
        CurrentHP = MaxHP;
        UpdateHealthBar();

        if (CharacterIcon == null)
            CharacterIcon = transform.Find("Character Icon").GetComponent<Image>();

        CharacterIcon.sprite = character.Icon;

        floatingObject = FightManager.Instance.floatingObject;

        try
        {
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animator/" + character.Name + "/" + character.Name);
        }
        catch (Exception)
        {

        }

        CoolDown = new List<int>(new int[this.character.skill.Count]);

        basicStatsCharacter = character.Clone();
    }

    void UpdateHealthBar()
    {
        IndexHealthBar.SetText(Math.Ceiling(CurrentHP) + "/" + MaxHP);
        healthBar.maxValue = MaxHP;
        healthBar.value = CurrentHP;
    }

    public void Floating(string msg, Color color)
    {

        FloatingObject fo = Instantiate(floatingObject, FloatingPoint.position, transform.rotation, transform);
        fo.Iniatialize(msg, color);
    }



    //SET UI OF TARGET BY fightManager.Instance
    void SetTargetSignal()
    {
        if (TargetSignal == null)
            TargetSignal = transform.Find("Target Signal").GetComponent<Image>();

        if (InTurnSignal == null)
            InTurnSignal = transform.Find("In Turn Signal").GetComponent<Image>();
    }
    public void IsTargetUI()
    {
        SetTargetSignal();

        TargetSignal.gameObject.SetActive(true);
        TargetSignal.color = Color.red;
        IsTarget = true;
    }
    public void UntargetUI()
    {
        SetTargetSignal();

        TargetSignal.gameObject.SetActive(false);
        TargetSignal.color = Color.white;
        IsTarget = false;
    }
    public void IsAllyUI()
    {
        SetTargetSignal();

        TargetSignal.gameObject.SetActive(true);
        TargetSignal.color = Color.green;
        IsTarget = true;
    }
    public void EndTurnUI()
    {
        SetTargetSignal();


        InTurnSignal.gameObject.SetActive(false);
    }
    public void InTurnUI()
    {
        SetTargetSignal();

        InTurnSignal.gameObject.SetActive(true);
    }
    public void AttackUI()
    {

    }
    public void BeingAttackUI()
    {
        this.CharacterIcon.color = Color.red;
        EffectManager.Instance.InitializeEffect(EffectDict[EffectPos.InMiddle], EffectManager.EffectName.BeingAttack, 0.5f);
        Invoke("EndBeingAttackUI", 1f);
    }
    public void EndBeingAttackUI()
    {
        this.CharacterIcon.color = Color.white;
    }
    public void CritUI()
    {
        Floating("Crit", Color.grey);
    }

    public void CounterUI()
    {

    }

    public void DogdeUI()
    {

    }

    public void StunUI()
    {

    }
    public void HealUI()
    {
        EffectManager.Instance.InitializeEffect(EffectDict[EffectPos.InMiddle], EffectManager.EffectName.Healing, 0.5f);
    }
    //Effect UI
    public void InitializeEffect(EffectPos effectPos,EffectManager.EffectName effectName, float LivingTime)
    {
        EffectManager.Instance.InitializeEffect(EffectDict[effectPos], effectName, LivingTime);
    }



    //ACTIVITY FLOW ===== ItsMyTurn -> Action -> EndMyTurn
    public void ItsMyTurn()
    {
        IsInTurn = true;
        InTurnUI();

        CheckBuff();
        ShowBuff();


        for (int i = 0; i < CoolDown.Count; i++)
            CoolDown[i] -= 1;

        if (stateFighting == StateFighting.Block)
        {
            stateFighting = StateFighting.Alive;
            Shield.SetActive(false);
            CharacterIcon.color = Color.white;
        }

        if (effectFighting == EffectFighting.Stunned)
        {
            effectFighting = EffectFighting.None;
            CharacterIcon.color = Color.white;
            EndMyTurn();
            Invoke("EndTurn",1f);
        }

    }

    public void EndMyTurn()
    {
        IsInTurn = false;
        EndTurnUI();

        animator.Play("Idle");
    }

    void EndTurn()
    {
        FightManager.Instance.Endturn();
    }


    //CALCULATE RECEIVE DAMAGE
    float CalculateDamage()
    {
        float TakenDamagePercent = 100 / (100 + this.character.DefensePoint);

        if (stateFighting == StateFighting.Block)
            TakenDamagePercent /= 2;

        return TakenDamagePercent;
    }


    //SKILL
    public void Skill(List<FightingUnit> ChosenUnit,int skillCount)
    {
        TurnManager.Instance.UsingSkillDamage(this,ChosenUnit, skillCount);
        this.CoolDown[skillCount] = this.character.skill[skillCount].Cooldown +1;
    }


    //BLOCK - Decrease 50% taken damage
    public void Block()
    {
        stateFighting = StateFighting.Block;
        Shield.SetActive(true);
        EndMyTurn();

        Invoke("EndTurn", 1f);
    }



    //ATTACK
    public Attack attack()
    {
        Attack attack = new Attack();

        attack.Causer = this;

        attack.DamageCause = this.character.AttackDamage;

        float CriticalRate = UnityEngine.Random.Range(0f, 100f);
        if (CriticalRate < character.PassiveList[BaseStats.Passive.CriticalChance])
        {
            attack.DamageCause *= (100 + character.PassiveList[BaseStats.Passive.CriticalDamage]) * 2 / 100;
            attack.IsCrit = true;
        }

        float StunRate = UnityEngine.Random.Range(0f, 100f);
        if (StunRate < this.character.PassiveList[BaseStats.Passive.Dodge])
            attack.IsStun = true;

        animator.Play("Attack");

        return attack;
    }

    //LIFESTEAL - recover % damage cause
    public void LifeSteal(float DamageCause)
    {
        float mount = DamageCause * this.character.PassiveList[BaseStats.Passive.LifeSteal] / 100;
        if (mount == 0)
            return;

        Heal(mount);
    }

    public void Heal(float mount)
    {
        CurrentHP += mount;
        if (CurrentHP > MaxHP)
            CurrentHP = MaxHP;

        UpdateHealthBar();
        HealUI();
        Floating(Math.Ceiling(mount).ToString(), Color.green);
    }
    public void UsingPercentHP(float percentHP)
    {
        float usingMount = percentHP * MaxHP/100;
        CurrentHP -= usingMount;
        if (CurrentHP <= 0)
            Death();

        UpdateHealthBar();
        HealUI();
        Floating(Math.Ceiling(usingMount).ToString(), Color.red);
    }



    //BEING ATTACKED - END BEING ATTACKED
    public Defense defense()
    {
        Defense defense = new Defense();
        defense.TakenDmgPercent = this.CalculateDamage();

        float CounterRate = UnityEngine.Random.Range(0f, 100f);
        if (CounterRate < character.PassiveList[BaseStats.Passive.CounterAttack])
            defense.IsCounter = true;

        float DogdeRate = UnityEngine.Random.Range(0f, 100f);
        if (DogdeRate < this.character.PassiveList[BaseStats.Passive.Dodge])
            defense.IsDogde = true;

        return defense;
    }


    public void BeingAttacked(float DamageTaken)
    {
        CurrentHP -= DamageTaken;

        Floating(Math.Ceiling(DamageTaken).ToString(), Color.red);

        if (CurrentHP <= 0)
        {
            Death();
            return;
        }

        BeingAttackUI();

        UpdateHealthBar();
    }


    //BUFF
    public void AddBuff(Buff buff)
    {
        buffOfUnit.AddBuff(buff);
        ShowBuff();
    }
    public void CheckBuff()
    {
        buffOfUnit.CheckBuff();
    }
    public void ShowBuff()
    {
        effectIcon.SetBuffIcon(buffOfUnit.buffs);
    }


    void Death()
    {
        this.stateFighting = StateFighting.Death;
        FightManager.Instance.TargerDie(this);
        gameObject.SetActive(false);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if(this.IsTarget == true)
            FightManager.Instance.ChooseTarget(this);
    }
}
