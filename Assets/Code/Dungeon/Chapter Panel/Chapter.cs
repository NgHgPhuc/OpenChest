using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Chapter", menuName = "ScriptableObjects/Chapter")]
public class Chapter : ScriptableObject
{
    public string Name;
    public List<Character> EnemyTeam;
    public List<Reward> reward;

}