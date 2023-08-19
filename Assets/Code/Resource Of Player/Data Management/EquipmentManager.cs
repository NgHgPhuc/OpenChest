using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    Dictionary<Equipment.Type, EquipmentSO> equipments = new Dictionary<Equipment.Type, EquipmentSO>();
    public List<float> Stats = new List<float>(new float[4]);
    public List<float> Passives = new List<float>(new float[6]);
    public float AllPower;
    
    public EquipmentPanelManager equipmentPanelManager;
    public StatsPanelManager statsPanelManager;

    public static EquipmentManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    public void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        equipmentPanelManager.Initialize();
        statsPanelManager.Initialize();

        for (int i = 1; i <= 12; i++)
        {

            Equipment.Type type = (Equipment.Type)i;
            if (equipments.ContainsKey(type) == true) //have = continue
                continue;

            string link = "Equipment/" + type;
            EquipmentSO equipmentSO = Resources.Load<EquipmentSO>(link);

            equipments.Add(type, equipmentSO);

            if (equipmentSO.IsNull == false)
                LoadEquipment(type);
        }
    }

    void LoadEquipment(Equipment.Type type)
    {
        Equipment equipment = equipments[type].equipment;

        EquipmentAttributeHandle(equipment.Clone());

        equipmentPanelManager.SetEquipmentUI(equipment.Clone());
        statsPanelManager.ShowStats(Stats, Passives, AllPower);
    }

    public void EquipNewEquipment(Equipment equipment)
    {
        if (equipment == null)
            return;

        Equipment.Type equipmentType = equipment.type;
        EquipmentSO equipmentSO = equipments[equipmentType];

        if (equipmentSO.IsNull == false)
            UnequipmentAttributeHandle(equipmentType);
        EquipmentAttributeHandle(equipment.Clone());

        equipmentPanelManager.SetEquipmentUI(equipment.Clone());
        statsPanelManager.ShowStats(Stats, Passives, AllPower);

        equipmentSO.equipment = equipment;
        if(equipmentSO.IsNull == true)
            equipmentSO.IsNull = false;

        SaveEquipment(equipment);
    }
    void EquipmentAttributeHandle(Equipment equipment)
    {
        Equipment.Type type = equipment.type;

        Stats[0] += equipment.AttackDamage;
        Stats[1] += equipment.HealthPoint;
        Stats[2] += equipment.DefensePoint;
        Stats[3] += equipment.Speed;

        foreach (KeyValuePair<Equipment.Passive, float> kvp in equipment.PassiveList)
            Passives[(int)kvp.Key - 1] += kvp.Value;

        AllPower += equipment.PowerPoint();

    }

    void UnequipmentAttributeHandle(Equipment.Type type)
    {
        Equipment equipment = equipments[type].equipment.Clone();
        Stats[0] -= equipment.AttackDamage;
        Stats[1] -= equipment.HealthPoint;
        Stats[2] -= equipment.DefensePoint;
        Stats[3] -= equipment.Speed;

        foreach (KeyValuePair<Equipment.Passive, float> kvp in equipment.PassiveList)
            Passives[(int)kvp.Key - 1] -= kvp.Value;

        AllPower -= equipment.PowerPoint();
    }




    public Equipment Get(Equipment.Type type)
    {
        if (equipments[type].IsNull == true)
            return null;

        return equipments[type].equipment;
    }

    void SaveEquipment(Equipment equipment)
    {
        DataManager.Instance.SaveData(equipment.type.ToString(), equipment.ToStringData());
    }
}
