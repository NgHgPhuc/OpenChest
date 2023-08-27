using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;

[Serializable]
public class Slot
{
    public Item item;
    public float Mount;
    public Slot(Item item, float Mount)
    {
        this.item = item;
        this.Mount = Mount;
    }
    public Item.Type TypeOfItem()
    {
        return this.item.type;
    }
}

[CreateAssetMenu(fileName = "TemporaryData", menuName = "ScriptableObjects/TemporaryData")]
public class TemporaryData : ScriptableObject
{
    public List<Slot> slotInventories;
    public Dictionary<Item.Type, Slot> inventorys = new Dictionary<Item.Type, Slot>();//split - resource - item


    public void LoadAllItemSO()
    {
        foreach (Slot i in slotInventories)
            if (!inventorys.ContainsKey(i.TypeOfItem()))
                inventorys.Add(i.TypeOfItem(), i);
    }


    public void LoadValueFromCloud(Item.Type type, float value)
    {
        if (!inventorys.ContainsKey(type))
            return;

        inventorys[type].Mount = value;
    }

    public string GetValue_String(Item.Type type)
    {
        return inventorys[type].Mount.ToString(CultureInfo.InvariantCulture);
    }
    public float GetValue_Float(Item.Type type)
    {
        return inventorys[type].Mount;
    }
    public int GetValue_Int(Item.Type type)
    {
        return (int)inventorys[type].Mount;
    }

    public enum ChangeType
    {
        USING,
        ADDING
    }
    public void ChangeValue(Item.Type type, float changeMount, ChangeType changeType)
    {
        int cT = (int)changeType * 2 - 1;//using = -1, adding = 1
        inventorys[type].Mount += cT * changeMount;
    }

    public bool IsCompareWith(Item.Type type, float value)
    {
        return inventorys[type].Mount > value;
    }
}
