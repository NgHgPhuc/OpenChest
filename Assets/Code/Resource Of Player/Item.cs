using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    public enum Type
    {
        //resources
        Gold,
        Diamond,
        CurrentExp,
        NeedExp,
        PlayerLevel,
        Chest,
        Ticket,
        //Chest
        ChestCurrentLevel,
        ChestNextLevel,
        //materials
        SmallExpPotion,
        MediumExpPotion,
        LargeExpPotion,
        SuperExpPotion
    }

    public Type type;

    public Sprite Icon;
}

