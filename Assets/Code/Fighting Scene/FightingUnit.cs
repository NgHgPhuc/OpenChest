using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;
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
        Block
    }
    public StateFighting stateFighting = StateFighting.Alive;

    public int Range;

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
    public void IsTargetUI()
    {
        if (TargetSignal == null)
            TargetSignal = transform.Find("Target Signal").GetComponent<Image>();

        TargetSignal.gameObject.SetActive(true);
        TargetSignal.color = Color.red;
    }
    public void IsAllyUI()
    {
        if(TargetSignal == null)
            TargetSignal = transform.Find("Target Signal").GetComponent<Image>();

        TargetSignal.gameObject.SetActive(true);
        TargetSignal.color = Color.green;
    }
    public void EndTurnUI()
    {
        if(TargetSignal == null)
            TargetSignal = transform.Find("Target Signal").GetComponent<Image>();

        TargetSignal.color = Color.white;
        TargetSignal.gameObject.SetActive(false);
    }
    public void InTurnUI()
    {
        if (TargetSignal == null)
            TargetSignal = transform.Find("Target Signal").GetComponent<Image>();

        TargetSignal.color = Color.yellow;
        TargetSignal.gameObject.SetActive(true);
    }
    public void UntargetUI()
    {
        if (TargetSignal == null)
            TargetSignal = transform.Find("Target Signal").GetComponent<Image>();

        TargetSignal.color = Color.white;
        TargetSignal.gameObject.SetActive(false);
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

    public void ItsMyTurn()
    {
        InTurnUI();
        if (stateFighting == StateFighting.Block)
        {
            stateFighting = StateFighting.Alive;
            Icon.color = Color.white;
        }
    }

    public void EndMyTurn()
    {
        EndTurnUI();
        FightManager.Instance.Endturn();
    }

    public void Block()
    {
        stateFighting = StateFighting.Block;
        Icon.color = Color.blue;
        EndMyTurn();
    }

    //ATTACK - END ATTACK
    public void Attack(FightingUnit target)
    {
        target.BeingAttacked(this.character.AttackDamage);
        Invoke("EndAttack", 1f);
    }

    public void EndAttack()
    {
        Icon.color = Color.white;
        EndMyTurn();
    }

    //BEING ATTACKED - END BEING ATTACKED
    public void BeingAttacked(float Damage)
    {
        float DamageReceive = CalculateDamage(Damage);
        CurrentHP -= DamageReceive;

        FloatingObject fo = Instantiate(floatingObject, FloatingPoint.position,transform.rotation,transform);
        fo.Iniatialize("-" + Math.Round(DamageReceive,1), Color.red);

        UpdateHealthBar();
        if (CurrentHP <= 0)
        {
            Death();
            return;
        }
        Icon.color = Color.red;
        Invoke("EndAttacked", 1f);
    }
    float CalculateDamage(float Damage)
    {
        float DamageReceive = Damage * 100 / (100 + this.character.DefensePoint);

        if (stateFighting == StateFighting.Block)
            DamageReceive /= 2;

        return DamageReceive;
    }
    public void EndAttacked()
    {
        Icon.color = Color.white;
    }

    //DEATH EFFECT
    void Death()
    {
        this.stateFighting = StateFighting.Death;
        FightManager.Instance.TargerDie(this);
        gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        FightManager.Instance.ChooseEnemy(this);
    }
}
