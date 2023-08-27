using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public AllySO AllySO;
    Character Player;
    public List<SkillSlot> skillSlotList;
    Dictionary<SkillSlot, BaseSkill> playerSkillDict = new Dictionary<SkillSlot, BaseSkill>();

    public static PlayerManager Instance { get; private set; }
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

        //SetSkill();
    }

    //public void SetSkill()
    //{
    //    foreach (SkillSlot ss in skillSlotList)
    //        playerSkillDict.Add(ss, ss.getSkill());
    //}
    //public void LoadSkillData(List<string> skillsNameData)
    //{
    //    for (int i = 0; i < 3; i++)
    //    {
    //        if (i < skillsNameData.Count)
    //        {
    //            BaseSkill bS = Resources.Load<BaseSkill>("Skill/" + skillsNameData[i]);
    //            skillSlotList[i].SetSkillInSlot(bS);
    //        }
    //        else
    //            skillSlotList[i].DontHaveSkillInSlot();
    //    }

    //    SetSkill();
    //}


    public void EquipSkillOfPlayer(SkillSlot currentSkillSlotChosen)
    {
        playerSkillDict[currentSkillSlotChosen] = currentSkillSlotChosen.getSkill();
        DataManager.Instance.SaveData(currentSkillSlotChosen.getSkill().Name, currentSkillSlotChosen.getSkill().ToStringData());
    }
    public void UnequipSkillOfPlayer(SkillSlot currentSkillSlotChosen)
    {
        currentSkillSlotChosen.getSkill().IsEquip = false;
        playerSkillDict[currentSkillSlotChosen] = null;
        DataManager.Instance.SaveData(currentSkillSlotChosen.getSkill().Name, currentSkillSlotChosen.getSkill().ToStringData());
    }

    //call in Stats Panel Manager
    public void SetStatsPlayer()
    {
        this.Player.AttackDamage = EquipmentManager.Instance.Stats[0];
        this.Player.HealthPoint = EquipmentManager.Instance.Stats[1];
        this.Player.DefensePoint = EquipmentManager.Instance.Stats[2];
        this.Player.Speed = EquipmentManager.Instance.Stats[3];

        for (int i = 0; i < EquipmentManager.Instance.Passives.Count; i++)
            this.Player.PassiveList[(Equipment.Passive)i + 1] = EquipmentManager.Instance.Passives[i];
    }

    public Character GetPlayer()
    {
        //if (Player.skill.Count == 0)
        //    for (int i = 0; i < 3; i++)
        //        Player.skill.Add(playerSkillDict.ElementAt(i).Value);
        //else
        //    for (int i = 0; i < 3; i++)
        //        Player.skill[i] = playerSkillDict.ElementAt(i).Value;

        if (Player == null)
            Player = AllySO.character;

        SetStatsPlayer();
        return Player;
    }
}
