using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static Equipment;

[System.Serializable]
public class Character : BaseStats
{
    public enum Tier
    {
        Common,
        Rare,
        Epic,
        Legendary,
    }
    public Tier tier;

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

        character.tier = this.tier;

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

        if (StarCount >= 5)
            return;

        CurrentSharp -= NeedSharp;
        StarCount += 1;
        TranscendStats();
    }
    public void TranscendStats()
    {
        if (Level == 1)
            return;

        float Delta = (float)Math.Pow(1 + ((int)this.tier + StarCount - 1) / 10f, Level - 1);

        this.AttackDamage = this.AttackDamage / Delta;
        this.HealthPoint = this.HealthPoint / Delta;
        this.DefensePoint = this.DefensePoint / Delta;
        this.Speed *= 1.5f;

        LevelStats(Level - 1);
    }

    public void LevelUp()
    {
        if (CurrentExp < NeedExp)
            return;

        int LevelUpMount = (int)Math.Floor(Math.Log10( (CurrentExp * 0.2) / NeedExp + 1) / Math.Log10(1.2));

        CurrentExp -= (float)(NeedExp*(Math.Pow(1.2f,LevelUpMount) - 1) / 0.2f);
        NeedExp *= (float)Math.Pow(1.2f, LevelUpMount);
        Level += LevelUpMount;
        LevelStats(LevelUpMount);
    }

    void LevelStats(int levelMount)
    {

        this.AttackDamage   *=  (float)Math.Pow(1 + ((int)this.tier + 1 + StarCount) / 10f, levelMount);
        this.HealthPoint    *=  (float)Math.Pow(1 + ((int)this.tier + 1 + StarCount) / 10f, levelMount);
        this.DefensePoint   *=  (float)Math.Pow(1 + ((int)this.tier + 1 + StarCount) / 10f, levelMount);
    }


}
