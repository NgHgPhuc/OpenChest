using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    public Transform Canvas;
    public List<FightingUnit> ChampList; 
    void Start()
    {
        SortSpeed();
    }

    public void SortSpeed()
    {
        ChampList.Sort((FightingUnit x, FightingUnit y) => y.speed.CompareTo(x.speed));

        foreach (FightingUnit f in ChampList)
            print(f.gameObject.name);
    }

 
}
