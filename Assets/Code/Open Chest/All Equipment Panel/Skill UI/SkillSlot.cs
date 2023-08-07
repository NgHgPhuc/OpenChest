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
    GameObject AddIcon;

    public BaseSkill skill;

    public bool IsSlotEquipment;
    public bool IsSlotInList;

    void SetAttr()
    {
        if (EquipmentImage == null)
            EquipmentImage = transform.Find("Equipment Image").GetComponent<Image>();

        if (EquipmentLevel == null)
            EquipmentLevel = transform.Find("Equipment Level").GetComponent<TextMeshProUGUI>();

        if (BackgroundSlot == null)
            BackgroundSlot = transform.Find("Background Slot").GetComponent<Image>();

        if (AddIcon == null)
            if (transform.Find("Add Icon") != null)
                AddIcon = transform.Find("Add Icon").gameObject;

        if (LockIcon == null)
            if (transform.Find("Lock Icon") != null)
                LockIcon = transform.Find("Lock Icon").gameObject;

        if (Border == null)
            Border = transform.Find("Border").gameObject;
    }

    void SetSkillUI(BaseSkill skill)
    {
        this.skill = skill;
        gameObject.SetActive(true);
        SetAttr();

        if (!skill.IsHave)
            EquipmentImage.color = new Color(142f / 255, 142f / 255, 142f / 255);
        else EquipmentImage.color = Color.white;

        if (LockIcon != null)
            LockIcon.SetActive(!skill.IsHave);

        SetBorderActive();

        if (AddIcon != null && this.skill != null)
            AddIcon.SetActive(false);

        this.EquipmentImage.sprite = skill.Icon;
    }
    public void GetSkillRandomUI(BaseSkill skill)
    {
        if (skill == null)
            gameObject.SetActive(false);
        else
            SetSkillUI(skill);
    }

    public void SetSkillInSlot(BaseSkill skill)
    {
        if (skill == null)
        {
            DontHaveSkillInSlot();
            return;
        }

        skill.IsEquip = true;
        SetSkillUI(skill);
    }

    public void SetBorderActive()
    {
        Border.SetActive(this.skill.IsEquip);
    }
    public BaseSkill getSkill()
    {
        return this.skill;
    }

    public void DontHaveSkillInSlot()
    {
        this.skill = null;
        gameObject.SetActive(true);
        SetAttr();
        EquipmentImage.color = new Color(142f / 255, 142f / 255, 142f / 255);

        if (AddIcon != null && this.skill == null)
            AddIcon.SetActive(true);

        Border.SetActive(false);

        this.EquipmentImage.sprite = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ListSkillSingleton.Instance.OpenPanel();
        ListSkillSingleton.Instance.ShowInfoOfSkill(this);
    }
}
