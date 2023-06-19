using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotInfomationPanel : MonoBehaviour, IPointerClickHandler
{
    public static SlotInfomationPanel Instance;
    Transform slotInfomation;

    SlotStatsPanel AttackDamage;
    SlotStatsPanel HealthPoint;
    SlotStatsPanel DefensePoint;
    SlotStatsPanel Speed;

    List<SlotStatsPanel> Passive = new List<SlotStatsPanel>();

    float StatsPlus=30;
    float PassivePlus = 20;

    TextMeshProUGUI StatsPlusText;
    TextMeshProUGUI StatsPlusUpgradeText;
    TextMeshProUGUI PassivePlusText;
    TextMeshProUGUI PassivePlusUpgradeText;

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

    public void Instantiate(Equipment equipment)
    {
        gameObject.SetActive(true);

        if (slotInfomation == null)
            SetAttr();

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
                Passive[i].SetStatsValue(e.Value, e.Key.ToString(), PassivePlus);

            }
            else Passive[i].gameObject.SetActive(false);
        }

    }

    void SetAttr()
    {
        slotInfomation = transform.Find("Slot Infomation");

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

        StatsPlusText.SetText("+" + StatsPlus.ToString() + "%");
        StatsPlusUpgradeText.SetText("+" + (StatsPlus + 1).ToString() + "%");
        PassivePlusText.SetText("+" + PassivePlus.ToString() + "%");
        PassivePlusUpgradeText.SetText("+" + (PassivePlus+1).ToString() + "%");
    }
    void ShowInfomation(Equipment equipment)
    {

    }

    public void UpgradeStatsButton()
    {
        StatsPlus += 1;

        StatsPlusText.SetText("+" + StatsPlus.ToString() + "%");
        StatsPlusUpgradeText.SetText("+" + (StatsPlus + 1).ToString() + "%");


    }

    public void UpgradePassiveButton()
    {
        PassivePlus += 1;

        PassivePlusText.SetText("+" + PassivePlus.ToString() + "%");
        PassivePlusUpgradeText.SetText("+" + (PassivePlus + 1).ToString() + "%");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.SetActive(false);
    }
}
