using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class Reward 
{
    public enum Type
    {
        Gold,
        Diamond,
        Exp,
        Chest
    }

    public Type type;

    public Sprite Icon;

    public int Mount;

    public void Earning()
    {
        switch(type)
        {
            case Type.Gold:
                ResourceManager.Instance.ChangeGold(Mount);
                break;

            case Type.Diamond:
                ResourceManager.Instance.ChangeDiamond(Mount);
                break;

            case Type.Exp:
                ResourceManager.Instance.GainExp(Mount);
                break;

            case Type.Chest:
                ChestHandlerManager.Instance.GetChest(Mount);
                break;
            default:
                break;
        }

    }

}