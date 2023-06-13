using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentData : MonoBehaviour
{
    Equipment equipment;
    EquipmentSlot equipmentSlot;
    void Start()
    {
        equipmentSlot = GetComponent<EquipmentSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEquipmentData(Equipment equipment)
    {
        this.equipment = equipment;
        equipmentSlot.SetEquipmentInSlot(this.equipment);
    }

    public Equipment GetEquipmentData()
    {
        return this.equipment;
    }
}
