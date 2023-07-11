using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Buff
{
    public enum Type
    {
        IncreaseDamage,
        
    }
    public Type type;

    public Sprite Icon;
    public int duration;
    public float ValueChange;

    public delegate void Mode();
    public Mode Activation;
    public Mode Deactivation;
    public Mode Onactivation;
}
