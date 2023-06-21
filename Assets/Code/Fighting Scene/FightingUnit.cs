using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightingUnit : MonoBehaviour
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
        Death
    }
    public StateFighting stateFighting;

    Slider healthBar;
    float MaxHP;
    float CurrentHP;
    Image Icon;
    private void Start()
    {
        healthBar = transform.Find("Health Bar").GetComponent<Slider>();
        Icon = transform.Find("Icon").GetComponent<Image>();

    }

    public void Instantiate()
    {
        if(healthBar == null)
            healthBar = transform.Find("Health Bar").GetComponent<Slider>();

        MaxHP = character.HealthPoint;
        CurrentHP = MaxHP;
        healthBar.maxValue = MaxHP;
        healthBar.value = CurrentHP;
    }
    public void Attack(FightingUnit target)
    {
        if (stateFighting == StateFighting.Death)
        {
            FightManager.Instance.Endturn();
            return;
        }
        target.BeingAttacked();
        Icon.color = Color.green;
        print(this.name + " -> " + target.name);
        Invoke("EndAttack", 1f);
    }
    public void EndAttack()
    {
        Icon.color = Color.white;
        FightManager.Instance.Endturn();
    }

    public void BeingAttacked()
    {
        CurrentHP -= 5;
        healthBar.value = CurrentHP;
        if (CurrentHP <= 0)
        {
            this.stateFighting = StateFighting.Death;
            FightManager.Instance.TargerDie(this.team);
            gameObject.SetActive(false);
            return;
        }
        Icon.color = Color.red;
        Invoke("EndAttacked", 1f);
    }

    public void EndAttacked()
    {
        Icon.color = Color.white;
    }
}
