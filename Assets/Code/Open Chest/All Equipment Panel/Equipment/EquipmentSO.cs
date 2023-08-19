using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentSO", menuName = "ScriptableObjects/EquipmentSO")]

public class EquipmentSO : ScriptableObject
{
    public bool IsNull;
    public float StatsPlus;
    public float PassivePlus;

    public Equipment equipment;

    private void OnEnable() => hideFlags = HideFlags.DontUnloadUnusedAsset;

    public Equipment.Type equipmentType()
    {
        return equipment.type;
    }

}
