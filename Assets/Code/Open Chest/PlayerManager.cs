using System.Collections;
using System.Collections.Generic;
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

        Player = AllySO.character;
        SetSkill();
    }

    public void SetSkill()
    {
        foreach (SkillSlot ss in skillSlotList)
            playerSkillDict.Add(ss, ss.getSkill());
    }
    public void LoadSkillData(List<string> skillsNameData)
    {
        for (int i = 0; i < 3; i++)
        {
            if (i < skillsNameData.Count)
            {
                BaseSkill bS = Resources.Load<BaseSkill>("Skill/" + skillsNameData[i]);
                skillSlotList[i].EquipSkill(bS);
            }
            else
                skillSlotList[i].DontHaveSkillInSlot();
        }

        SetSkill();
    }


    public void EquipSkillOfPlayer(SkillSlot currentSkillSlotChosen)
    {
        playerSkillDict[currentSkillSlotChosen] = currentSkillSlotChosen.getSkill();
        DataManager.Instance.SaveData(currentSkillSlotChosen.name, currentSkillSlotChosen.getSkill().Name);
    }
    public void UnequipSkillOfPlayer(SkillSlot currentSkillSlotChosen)
    {
        playerSkillDict[currentSkillSlotChosen] = null;
        DataManager.Instance.SaveData(currentSkillSlotChosen.name, "");
    }

    //call in Stats Panel Manager
    public void SetStatsPlayer(List<StatsPanel> Stats, List<StatsPanel> Passives)
    {
        this.Player.AttackDamage = Stats[0].GetValue();
        this.Player.HealthPoint = Stats[1].GetValue();
        this.Player.DefensePoint = Stats[2].GetValue();
        this.Player.Speed = Stats[3].GetValue();

        for (int i = 0; i < Passives.Count; i++)
            this.Player.PassiveList[(Equipment.Passive)i + 1] = Passives[i].GetValue();
    }

    public Character GetPlayer()
    {
        //if (Player.skill.Count == 0)
        //    for (int i = 0; i < 3; i++)
        //        Player.skill.Add(playerSkillDict.ElementAt(i).Value);
        //else
        //    for (int i = 0; i < 3; i++)
        //        Player.skill[i] = playerSkillDict.ElementAt(i).Value;
        return Player;
    }
}
