using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class StatsPanel : MonoBehaviour
{
    protected float Value=0;
    TextMeshProUGUI StatsValue;
    void Start()
    {
        StatsValue = transform.Find("Stats Value").GetComponent<TextMeshProUGUI>();
        ShowStatsValue();
    }

    public void ShowStatsValue()
    {
        if(StatsValue == null)
            StatsValue = transform.Find("Stats Value").GetComponent<TextMeshProUGUI>();

        StatsValue.SetText(Math.Ceiling(this.Value).ToString());
    }

    public virtual void SetStatsValue(float value)
    {
        this.Value += value;
        this.Value = (float)Math.Round(this.Value, 2);
        ShowStatsValue();
    }
}
