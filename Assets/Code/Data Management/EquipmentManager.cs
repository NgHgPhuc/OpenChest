using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance { get; private set; }
    Dictionary<Equipment.Type, EquipmentSO> equipments = new Dictionary<Equipment.Type, EquipmentSO>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        for (int i = 0; i < 12; i++)
        {

            Equipment.Type type = (Equipment.Type)i;
            if (equipments.ContainsKey(type) == true) //have = continue
                continue;

            EquipmentSO equipmentSO = Resources.Load<EquipmentSO>("Equipment/" + type);

            equipments.Add(type, equipmentSO);
        }
    }
}
