using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAllyPanel : MonoBehaviour
{
    List<SkillAllyUI> skillAllyUIs = new List<SkillAllyUI>();

    int skillIndex = 1; // dont add strikes and block
    int skillMax = 3;
    void SetAttr()
    {
        if(skillAllyUIs.Count == 0)
            for(int i = skillIndex; i <= skillMax; i++)
                skillAllyUIs.Add(transform.Find("Skill Panel " + (i + 2)).GetComponent<SkillAllyUI>());
    }

    public void SetSkillList(List<BaseSkill> skills)
    {
        SetAttr();

        for (int i = skillIndex-1; i <= skillMax-1; i++)
            if (i < skills.Count && skills[i] != null)
                skillAllyUIs[i].ShowSkill(skills[i]);
            else
                skillAllyUIs[i].ShowSkill(null);

    }
}
