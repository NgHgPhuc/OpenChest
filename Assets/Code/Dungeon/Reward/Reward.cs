using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class Reward 
{
    public Item item;

    public int Mount;

    public void Earning()
    {
        switch(item.type)
        {
            case Item.Type.Gold:
                DataManager.Instance.ChangeValue(Item.Type.Gold,Mount, TemporaryData.ChangeType.ADDING);
                break;

            case Item.Type.Diamond:
                DataManager.Instance.ChangeValue(Item.Type.Diamond, Mount, TemporaryData.ChangeType.ADDING);
                break;

            case Item.Type.CurrentExp:
                DataManager.Instance.ChangeValue(Item.Type.CurrentExp, Mount, TemporaryData.ChangeType.ADDING);
                break;

            case Item.Type.Chest:
                DataManager.Instance.ChangeValue(Item.Type.Chest, Mount, TemporaryData.ChangeType.ADDING);
                break;

            default:
                break;
        }

    }

}