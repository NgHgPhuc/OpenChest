using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonSkillUI : MonoBehaviour
{
    Image SkillIcon;
    Image CooldownImage;
    TextMeshProUGUI CooldownText;
    TextMeshProUGUI SkillName;
    BaseSkill skill;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetAttr()
    {
        if (CooldownImage != null && CooldownText != null && SkillName != null)
            return;

        SkillIcon = transform.Find("Icon Button").GetComponent<Image>();
        CooldownImage = transform.Find("Cooldown Image").GetComponent<Image>();
        CooldownText = CooldownImage.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        SkillName = transform.Find("Button Name").GetComponent<TextMeshProUGUI>();
    }

    public void SetSkill(BaseSkill skill,int Cooldown)
    {
        if (skill == null)
        {
            gameObject.SetActive(false);
            return;
        }

        SetAttr();

        gameObject.SetActive(true);

        this.skill = skill;
        SkillIcon.sprite = this.skill.Icon;
        SkillName.SetText(this.skill.Name);

        SetCooldown(skill.Cooldown,Cooldown);

    }

    public void SetCooldown(int SkillCooldown,int CooldownRemain)
    {
        if(CooldownRemain > 0)
        {
            CooldownImage.gameObject.SetActive(true);
            CooldownText.SetText(CooldownRemain.ToString());
            CooldownImage.fillAmount = (float)CooldownRemain / SkillCooldown;
        }
        else
        {
            CooldownImage.gameObject.SetActive(false);
        }
    }

}
