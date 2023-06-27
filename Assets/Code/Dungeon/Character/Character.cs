using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Equipment;

[System.Serializable]
public class Character : BaseStats
{
    public new Dictionary<BaseStats.Passive, float> PassiveList = new Dictionary<BaseStats.Passive, float>()
    {
        {Passive.Stun , 0f },
        {Passive.Dodge , 0f },
        {Passive.LifeSteal , 0f },
        {Passive.CounterAttack , 0f },
        {Passive.CriticalChance , 0f },
        {Passive.CriticalDamage , 0f },
    };

    public int Level;

    public Sprite Icon;
    public string Name;

    public int StarCount;

    public float CurrentExp;
    public float NeedExp;

    public int CurrentSharp;
    public int NeedSharp;

    public bool IsOwn;

    public bool IsInTeam;

    public Character Clone()
    {
        Character character = new Character();

        character.AttackDamage = this.AttackDamage;
        character.HealthPoint = this.HealthPoint;
        character.DefensePoint = this.DefensePoint;
        character.Speed = this.Speed;

        character.Level = this.Level;

        character.Icon = this.Icon;
        character.Name = this.Name;

        character.StarCount = this.StarCount;

        character.CurrentExp = this.CurrentExp;
        character.NeedExp = this.NeedExp;

        character.CurrentSharp = this.CurrentSharp;
        character.NeedSharp = this.NeedSharp;

        character.IsOwn = this.IsOwn;

        character.IsInTeam = this.IsInTeam;

        foreach (KeyValuePair<Passive, float> kvp in PassiveList)
            character.PassiveList[kvp.Key] = kvp.Value;
        character.PowerPoint = this.PowerPoint;

        return character;
    }

    public void Transcend()
    {
        if (CurrentSharp < NeedSharp)
            return;

        CurrentSharp -= NeedSharp;
        StarCount += 1;
    }

    public void LevelUp()
    {
        if (CurrentExp < NeedExp)
            return;

        CurrentExp -= NeedExp;
        Level += 1;
    }
}
