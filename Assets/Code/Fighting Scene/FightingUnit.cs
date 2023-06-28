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

public class FightingUnit : MonoBehaviour, IPointerClickHandler
{
    public Character character;

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

    //UI
    Slider healthBar;
    TextMeshProUGUI IndexHealthBar;
    float MaxHP;
    public float CurrentHP;
    Image Icon;
    Image TargetSignal;
    Transform FloatingPoint;
    FloatingObject floatingObject;

    private void Awake()
    {
        if (healthBar == null)
        {
            healthBar = transform.Find("Health Bar").GetComponent<Slider>();
            IndexHealthBar = healthBar.transform.Find("Index").GetComponent<TextMeshProUGUI>();
        }

        if (Icon == null)
            Icon = transform.Find("Icon").GetComponent<Image>();

        if (TargetSignal == null)
            TargetSignal = transform.Find("Target Signal").GetComponent<Image>();
        TargetSignal.gameObject.SetActive(false);

        if (FloatingPoint == null)
            FloatingPoint = transform.Find("Floating Point");
    }



    //SET UI OF TARGET BY fightManager.Instance
    void SetTargetSignal()
    {
        if (TargetSignal == null)
            TargetSignal = transform.Find("Target Signal").GetComponent<Image>();
    }
    public void IsTargetUI()
    {
        SetTargetSignal();

        TargetSignal.gameObject.SetActive(true);
        TargetSignal.color = Color.red;
    }
    public void UntargetUI()
    {
        SetTargetSignal();

        TargetSignal.color = Color.white;
        TargetSignal.gameObject.SetActive(false);
    }
    public void IsAllyUI()
    {
        SetTargetSignal();

        TargetSignal.gameObject.SetActive(true);
        TargetSignal.color = Color.green;
    }
    public void EndTurnUI()
    {
        SetTargetSignal();

        TargetSignal.color = Color.white;
        TargetSignal.gameObject.SetActive(false);
    }
    public void InTurnUI()
    {
        SetTargetSignal();

        TargetSignal.color = Color.yellow;
        TargetSignal.gameObject.SetActive(true);
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

        if(Icon == null)
            Icon = transform.Find("Icon").GetComponent<Image>();

        Icon.sprite = character.Icon;

        floatingObject = FightManager.Instance.floatingObject;
    }

    void UpdateHealthBar()
    {
        IndexHealthBar.SetText(Math.Ceiling(CurrentHP) + "/" + MaxHP);
        healthBar.maxValue = MaxHP;
        healthBar.value = CurrentHP;
    }

    public void Floating(string msg,Color color)
    {

        FloatingObject fo = Instantiate(floatingObject, FloatingPoint.position, transform.rotation, transform);
        fo.Iniatialize(msg, color);
    }


    //ACTIVITY FLOW ===== ItsMyTurn -> Action -> EndMyTurn
    public void ItsMyTurn()
    {
        IsInTurn = true;
        InTurnUI();
        if (stateFighting == StateFighting.Block)
        {
            stateFighting = StateFighting.Alive;
            Icon.color = Color.white;
        }

        if (effectFighting == EffectFighting.Stunned)
        {
            effectFighting = EffectFighting.None;
            Icon.color = Color.white;
            EndMyTurn();
        }

    }

    public void EndMyTurn()
    {
        IsInTurn = false;
        EndTurnUI();
        FightManager.Instance.Endturn();
    }


    //CALCULATE RECEIVE DAMAGE
    float CalculateDamage(float Damage)
    {
        float DamageReceive = Damage * 100 / (100 + this.character.DefensePoint);

        if (stateFighting == StateFighting.Block)
            DamageReceive /= 2;

        return DamageReceive;
    }


    //BLOCK - Decrease 50% taken damage
    public void Block()
    {
        stateFighting = StateFighting.Block;
        Icon.color = Color.blue;
        EndMyTurn();
    }



    //ATTACK - END ATTACK
    public void Attack(FightingUnit target)
    {
        float DamageCause = this.character.AttackDamage;

        float CriticalRate = UnityEngine.Random.Range(0f, 100f);
        if (CriticalRate < character.PassiveList[BaseStats.Passive.CriticalChance])
        {
            DamageCause *= (100 + character.PassiveList[BaseStats.Passive.CriticalDamage]) * 2 / 100;
            Floating("Crit", Color.grey);
        }    

        float DogdeRate = UnityEngine.Random.Range(0f, 100f);
        if (DogdeRate < target.character.PassiveList[BaseStats.Passive.Dodge])
        {
            Invoke("EndMyTurn", 1f);
            target.Floating("Dogde", Color.grey);
            return;
        }

        float StunRate = UnityEngine.Random.Range(0f, 100f);
        if (StunRate < this.character.PassiveList[BaseStats.Passive.Dodge])
        {
            Floating("Stun", Color.grey);
            target.effectFighting = EffectFighting.Stunned;
        }

        target.BeingAttacked(this, DamageCause);
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

        UpdateHealthBar();

        Floating(Math.Ceiling(mount).ToString(), Color.green);
    }



    //BEING ATTACKED - END BEING ATTACKED
    public void BeingAttacked(FightingUnit Causer, float Damage)
    {

        float DamageReceive = CalculateDamage(Damage);
        CurrentHP -= DamageReceive;

        Causer.LifeSteal(DamageReceive);

        Floating(Math.Ceiling(DamageReceive).ToString(), Color.red);

        this.Icon.color = Color.red;

        UpdateHealthBar();

        if (CurrentHP <= 0)
        {
            Death(Causer);
            return;
        }

        IEnumerator coroutine = EndBeingAttack(Causer);
        StartCoroutine(coroutine);
    }

    private IEnumerator EndBeingAttack(FightingUnit Causer)
    {
        yield return new WaitForSeconds(1f);

        this.Icon.color = Color.white;

        if (this.effectFighting == EffectFighting.Stunned)
        {
            this.SendMessage("End Turn", Causer);
            Floating("Stuning", Color.grey);
            yield break;
        }

        float CounterRate = UnityEngine.Random.Range(0f, 100f);
        if (CounterRate < character.PassiveList[BaseStats.Passive.CounterAttack])
        {
            Floating("Counter", Color.grey);
            this.SendMessage("Counter", Causer);
            Attack(Causer);
            yield break;
        }

        if (IsInTurn == true)
            EndMyTurn();
        else
            this.SendMessage("End Turn", Causer);
    }



    // MESSAGE FLOW
    void SendMessage(string mes,FightingUnit to)
    {
        to.ReceiveMessage(mes, this);
    }

    void ReceiveMessage(string mes,FightingUnit from)
    {
        if (IsInTurn == false)
            return;

        if (mes == "Counter")
            return;

        if (mes == "End Turn" || mes == "Target Die")
            EndMyTurn();
    }



    //DEATH EFFECT
    void Death(FightingUnit Causer)
    {
        this.stateFighting = StateFighting.Death;
        FightManager.Instance.TargerDie(this);
        gameObject.SetActive(false);

        this.SendMessage("Target Die", Causer);
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        FightManager.Instance.ChooseEnemy(this);
    }
}
