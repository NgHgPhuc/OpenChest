using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Globalization;

//In Get new equipment after open chest
public class SetEquipmentPanel : MonoBehaviour
{
    EquipmentSlot equipmentSlot;
    public EquipmentStats equipmentStats { get; private set; }
    public TextMeshProUGUI equipmentPower;

    void Start()
    {
        equipmentSlot = transform.Find("Weapon Slot Panel").GetComponent<EquipmentSlot>();
        equipmentStats = transform.Find("Equipment Stats Panel").GetComponent<EquipmentStats>();
        equipmentPower = transform.Find("Equipment Power").GetComponent<TextMeshProUGUI>();
    }

    public void SetEquipment(Equipment equipment)
    {
        SetAttr();

        equipmentSlot.SetEquipmentInSlot(equipment);
        equipmentStats.SetStatsInSlot(equipment);
        if(equipment != null)
            equipmentPower.SetText(equipment.PowerPoint().ToString(CultureInfo.InvariantCulture));
        else equipmentPower.SetText("No Equipment");
    }

    void SetAttr()
    {
        if (equipmentSlot == null)
            equipmentSlot = transform.Find("Weapon Slot Panel").GetComponent<EquipmentSlot>();

        if (equipmentStats == null)
            equipmentStats = transform.Find("Equipment Stats Panel").GetComponent<EquipmentStats>();

        if (equipmentPower == null)
            equipmentPower = transform.Find("Equipment Power").GetComponent<TextMeshProUGUI>();
    }
    public float Power()
    {
        SetAttr();
        try
        {
            return (float)Convert.ToDouble(equipmentPower.text);
        }
        catch (Exception) { return 0f; }
    }
}
