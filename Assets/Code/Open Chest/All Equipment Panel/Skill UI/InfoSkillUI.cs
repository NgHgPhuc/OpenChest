using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoSkillUI : MonoBehaviour
{
    public SkillSlot skillSlot;
    BaseSkill skill;

    public TextMeshProUGUI SkillName;
    public TextMeshProUGUI Cooldown;
    public TextMeshProUGUI Level;
    public TextMeshProUGUI Description;

    public Button EquipButton;
    public Button UnequipButton;
    public Button UnlockButton;
    public Button UpgradeButton;

    public Button ShowNextLevelButton;
    public Button ShowPreLevelButton;
    int currentLevel;

    private void Start()
    {
        EquipButton.onClick.AddListener(Equip_ButtonFunc);
        UnequipButton.onClick.AddListener(Unequip_ButtonFunc);
        UnlockButton.onClick.AddListener(Unlock_ButtonFunc);
        UpgradeButton.onClick.AddListener(Upgrade_ButtonFunc);
        ShowNextLevelButton.onClick.AddListener(ShowNextLevelFunc);
        ShowPreLevelButton.onClick.AddListener(ShowPreLevelFunc);
    }

    void Equip_ButtonFunc()
    {
        ListSkillSingleton.Instance.EquipSkill();
        EquipButton.gameObject.SetActive(!this.skill.IsEquip);
        UnequipButton.gameObject.SetActive(this.skill.IsEquip);
        skillSlot.SetBorderActive();
    }
    void Unequip_ButtonFunc()
    {
        if (!ListSkillSingleton.Instance.CheckIsSkillInSlot())
        {
            InformManager.Instance.Initialize_FloatingInform("This skill in another slot, cant Unequip from this slot");
            return;
        }

        this.skill.IsEquip = false;
        EquipButton.gameObject.SetActive(!this.skill.IsEquip);
        UnequipButton.gameObject.SetActive(this.skill.IsEquip);
        skillSlot.SetBorderActive();
        ListSkillSingleton.Instance.UnequipSkill();
    }
    void Unlock_ButtonFunc()
    {
        WardPanel.Instance.ShopWarp();
    }
    void Upgrade_ButtonFunc()
    {
        //upgrade
    }
    void ShowNextLevelFunc()
    {
        if (currentLevel >= 5)
            return;

        currentLevel += 1;
        Description.SetText(skill.SkillLevelEffect[currentLevel-1]);
        Level.SetText("Level: " + currentLevel);
        ShowNextLevelButton.gameObject.SetActive(currentLevel != 5);
        ShowPreLevelButton.gameObject.SetActive(currentLevel != 1);
    }
    void ShowPreLevelFunc()
    {
        if (currentLevel <= 1)
            return;

        currentLevel -= 1;
        Description.SetText(skill.SkillLevelEffect[currentLevel - 1]);
        Level.SetText("Level: " + currentLevel);
        ShowNextLevelButton.gameObject.SetActive(currentLevel != 5);
        ShowPreLevelButton.gameObject.SetActive(currentLevel != 1);
    }

    void Update()
    {
        
    }

    public void ShowInfomationSkill(SkillSlot skillSlot)
    {
        if(skillSlot.getSkill() == null)
        {          
            SetInfoWhenNull();
            return;
        }

        if (this.skill == skillSlot.getSkill())
            return;

        SetInfoOfSkill(skillSlot);

    }

    public void SetInfoWhenNull()
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

        //this.skillSlot = skillSlot;
        this.skill = skillSlot.getSkill();
        this.skillSlot.SetSkillInSlot(this.skill);

        SkillName.SetText(skill.Name);
        Cooldown.SetText("CD: " + skill.Cooldown + " Turns");
        currentLevel = this.skill.Level;
        Level.SetText("Level: " + currentLevel);
        Description.SetText(skill.Description);

        EquipButton.gameObject.SetActive(skill.IsHave && !skill.IsEquip);
        UnequipButton.gameObject.SetActive(skill.IsHave && skill.IsEquip);
        UpgradeButton.gameObject.SetActive(skill.IsHave);
        UnlockButton.gameObject.SetActive(!skill.IsHave);

        ShowNextLevelButton.gameObject.SetActive(currentLevel != 5);
        ShowPreLevelButton.gameObject.SetActive(currentLevel != 1);
    }

    public void TurnOff()
    {
        skill = null;
    }

    public void SetInfo(SkillSlot skillSlot)
    {
        if (skillSlot.getSkill() == null)
            SetInfoWhenNull();
        else
            SetInfoOfSkill(skillSlot);
    }
}
