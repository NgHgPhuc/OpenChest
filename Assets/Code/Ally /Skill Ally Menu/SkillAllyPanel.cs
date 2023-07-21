using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAllyPanel : MonoBehaviour
{
    List<SkillAllyUI> skillAllyUIs = new List<SkillAllyUI>();

    void SetAttr()
    {
        if(skillAllyUIs.Count == 0)
            for(int i = 1; i <= 5; i++)
                skillAllyUIs.Add(transform.Find("Skill Panel " + i).GetComponent<SkillAllyUI>());
    }

    public void SetSkillList(List<BaseSkill> skills)
    {
        for (int i = 1; i <= 5; i++)
            if (i < skills.Count || skills[i] != null)
                skillAllyUIs[i - 1].ShowSkill(skills[i]);
            else
                skillAllyUIs[i - 1].ShowSkill(null);

    }
}
