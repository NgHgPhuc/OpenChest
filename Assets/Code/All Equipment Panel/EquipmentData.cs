using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentData : MonoBehaviour, IPointerClickHandler
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


    public void OnPointerClick(PointerEventData eventData)
    {
        if(equipment != null)
            SlotInfomationPanel.Instance.Instantiate(equipment);
    }
}
