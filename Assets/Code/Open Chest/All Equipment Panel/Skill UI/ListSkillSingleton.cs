using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class ListSkillSingleton : MonoBehaviour
{
    //Dictionary<string, BaseSkill> skills = new Dictionary<string, BaseSkill>();
    public GameObject BlurBackground;
    public GameObject listSkillObject;

    public ListSkillUI listSkillUI;//show all skill
    public InfoSkillUI infoSkillUI;

    bool IsOpen = false;

    SkillSlot skillSlotOfEquipment;
    SkillSlot skillSlotOfList;
    SkillSlot skillSlotChosen; // if skillSlotOfEquipment have skill so it is skill slot chosen in list skill show

    public static ListSkillSingleton Instance { get; private set; }
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

        GetAllSkill();
    }

    public void GetAllSkill()
    {
        //if (skills.Count > 0)
        //    return;

        //List<BaseSkill> skillList = new List<BaseSkill>(Resources.LoadAll<BaseSkill>("Skill"));
        //foreach (BaseSkill s in skillList)
        //    skills.Add(s.Name, s);

    }

    public void ShowAllSkill()
    {
        List<BaseSkill> s = new List<BaseSkill>(Resources.LoadAll<BaseSkill>("Skill"));
        listSkillUI.SetListSkillUI(s);
    }

    //public BaseSkill getSkill(string name)
    //{
    //    return skills[name];
    //}

    public void OpenPanel()
    {
        if (IsOpen == true)
            return;

        IsOpen = true;
        BlurBackground.SetActive(true);
        listSkillObject.gameObject.SetActive(true);

        GetAllSkill();
        ShowAllSkill();
    }

    public void ShowInfoOfSkill(SkillSlot skillSlot)
    {
        if (skillSlot.IsSlotEquipment)
        {
            this.skillSlotOfEquipment = skillSlot;
            if (this.skillSlotOfEquipment.getSkill() != null)
            {
                this.skillSlotChosen = listSkillUI.skillDict[this.skillSlotOfEquipment.getSkill().Name];
                this.skillSlotOfList = listSkillUI.skillDict[this.skillSlotOfEquipment.getSkill().Name];
            }
        }

        if (skillSlot.IsSlotInList)
            this.skillSlotOfList = skillSlot;

        infoSkillUI.ShowInfomationSkill(skillSlot);
    }
    public void EquipSkill()
    {
        if(this.skillSlotChosen != null)
        {
            this.skillSlotChosen.getSkill().IsEquip = false;
            this.skillSlotChosen.SetBorderActive();
        }

        if(this.skillSlotOfList != null)
            this.skillSlotOfEquipment.SetSkillInSlot(this.skillSlotOfList.getSkill());
        else
            this.skillSlotOfEquipment.SetSkillInSlot(this.skillSlotChosen.getSkill());

        this.skillSlotChosen = listSkillUI.skillDict[this.skillSlotOfEquipment.getSkill().Name];

        if(this.skillSlotOfList != null)
        this.skillSlotOfList.SetBorderActive();

        PlayerManager.Instance.EquipSkillOfPlayer(this.skillSlotOfEquipment);
    }
    public void UnequipSkill()
    {
        this.skillSlotOfEquipment.DontHaveSkillInSlot();
        if(this.skillSlotOfList != null)
            this.skillSlotOfList.SetBorderActive();
    }
    public bool CheckIsSkillInSlot()
    {
        if (this.skillSlotOfList == null)
            return true;

        if (this.skillSlotOfList.getSkill() == this.skillSlotOfEquipment.getSkill())
            return true;

        return false;
    }

    public void ClickBlurBG()
    {
        BlurBackground.SetActive(false);
        listSkillObject.gameObject.SetActive(false);
        IsOpen = false;
        this.skillSlotChosen = null;
        this.skillSlotOfList = null;

        this.infoSkillUI.TurnOff();
    }


}
