using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ButtonSkillUI : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    Image Border;
    Image SkillIcon;
    Image CooldownImage;
    TextMeshProUGUI CooldownText;
    TextMeshProUGUI SkillName;
    BaseSkill skill;

    float showInfoTime = 0.5f;
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

        Border = transform.Find("Border").GetComponent<Image>();
        SkillIcon = Border.transform.Find("Icon Button").GetComponent<Image>();
        CooldownImage = Border.transform.Find("Cooldown Image").GetComponent<Image>();
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

        if (skill.skillType == BaseSkill.SkillType.Passive)
            SkillPassiveUI();
        if (skill.skillType == BaseSkill.SkillType.Active)
            SkillActiveUI();

    }
    void SkillActiveUI()
    {
        Border.enabled = false;
    }
    void SkillPassiveUI()
    {
        Border.enabled = true;
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

    public void OnPointerDown(PointerEventData eventData)
    {
        Invoke("ShowInfo", showInfoTime);
    }

    void ShowInfo()
    {
        InfomationFighting.Instance.Initialize(this.skill, this.transform.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CancelInvoke("ShowInfo");
    }
}
