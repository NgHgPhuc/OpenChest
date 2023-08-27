using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public Dictionary<string, BaseSkill> skills = new Dictionary<string, BaseSkill>();

    public static SkillManager Instance { get; private set; }
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

        loadAllSkill();
    }

    void loadAllSkill()
    {
        List<BaseSkill> bSs = new List<BaseSkill>(Resources.LoadAll<BaseSkill>("Skill/"));
        foreach (BaseSkill s in bSs)
            if (!skills.ContainsKey(s.Name))
                skills.Add(s.Name,s);
    }
}
