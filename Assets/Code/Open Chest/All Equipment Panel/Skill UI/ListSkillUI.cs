using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ListSkillUI : MonoBehaviour
{
    public Transform Contain;
    List<SkillSlot> listSkill = new List<SkillSlot>();
    public Dictionary<string, SkillSlot> skillDict = new Dictionary<string, SkillSlot>();
    void SetAttr()
    {
        if (listSkill.Count <= 0)
            for (int i = 0; i < Contain.childCount; i++)
            {
                Transform hor = Contain.GetChild(i);
                for (int j = 0; j < hor.childCount; j++)
                    listSkill.Add(hor.GetChild(j).GetComponent<SkillSlot>());
            }
    }

    public void SetListSkillUI(List<BaseSkill> skills)
    {
        SetAttr();

        for (int i = 0; i < listSkill.Count; i++)
            if (i < skills.Count)
            {
                this.listSkill[i].SetSkillInSlot(skills[i]);
                if (!this.skillDict.ContainsKey(skills[i].Name))
                    this.skillDict.Add(skills[i].Name, this.listSkill[i]);
            }
            else
                listSkill[i].SetSkillInSlot(null);
    }
}