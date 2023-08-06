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
                ResourceManager.Instance.ChangeGold(Mount, TemporaryData.ChangeType.ADDING);
                break;

            case Item.Type.Diamond:
                ResourceManager.Instance.ChangeDiamond(Mount, TemporaryData.ChangeType.ADDING);
                break;

            case Item.Type.CurrentExp:
                ResourceManager.Instance.ChangeExp(Mount, TemporaryData.ChangeType.ADDING);
                break;

            case Item.Type.Chest:
                ResourceManager.Instance.ChangeChest(Mount, TemporaryData.ChangeType.ADDING);
                break;

            default:
                break;
        }

    }

}