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

    public void ShowStatsValue(int mode = 0)
    {
        if(StatsValue == null)
            StatsValue = transform.Find("Stats Value").GetComponent<TextMeshProUGUI>();

        if(mode == 0)
            StatsValue.SetText(Math.Ceiling(this.Value).ToString());
        else
            StatsValue.SetText((this.Value).ToString());
    }



    public void SetStatsValue(float value, int mode = 0)
    {
        this.Value = value;
        this.Value = (float)Math.Round(this.Value, 2);
        ShowStatsValue(mode);
    }

    public float GetValue()
    {
        return Value;
    }
}
