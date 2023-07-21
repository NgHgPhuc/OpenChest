using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillAllyUI : MonoBehaviour
{
    BaseSkill skill;

    TextMeshProUGUI SkillName;
    TextMeshProUGUI SkillCD;
    TextMeshProUGUI SkillDescription;
    Image SkillImage;

    Button SkillUpButton;

    public void SetAttr()
    {
        if(SkillName == null)
            SkillName = transform.Find("Skill Name").GetComponent<TextMeshProUGUI>();

        if (SkillCD == null)
            SkillCD = transform.Find("Skill CD").GetComponent<TextMeshProUGUI>();

        if (SkillDescription == null)
            SkillDescription = transform.Find("Skill Description").GetComponent<TextMeshProUGUI>();

        if (SkillImage == null)
            SkillImage = transform.Find("Skill Icon").GetComponentInChildren<Image>();

        if (SkillUpButton == null)
        {
            SkillUpButton = transform.Find("Skill Up Button").GetComponent<Button>();
            SkillUpButton.onClick.AddListener(UpgradeSkill);
        }
    }

    public void ShowSkill(BaseSkill skill)
    {
        if(skill == null)
        {
            this.skill = null;
            gameObject.SetActive(false);
            return;
        }

        this.skill = skill;
        SkillName.SetText(this.skill.Name);
        SkillCD.SetText("CD: " + this.skill.Cooldown + " Turns");
        SkillDescription.SetText(this.skill.Description);
        SkillImage.sprite = skill.Icon;

    }

    void UpgradeSkill()
    {
        if (this.skill == null)
            return;

        print("Skill Upgraded");
    }
}
