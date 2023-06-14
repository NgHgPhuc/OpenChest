using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class StatsPanel : MonoBehaviour
{
    float Value=0;
    TextMeshProUGUI StatsValue;
    void Start()
    {
        StatsValue = transform.Find("Stats Value").GetComponent<TextMeshProUGUI>();
        ShowStatsValue();
    }

    public void ShowStatsValue()
    {
        StatsValue.SetText(this.Value.ToString());
    }

    public void SetStatsValue(float value)
    {
        this.Value += value;
        this.Value = (float)Math.Round(this.Value, 2);
        ShowStatsValue();
    }
}
