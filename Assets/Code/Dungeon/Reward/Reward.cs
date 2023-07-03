using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class Reward 
{
    public RewardItem item;

    public int Mount;

    public void Earning()
    {
        switch(item.type)
        {
            case RewardItem.Type.Gold:
                ResourceManager.Instance.ChangeGold(Mount);
                break;

            case RewardItem.Type.Diamond:
                ResourceManager.Instance.ChangeDiamond(Mount);
                break;

            case RewardItem.Type.Exp:
                ResourceManager.Instance.GainExp(Mount);
                break;

            case RewardItem.Type.Chest:
                ChestHandlerManager.Instance.GetChest(Mount);
                break;
            default:
                break;
        }

    }

}