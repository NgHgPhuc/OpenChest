using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static Equipment;
using System.Linq;

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

    Dictionary<Tier, Color> colors = new Dictionary<Tier, Color>()
    {
        { Tier.Common,new Color(240f/255, 240f/255, 240f/255) },
        { Tier.Rare, new Color(71f / 255, 160f / 255, 241f / 255)},
        { Tier.Epic, new Color(255f / 255, 81f / 255, 222f / 255)},
        { Tier.Legendary, new Color(255f / 255, 199f / 255, 69f / 255)},
    };

    public enum Role
    {
        Assassin,
        Fighter,
        Mage,
        Support,
        Tanker
    }
    public Role role;

    //override
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

    public Sprite Avatar;
    public Sprite Icon;
    public string Name;

    public int currentStarCount;
    public int maxStarCount;

    public List<string> DescriptionTranscendLevel = new List<string>();

    public float CurrentExp;
    public float NeedExp;

    public int CurrentSharp;
    public int NeedSharp;

    public bool IsOwn;

    public bool IsInTeam;
    public int PositionInTeam;

    public List<BaseSkill> skill = new List<BaseSkill>();

    public Color GetColor()
    {
        return colors[tier];
    }

    public Character Clone()
    {
        if (this == null)
            return null;

        Character character = new Character();

        character.AttackDamage = this.AttackDamage;
        character.HealthPoint = this.HealthPoint;
        character.DefensePoint = this.DefensePoint;
        character.Speed = this.Speed;

        character.Level = this.Level;
        character.CurrentExp = this.CurrentExp;
        character.NeedExp = this.NeedExp;

        character.tier = this.tier;

        character.Icon = this.Icon;
        character.Avatar = this.Avatar;
        character.Name = this.Name;

        character.currentStarCount = this.currentStarCount;
        character.maxStarCount = this.maxStarCount;

        character.CurrentSharp = this.CurrentSharp;
        character.NeedSharp = this.NeedSharp;

        character.IsOwn = this.IsOwn;

        character.IsInTeam = this.IsInTeam;

        foreach (KeyValuePair<Passive, float> kvp in PassiveList)
            character.PassiveList[kvp.Key] = kvp.Value;
        character.PowerPoint = this.PowerPoint;

        character.skill = new List<BaseSkill>(this.skill);

        character.DescriptionTranscendLevel = new List<string>(this.DescriptionTranscendLevel);

        return character;
    }

    bool CanTranscend()
    {
        if (CurrentSharp < NeedSharp)
            return false;

        if (currentStarCount >= 5)
            return false;

        return true;
    }
    public void Transcend()
    {
        if (!CanTranscend())
            return;

        CurrentSharp -= NeedSharp;
        currentStarCount += 1;
        TranscendStats();
    }
    public void TranscendStats()
    {
        float Delta = (float)Math.Pow(1 + ((int)this.tier + currentStarCount - 1) / 10f, Level - 1);

        //stat increase by star in levelstats
        this.AttackDamage = this.AttackDamage / Delta;
        this.HealthPoint = this.HealthPoint / Delta;
        this.DefensePoint = this.DefensePoint / Delta;
        //this.Speed *= 1.5f;

        if (Level > 1)
            LevelStats(Level - 1);
    }

    public void AddCurrentExp(float mount)
    {
        CurrentExp += mount;
        if (CheckCanLevelUp())
            LevelUp();
    }

    public void AddCurrentExp(float mount,Action e)
    {
        CurrentExp += mount;
        if (CheckCanLevelUp())
        {
            LevelUp();
            e?.Invoke();
        }
    }

    public bool CheckCanLevelUp()
    {
        if (CurrentExp < NeedExp)
            return false;
        return true;
    }
    public void LevelUp()
    {
        int LevelUpMount = (int)Math.Floor(Math.Log10( (CurrentExp * 0.2) / NeedExp + 1) / Math.Log10(1.2));

        CurrentExp -= (float)(NeedExp*(Math.Pow(1.2f,LevelUpMount) - 1) / 0.2f);
        NeedExp *= (float)Math.Pow(1.2f, LevelUpMount);
        Level += LevelUpMount;
        LevelStats(LevelUpMount);
    }

    void CalculatorNeedExp(int LevelUpMount)
    {
        NeedExp *= (float)Math.Pow(1.2f, LevelUpMount);
    }

    void LevelStats(int levelMount)
    {
        this.AttackDamage   *=  (float)Math.Pow(1 + ((int)this.tier + 1 + currentStarCount) / 10f, levelMount);
        this.HealthPoint    *=  (float)Math.Pow(1 + ((int)this.tier + 1 + currentStarCount) / 10f, levelMount);
        this.DefensePoint   *=  (float)Math.Pow(1 + ((int)this.tier + 1 + currentStarCount) / 10f, levelMount);
    }


    //public string ToStringData()
    //{
    //    string Stat = AttackDamage + "-" + HealthPoint + "-" + DefensePoint + "-" + Speed + "-" + (int)tier + "-"
    //                + Level + "-" + Name + "-" + currentStarCount + "-" + CurrentExp + "-" + NeedExp + "-"
    //                + CurrentSharp + "-" + NeedSharp + "-" + IsOwn + "-" + IsInTeam + "-"
    //                + PassiveList[BaseStats.Passive.Stun] + "-" + PassiveList[BaseStats.Passive.Dodge] + "-"
    //                + PassiveList[BaseStats.Passive.LifeSteal] + "-" + PassiveList[BaseStats.Passive.CounterAttack] + "-"
    //                + PassiveList[BaseStats.Passive.CriticalChance] + "-" + PassiveList[BaseStats.Passive.CriticalDamage];

    //    return Stat;
    //}

    //public Character ExtractStringData(string DataReceive)
    //{
    //    List<string> dataList = new List<string>(DataReceive.Split("-"));

    //    this.AttackDamage = (float)Convert.ToDouble(dataList[0]);
    //    this.HealthPoint = (float)Convert.ToDouble(dataList[1]);
    //    this.DefensePoint = (float)Convert.ToDouble(dataList[2]);
    //    this.Speed = (float)Convert.ToDouble(dataList[3]);
    //    //this.tier = (Tier)(Convert.ToInt16(dataList[4]));
    //    this.Level = Convert.ToInt16(dataList[5]);
    //    //this.Name = dataList[6];
    //    this.currentStarCount = Convert.ToInt32(dataList[7]);
    //    this.CurrentExp = (float)Convert.ToDouble(dataList[8]);
    //    this.NeedExp = (float)Convert.ToDouble(dataList[9]);
    //    this.CurrentSharp = Convert.ToInt32(dataList[10]);
    //    this.NeedSharp = Convert.ToInt32(dataList[11]);
    //    this.IsOwn = Convert.ToBoolean(dataList[12]);
    //    this.IsInTeam = Convert.ToBoolean(dataList[13]);

    //    for (int j = 1; j < 7; j++)
    //        PassiveList[(BaseStats.Passive)j] = (float)Convert.ToDouble(dataList[13 + j]);

    //    return this;
    //}

    public string ToStringData()
    {
        string Stat = "current Star Count:" + currentStarCount + "-"
                    + "Level:" + Level + "-"
                    + "Current Exp:" + CurrentExp + "-"
                    + "Current Sharp:" + CurrentSharp + "-"
                    + "Need Sharp:" + NeedSharp + "-"
                    + "Is Own:" + IsOwn + "-"
                    + "Is In Team:" + IsInTeam;

        return Stat;
    }

    public Character ExtractStringData(string DataReceive)
    {
        List<string> dataList = new List<string>(DataReceive.Split("-"));

        this.currentStarCount = Convert.ToInt32(dataList[0].Split(":")[1]);

        this.Level = Convert.ToInt16(dataList[1].Split(":")[1]);
        int LevelUpMount = this.Level - 1;
        LevelStats(LevelUpMount);
        this.CurrentExp = (float)Convert.ToDouble(dataList[2].Split(":")[1]);

        CalculatorNeedExp(LevelUpMount);

        this.CurrentSharp = Convert.ToInt32(dataList[3].Split(":")[1]);
        this.NeedSharp = Convert.ToInt32(dataList[4].Split(":")[1]);
        this.IsOwn = Convert.ToBoolean(dataList[5].Split(":")[1]);
        this.IsInTeam = Convert.ToBoolean(dataList[6].Split(":")[1]);

        return this;
    }
}
