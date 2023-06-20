using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class CompareStats : MonoBehaviour
{
    SetEquipmentPanel NewEquipmentPanel;
    public SetEquipmentPanel OldEquipmentPanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Compare()
    {
        if (NewEquipmentPanel == null)
            NewEquipmentPanel = GetComponent<SetEquipmentPanel>();

        CompareStat(NewEquipmentPanel.Power(), OldEquipmentPanel.Power(), NewEquipmentPanel.equipmentPower);

        EquipmentStats NewEquipmentStats = NewEquipmentPanel.equipmentStats;
        EquipmentStats OldEquipmentStats = OldEquipmentPanel.equipmentStats;
        for (int i = 0; i <= 3; i++)
        {
            float New_Stats = NewEquipmentStats.GetStatsFloat()[i];
            float Old_Stats = OldEquipmentStats.GetStatsFloat()[i];
            CompareStat(New_Stats, Old_Stats, NewEquipmentStats.GetStatsText()[i]);
        }
    }

    void CompareStat(float NewEquipment,float OldEquipment,TextMeshProUGUI text)
    {
        if (NewEquipment > OldEquipment)
            text.color = new Color(96f / 255, 241f / 255, 72f / 255);
        else
            if(NewEquipment == OldEquipment)
                text.color = new Color(0,0,0);
            else
                text.color = new Color(255f / 255, 69f / 255, 71f / 255);
    }
}
