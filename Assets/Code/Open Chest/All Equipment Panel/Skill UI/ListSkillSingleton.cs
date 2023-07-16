using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class ListSkillSingleton : MonoBehaviour
{
    Dictionary<string, BaseSkill> skills = new Dictionary<string, BaseSkill>();
    public GameObject BlurBackground;
    public GameObject listSkillObject;

    public ListSkillUI listSkillUI;//show all skill
    public InfoSkillUI infoSkillUI;

    bool IsOpen = false;

    public static ListSkillSingleton Instance { get; private set; }
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

        GetAllSkill();
    }

    public void GetAllSkill()
    {
        if (skills.Count > 0)
            return;

        List<BaseSkill> skillList = new List<BaseSkill>(Resources.LoadAll<BaseSkill>("Skill"));
        foreach (BaseSkill s in skillList)
            skills.Add(s.Name, s);

    }

    public void ShowAllSkill()
    {
        listSkillUI.SetListSkillUI(skills);
    }

    public BaseSkill getSkill(string name)
    {
        return skills[name];
    }

    public void OpenPanel()
    {
        if (IsOpen == true)
            return;

        IsOpen = true;
        BlurBackground.SetActive(true);
        listSkillObject.gameObject.SetActive(true);

        GetAllSkill();
        ShowAllSkill();
    }

    public void ShowInfoOfSkill(SkillSlot skillSlot)
    {
        infoSkillUI.ShowInfomationSkill(skillSlot);
    }

    public void ClickBlurBG()
    {
        BlurBackground.SetActive(false);
        listSkillObject.gameObject.SetActive(false);
        IsOpen = false;
    }
}
