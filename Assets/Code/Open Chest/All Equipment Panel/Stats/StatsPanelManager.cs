using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class StatsPanelManager : MonoBehaviour
{
    public List<StatsPanel> AllStatsPanel { get; private set; }
    public List<StatsPanel> AllPassivePanel { get; private set; }

    public TextMeshProUGUI PowerText;

    public void Initialize()
    {
        if (AllStatsPanel != null && AllPassivePanel != null)
            return;

        AllStatsPanel = new List<StatsPanel>();
        AllPassivePanel = new List<StatsPanel>();

        for (int i = 0; i < transform.childCount; i++)
        {
            if(i<4)
                AllStatsPanel.Add(transform.GetChild(i).GetComponent<StatsPanel>());
            else
                AllPassivePanel.Add(transform.GetChild(i).GetComponent<StatsPanel>());

        }
    }

    public void ShowStats(List<float> Stats, List<float> Passives,float AllPower)
    {
        Initialize();

        for (int i = 0; i < AllStatsPanel.Count; i++)
            AllStatsPanel[i].SetStatsValue(Stats[i]);

        for (int i = 0; i < AllPassivePanel.Count; i++)
            AllPassivePanel[i].SetStatsValue(Passives[i],1);

        PowerText.SetText((Convert.ToUInt32(AllPower)).ToString());

    }
}
