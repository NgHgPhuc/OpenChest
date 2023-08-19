using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanelManager : MonoBehaviour
{
    Dictionary<string, EquipmentSlot> equipments = new Dictionary<string, EquipmentSlot>();

    public void Initialize()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform childTrans = transform.GetChild(i);
            if (childTrans.GetComponent<EquipmentSlot>() == null)
                continue;

            string childName = childTrans.name; //name : "Weapon Slot Panel
            if (equipments.ContainsKey(childName) == false) //have = continue
            {
                EquipmentSlot equipmentSlot = childTrans.GetComponent<EquipmentSlot>();
                equipments.Add(childName, equipmentSlot);
            }
        }
    }

    public void SetEquipmentUI(Equipment equipment)
    {
        if (equipment == null)
            return;

        string ChildName = equipment.type.ToString() + " Slot Panel";
        EquipmentSlot equipmentSlot = equipments[ChildName];

        equipmentSlot.SetEquipmentInSlot(equipment);
    }
}
