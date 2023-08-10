using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanelManager : MonoBehaviour
{
    public static EquipmentPanelManager Instance { get; private set; }
    Dictionary<string, EquipmentData> equipments = new Dictionary<string, EquipmentData>();
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform childTrans = transform.GetChild(i);
            if (childTrans.GetComponent<EquipmentData>() == null)
                continue;

            string childName = childTrans.name; //name : "Weapon Slot Panel
            if (equipments.ContainsKey(childName) == true) //have = continue
                continue;

            EquipmentData equipmentData = childTrans.GetComponent<EquipmentData>();

            string type = childName.Split(" ")[0];
            EquipmentSO equipmentSO = Resources.Load<EquipmentSO>("Equipment/" + type);

            equipments.Add(childName, equipmentData);

            if (equipmentSO.IsNull == false)
                equipmentData.SetEquipmentData(equipmentSO.equipment);
        }

        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    Transform childTrans = transform.GetChild(i);
        //    if (childTrans.GetComponent<EquipmentData>() == null)
        //        continue;

        //    string childName = childTrans.name; //name : "Weapon Slot Panel
        //    if (equipments.ContainsKey(childName) == false) //have = continue
        //    {
        //        EquipmentData equipmentData = childTrans.GetComponent<EquipmentData>();
        //        equipments.Add(childName, equipmentData);
        //    }
        //}
    }


    public void SetEquipment(Equipment equipment)
    {
        string ChildName = equipment.type.ToString() + " Slot Panel";
        EquipmentData equipmentData = equipments[ChildName];

        SaveEquipment(equipment);
        equipmentData.SetEquipmentData(equipment);
    }

    public Equipment GetEquipment(Equipment.Type EquipmentType)
    {
        string ChildName = EquipmentType.ToString() + " Slot Panel";
        EquipmentData equipmentSlot = equipments[ChildName];
        return equipmentSlot.equipment;
    }

    public void LoadEquipment(Equipment equipment)
    {
        SetEquipment(equipment);
    }

    public void SaveEquipment(Equipment equipment)
    {
        DataManager.Instance.SaveData(equipment.type.ToString(), equipment.ToStringData());
    }
}
