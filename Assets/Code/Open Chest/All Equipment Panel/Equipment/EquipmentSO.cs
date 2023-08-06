using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentSO", menuName = "ScriptableObjects/EquipmentSO")]

public class EquipmentSO : ScriptableObject
{
    public bool IsNull;

    public Equipment equipment;

    public float StatsPlus;
    public float PassivePlus;

    public Equipment.Type equipmentType()
    {
        return equipment.type;
    }

}
