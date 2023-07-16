using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentData : MonoBehaviour, IPointerClickHandler
{
    public Equipment equipment { get; private set; }
    Equipment slot;

    EquipmentSlot equipmentSlot;

    public float StatsPlus;
    public float PassivePlus;

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
        StatsPanelManager.Instance.Unequip(slot);

        this.equipment = equipment;
        SetSlotData(equipment);

        equipmentSlot.SetEquipmentInSlot(this.equipment);

        StatsPanelManager.Instance.Equip(slot);

    }

    public Equipment GetEquipmentData()
    {
        return this.equipment;
    }

    public void SetSlotData(Equipment equipment)
    {
        slot = equipment.Clone();

        slot.AttackDamage += slot.AttackDamage * StatsPlus/100f;
        slot.HealthPoint += slot.HealthPoint * StatsPlus/100f;
        slot.DefensePoint += slot.DefensePoint * StatsPlus/100f;
        slot.Speed += slot.Speed * StatsPlus/100f;

        List<Equipment.Passive> keys = new List<Equipment.Passive>(slot.PassiveList.Keys);
        foreach (Equipment.Passive key in keys)
            slot.PassiveList[key] += slot.PassiveList[key] * PassivePlus / 100f;

        slot.PowerPoint = slot.calPowerPoint();

    }

    public Equipment GetSlotData()
    {
        return slot;
    }

    public void UpgradeStats()
    {
        StatsPlus += 1;
        SetEquipmentData(this.equipment);
    }

    public void UpgradePassive()
    {
        PassivePlus += 1;
        SetEquipmentData(this.equipment);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(equipment != null)
            SlotInfomationPanel.Instance.Instantiate(this,transform);
    }
}
