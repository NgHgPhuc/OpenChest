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

    public void SetEquipment(Equipment equipment)
    {
        string ChildName = equipment.type.ToString() + " Slot Panel";
        EquipmentData equipmentData = transform.Find(ChildName).GetComponent<EquipmentData>();

        equipmentData.SetEquipmentData(equipment);
    }

    public Equipment GetEquipment(Equipment.Type EquipmentType)
    {
        string ChildName = EquipmentType.ToString() + " Slot Panel";
        EquipmentData equipmentSlot = transform.Find(ChildName).GetComponent<EquipmentData>();
        return equipmentSlot.GetEquipmentData();
    }

}
