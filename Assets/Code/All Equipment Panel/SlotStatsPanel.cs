using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class SlotStatsPanel : StatsPanel
{
    TextMeshProUGUI StatsName;
    TextMeshProUGUI SlotPlus;
    public void SetStatsValue(float value,string name,float Percent)
    {
        this.Value = value;
        this.Value = (float)Math.Round(this.Value, 2);
        ShowStatsName(name);
        ShowStatsValue();
        ShowSlotPlus(Percent);
    }

    public void ShowStatsName(string name)
    {
        if (StatsName == null)
            StatsName = transform.Find("Stats Name").GetComponent<TextMeshProUGUI>();

        if (name == "")
            return;

        StatsName.SetText(name);
    }

    public void ShowSlotPlus(float Percent)
    {
        if (SlotPlus == null)
            SlotPlus = transform.Find("Slot Plus").GetComponent<TextMeshProUGUI>();

        if (Percent == 0)
        {
            SlotPlus.SetText("");
            return;
        }

        float plus = (float)(this.Value * Percent / 100);
        SlotPlus.SetText("(+" + plus + ")");
    }
}
