using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//use in fight unit - return a skill
[Serializable]
public abstract class BaseSkill : ScriptableObject
{
    public string Name;
    public int Cooldown;
    public Sprite Icon;
    public int Level = 1;

    [TextAreaAttribute]
    public string Description;

    [TextAreaAttribute]
    public List<string> SkillLevelEffect;

    public enum Range
    {
        OnMySelf,
        //OnAllEnemy,
        OnEnemy,
        OnEnemyTeam,
        OnAlly,
        OnAllyTeam,
    }
    public Range range;

    public enum SkillType
    {
        Passive,
        Active
    }
    public SkillType skillType = SkillType.Active;

    public enum UserType
    {
        Player,
        Ally
    }
    public UserType userType;

    public int CurrentSharp;
    public int NeedSharp;
    public bool IsHave;
    public bool IsEquip;
    public int SlotEquipIndex;

    private void Awake()
    {
        SetAttr();
    }
    public void SetAttr()
    {
        UpgradeSkill_Effect();//set attr    
    }

    public string ToStringData()
    {
        string data = "CurrentSharp:" + CurrentSharp + "-" +
                      "Level:" + Level + "-" +
                      "IsHave:" + IsHave + "-" +
                      "IsEquip:" + IsEquip + "-" +
                      "SlotEquipIndex:" + SlotEquipIndex;

        return data;
    }
    public void ExtractString(string s)
    {
        List<string> dataList = new List<string>(s.Split("-"));

        CurrentSharp = Convert.ToInt32(dataList[0].Split(":")[1]);
        Level = Convert.ToInt16(dataList[1].Split(":")[1]);
        IsHave = Convert.ToBoolean(dataList[2].Split(":")[1]);
        IsEquip = Convert.ToBoolean(dataList[3].Split(":")[1]);
        SlotEquipIndex = Convert.ToInt16(dataList[4].Split(":")[1]);
    }
    public abstract void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit);

    bool CheckEnoughSharp()
    {
        if (CurrentSharp >= NeedSharp)
            return true;
        return false;
    }
    public void UpgradeSkill()
    {
        if (CheckEnoughSharp())
            UpgradeSkill_Effect();

    }
    public abstract void UpgradeSkill_Effect();
}
