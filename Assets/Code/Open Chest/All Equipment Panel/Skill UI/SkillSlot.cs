using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour,IPointerClickHandler
{
    Image BackgroundSlot;
    Image EquipmentImage;
    TextMeshProUGUI EquipmentLevel;
    GameObject Border;
    GameObject LockIcon;

    BaseSkill skill;

    public bool IsSlotEquipment;
    public bool IsSlotInList;

    void SetAttr()
    {
        if (EquipmentImage == null || EquipmentLevel == null || BackgroundSlot == null || Border == null || LockIcon == null)
        {
            EquipmentImage = transform.Find("Equipment Image").GetComponent<Image>();
            EquipmentLevel = transform.Find("Equipment Level").GetComponent<TextMeshProUGUI>();
            BackgroundSlot = transform.Find("Background Slot").GetComponent<Image>();
            Border = transform.Find("Border").gameObject;
            LockIcon = transform.Find("Lock Icon").gameObject;
        }
    }

    public void SetSkillInSlot(BaseSkill skill)
    {
        if (skill == null)
        {
            gameObject.SetActive(false);
            return;
        }

        this.skill = skill;
        gameObject.SetActive(true);
        SetAttr();

        if (!skill.IsHave)
            EquipmentImage.color = new Color(142f / 255, 142f / 255, 142f / 255);
        else EquipmentImage.color = Color.white;

        LockIcon.SetActive(!skill.IsHave);
        Border.SetActive(skill.IsEquip);

        this.EquipmentImage.sprite = skill.Icon;
    }
    public void SetBorderActive()
    {
        Border.SetActive(this.skill.IsEquip);
    }
    public BaseSkill getSkill()
    {
        return this.skill;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ListSkillSingleton.Instance.OpenPanel();
        ListSkillSingleton.Instance.ShowInfoOfSkill(this);
    }
}
