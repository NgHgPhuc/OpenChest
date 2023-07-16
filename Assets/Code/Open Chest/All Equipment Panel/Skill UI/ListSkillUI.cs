using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ListSkillUI : MonoBehaviour
{
    public Transform Contain;
    List<SkillSlot> listSkill = new List<SkillSlot>();

    void SetAttr()
    {
        if(listSkill.Count <= 0)
            for(int i = 0; i<Contain.childCount; i++)
            {
                Transform hor = Contain.GetChild(i);
                for(int j=0; j< hor.childCount; j++)
                    listSkill.Add(hor.GetChild(j).GetComponent<SkillSlot>());
            }
    }

    public void SetListSkillUI(Dictionary<string,BaseSkill> skillDict)
    {
        SetAttr();
        for (int i = 0; i < listSkill.Count; i++)
            if(i < skillDict.Count)
                listSkill[i].SetSkillInSlot(skillDict.ElementAt(i).Value);
            else
                listSkill[i].SetSkillInSlot(null);
    }
}
