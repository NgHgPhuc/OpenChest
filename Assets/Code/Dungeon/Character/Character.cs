using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character")]
public class Character : ScriptableObject
{
    public Sprite Icon;

    public int Level;
    public float AttackDamage;
    public float HealthPoint;
    public float DefensePoint;
    public float Speed;

}