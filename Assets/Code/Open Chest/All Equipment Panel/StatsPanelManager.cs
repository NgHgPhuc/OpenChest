using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class StatsPanelManager : MonoBehaviour
{
    public List<StatsPanel> AllStatsPanel { get; private set; }
    public List<StatsPanel> AllPassivePanel { get; private set; }

    float Power=0;
    public TextMeshProUGUI PowerText;

    public static StatsPanelManager Instance { get; private set; }
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
    }

    void Start()
    {
        AllStatsPanel = new List<StatsPanel>();
        AllPassivePanel = new List<StatsPanel>();

        for (int i = 0; i < transform.childCount; i++)
        {
            if(i<4)
                AllStatsPanel.Add(transform.GetChild(i).GetComponent<StatsPanel>());
            else
                AllPassivePanel.Add(transform.GetChild(i).GetComponent<StatsPanel>());

        }

        PowerText.SetText(Power.ToString());
    }

    public void Equip(Equipment equipment)
    {
        if (equipment == null)
            return;

        AllStatsPanel[0].SetStatsValue(equipment.AttackDamage);
        AllStatsPanel[1].SetStatsValue(equipment.HealthPoint);
        AllStatsPanel[2].SetStatsValue(equipment.DefensePoint);
        AllStatsPanel[3].SetStatsValue(equipment.Speed);

        foreach (KeyValuePair<Equipment.Passive, float> kvp in equipment.PassiveList)
            AllPassivePanel[(int)kvp.Key-1].SetStatsValue(kvp.Value,1);

        TeamManager.Instance.SetStatsPlayer(AllStatsPanel, AllPassivePanel);

        Power += equipment.PowerPoint;
        PowerText.SetText((Convert.ToUInt32(Power)).ToString());

    }

    public void Unequip(Equipment equipment)
    {
        if (equipment == null)
            return;

        AllStatsPanel[0].SetStatsValue(-equipment.AttackDamage);
        AllStatsPanel[1].SetStatsValue(-equipment.HealthPoint);
        AllStatsPanel[2].SetStatsValue(-equipment.DefensePoint);
        AllStatsPanel[3].SetStatsValue(-equipment.Speed);

        foreach (KeyValuePair<Equipment.Passive, float> kvp in equipment.PassiveList)
            AllPassivePanel[(int)kvp.Key-1].SetStatsValue(-kvp.Value);

        Power -= equipment.PowerPoint;
        PowerText.SetText((Convert.ToUInt32(Power)).ToString());
    }
}
