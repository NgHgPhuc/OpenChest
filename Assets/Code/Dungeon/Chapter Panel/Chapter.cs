using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Chapter", menuName = "ScriptableObjects/Chapter")]
public class Chapter : ScriptableObject
{
    public string Name;
    public bool IsOpen;
    public bool IsDone;
    public int StarCount;
    public List<Character> EnemyTeam;
    public List<Reward> reward;

    public List<Character> MyTeam { get; private set; }

    public void SetMyTeam(List<Character> MyTeam)
    {
        this.MyTeam = MyTeam;
    }
}