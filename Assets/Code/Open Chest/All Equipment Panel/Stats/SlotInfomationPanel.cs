using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotInfomationPanel : MonoBehaviour
{
    public Transform Panel;

    Transform slotInfomation;

    SlotStatsPanel AttackDamage;
    SlotStatsPanel HealthPoint;
    SlotStatsPanel DefensePoint;
    SlotStatsPanel Speed;

    List<SlotStatsPanel> Passive = new List<SlotStatsPanel>();

    float StatsPlus;
    float PassivePlus;

    TextMeshProUGUI StatsPlusText;
    TextMeshProUGUI StatsPlusUpgradeText;
    TextMeshProUGUI PassivePlusText;
    TextMeshProUGUI PassivePlusUpgradeText;

    EquipmentSlot equipmentSlot;
    TextMeshProUGUI EquipName;

    EquipmentData equipmentData;

    Animator animator;
    public static SlotInfomationPanel Instance;
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

        gameObject.SetActive(false);
    }

    void Start()
    {
        
    }

    public void Instantiate(EquipmentData equipmentData,Transform trans)
    {
        this.equipmentData = equipmentData;
        this.StatsPlus = equipmentData.StatsPlus;
        this.PassivePlus = equipmentData.PassivePlus;

        Equipment equipment = equipmentData.equipment;

        gameObject.SetActive(true);

        if (slotInfomation == null)
            SetAttr();

        FirstShowing(trans);

        AttackDamage.SetStatsValue(equipment.AttackDamage,"", StatsPlus);
        HealthPoint.SetStatsValue(equipment.HealthPoint,"", StatsPlus);
        DefensePoint.SetStatsValue(equipment.DefensePoint, "", StatsPlus);
        Speed.SetStatsValue(equipment.Speed, "", StatsPlus);

        for (int i = 0; i < 2; i++)
        {
            if (i < equipment.PassiveList.Count)
            {
                Passive[i].gameObject.SetActive(true);
                var e = equipment.PassiveList.ElementAt(i);
                Passive[i].SetStatsValue(e.Value, e.Key.ToString(), PassivePlus,1);

            }
            else Passive[i].gameObject.SetActive(false);
        }

        animator.Play("Open");
    }

    void FirstShowing(Transform trans)
    {
        equipmentSlot.SetEquipmentInSlot(equipmentData.equipment);
        string equipName = "[" + equipmentData.equipment.quality + "]" + equipmentData.equipment.type;
        EquipName.SetText(equipName);
        EquipName.color = equipmentSlot.BackgroundColor();


        StatsPlusText.SetText("+" + StatsPlus.ToString() + "%");
        StatsPlusUpgradeText.SetText("+" + (StatsPlus + 1).ToString() + "%");
        PassivePlusText.SetText("+" + PassivePlus.ToString() + "%");
        PassivePlusUpgradeText.SetText("+" + (PassivePlus + 1).ToString() + "%");

        SetPosition(trans);
    }

    void SetPosition(Transform trans)
    {
        Panel.gameObject.SetActive(true);

        RectTransform transRect = (RectTransform)trans;
        RectTransform slotInfomationRect = (RectTransform)slotInfomation;
        float deltaX = transRect.rect.width / 2 + slotInfomationRect.rect.width / 2;
        float deltaY = transRect.rect.height / 2 - slotInfomationRect.rect.height / 2;
        RectTransform MainCanvas = (RectTransform)Panel.root;

        if (trans.position.x + transRect.rect.width / 2 + slotInfomationRect.rect.width > MainCanvas.rect.width)
            slotInfomation.transform.position = trans.position + new Vector3(-(deltaX + deltaX * 2 / 100), deltaY, 0);
        else
            slotInfomation.transform.position = trans.position + new Vector3(deltaX + deltaX * 2 / 100, deltaY, 0);
    }

    void SetAttr()
    {
        slotInfomation = Panel.Find("Slot Infomation");

        equipmentSlot = slotInfomation.Find("Weapon Slot Panel").GetComponent<EquipmentSlot>();
        EquipName = slotInfomation.Find("Equipment Name").GetComponent<TextMeshProUGUI>();

        AttackDamage = slotInfomation.Find("Stats Attack Panel").GetComponent<SlotStatsPanel>();
        HealthPoint = slotInfomation.Find("Stats Health Point Panel").GetComponent<SlotStatsPanel>();
        DefensePoint = slotInfomation.Find("Stats Defense Point Panel").GetComponent<SlotStatsPanel>();
        Speed = slotInfomation.Find("Stats Speed Panel").GetComponent<SlotStatsPanel>();

        for (int i = 0; i < 2; i++)
        {
            Passive.Add(slotInfomation.Find("Passive " + (i+1).ToString() + " Panel").GetComponent<SlotStatsPanel>());
        }

        StatsPlusText = slotInfomation.Find("Stats Upgrade Panel").Find("Stats Current Value").GetComponent<TextMeshProUGUI>();
        StatsPlusUpgradeText = slotInfomation.Find("Stats Upgrade Panel").Find("Stats Upgrade Value").GetComponent<TextMeshProUGUI>();
        PassivePlusText = slotInfomation.Find("Passive Upgrade Panel").Find("Passive Current Value").GetComponent<TextMeshProUGUI>();
        PassivePlusUpgradeText = slotInfomation.Find("Passive Upgrade Panel").Find("Passive Upgrade Value").GetComponent<TextMeshProUGUI>();

        animator = GetComponent<Animator>();

    }
    void ShowInfomation(Equipment equipment)
    {

    }

    public void UpgradeStatsButton()
    {
        StatsPlus += 1;
        equipmentData.UpgradeStats();

        StatsPlusText.SetText("+" + StatsPlus.ToString() + "%");
        StatsPlusUpgradeText.SetText("+" + (StatsPlus + 1).ToString() + "%");

        AttackDamage.ShowSlotPlus(StatsPlus);
        HealthPoint.ShowSlotPlus(StatsPlus);
        DefensePoint.ShowSlotPlus(StatsPlus);
        Speed.ShowSlotPlus(StatsPlus);
    }

    public void UpgradePassiveButton()
    {
        PassivePlus += 1;
        equipmentData.UpgradePassive();

        PassivePlusText.SetText("+" + PassivePlus.ToString() + "%");
        PassivePlusUpgradeText.SetText("+" + (PassivePlus + 1).ToString() + "%");

        for (int i = 0; i < 2; i++)
        {
            if (Passive[i].gameObject.activeSelf == false)
                return;

            Passive[i].ShowSlotPlus(PassivePlus);
        }
    }

    public void ClickBackground()
    {
        Panel.gameObject.SetActive(false);
    }
}
