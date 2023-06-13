using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanelManager : MonoBehaviour
{
    public static EquipmentPanelManager Instance { get; private set; }
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

    }

    public void SetEquipmentSlot(Equipment equipment)
    {
        string ChildName = equipment.type.ToString() + " Slot Panel";
        EquipmentData equipmentSlot = transform.Find(ChildName).GetComponent<EquipmentData>();
        equipmentSlot.SetEquipmentData(equipment);
    }

    public Equipment GetEquipmentSlot(Equipment.Type EquipmentType)
    {
        string ChildName = EquipmentType.ToString() + " Slot Panel";
        EquipmentData equipmentSlot = transform.Find(ChildName).GetComponent<EquipmentData>();
        return equipmentSlot.GetEquipmentData();
    }
}
