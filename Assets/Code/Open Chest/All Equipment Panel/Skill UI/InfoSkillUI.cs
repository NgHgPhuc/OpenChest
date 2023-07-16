using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoSkillUI : MonoBehaviour
{
    public SkillSlot skillSlot;
    BaseSkill skill;
    SkillSlot chosenSkillInList; // for sync

    public TextMeshProUGUI SkillName;
    public TextMeshProUGUI Cooldown;
    public TextMeshProUGUI Description;

    public Button EquipButton;
    public Button UnequipButton;
    public Button UnlockButton;
    public Button UpgradeButton;

    private void Start()
    {
        EquipButton.onClick.AddListener(Equip_ButtonFunc);
        UnequipButton.onClick.AddListener(Unequip_ButtonFunc);
        UnlockButton.onClick.AddListener(Unlock_ButtonFunc);
        UpgradeButton.onClick.AddListener(Upgrade_ButtonFunc);
    }

    void Equip_ButtonFunc()
    {
        this.skill.IsEquip = true;
        EquipButton.gameObject.SetActive(!this.skill.IsEquip);
        UnequipButton.gameObject.SetActive(this.skill.IsEquip);
        skillSlot.SetBorderActive();
        chosenSkillInList.SetBorderActive();
    }
    void Unequip_ButtonFunc()
    {
        this.skill.IsEquip = false;
        EquipButton.gameObject.SetActive(!this.skill.IsEquip);
        UnequipButton.gameObject.SetActive(this.skill.IsEquip);
        skillSlot.SetBorderActive();
        chosenSkillInList.SetBorderActive();
    }
    void Unlock_ButtonFunc()
    {
        //go to shop
    }
    void Upgrade_ButtonFunc()
    {
        //upgrade
    }

    void Update()
    {
        
    }

    public void ShowInfomationSkill(SkillSlot skillSlot)
    {
        if(skillSlot.getSkill() == null)
        {
            SetInfoWhenNull(skillSlot);
            return;
        }

        if (this.skillSlot.getSkill() == skillSlot.getSkill())
            return;

        SetInfoOfSkill(skillSlot);

    }

    public void SetInfoWhenNull(SkillSlot skillSlot)
    {
        this.skillSlot.SetSkillInSlot(null);
        SkillName.gameObject.SetActive(false);
        Cooldown.gameObject.SetActive(false);
        Description.gameObject.SetActive(false);

        EquipButton.gameObject.gameObject.SetActive(false);
        UnequipButton.gameObject.gameObject.SetActive(false);
        UpgradeButton.gameObject.gameObject.SetActive(false);
        UnlockButton.gameObject.gameObject.SetActive(false);
    }

    public void SetInfoOfSkill(SkillSlot skillSlot)
    {
        SkillName.gameObject.SetActive(true);
        Cooldown.gameObject.SetActive(true);
        Description.gameObject.SetActive(true);

        this.skillSlot.SetSkillInSlot(skillSlot.getSkill());
        this.skill = skillSlot.getSkill();
        this.chosenSkillInList = skillSlot;

        SkillName.SetText(skill.Name);
        Cooldown.SetText("CD: " + skill.Cooldown + " Turns");
        Description.SetText(skill.Description);

        EquipButton.gameObject.SetActive(skill.IsHave && !skill.IsEquip);
        UnequipButton.gameObject.SetActive(skill.IsHave && skill.IsEquip);
        UpgradeButton.gameObject.SetActive(skill.IsHave);
        UnlockButton.gameObject.SetActive(!skill.IsHave);
    }


    public void SetInfo(SkillSlot skillSlot)
    {
        if (skillSlot.getSkill() == null)
            SetInfoWhenNull(skillSlot);
        else
            SetInfoOfSkill(skillSlot);
    }
}
