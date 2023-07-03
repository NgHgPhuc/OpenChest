using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "RewardItem", menuName = "ScriptableObjects/RewardItem")]
public class RewardItem : ScriptableObject
{
    public enum Type
    {
        Gold,
        Diamond,
        Exp,
        Chest,
        Ticket
    }

    public Type type;

    public Sprite Icon;
}

