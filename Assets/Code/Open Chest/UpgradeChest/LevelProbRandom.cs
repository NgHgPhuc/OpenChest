using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/LevelProbRandom", order = 1)]
public class LevelProbRandom : ScriptableObject
{
    public List<float> RandomRate;
    public List<int> DamageChestRange;
    public List<int> ChestHpRange;

    public float Cost;

    public List<float> GetSumRandomRate()
    {
        float s = 0;
        List<float> sumRandomRate = new List<float>();
        for(int i = 0 ; i < RandomRate.Count ; i++)
        {
            s += RandomRate[i];
            sumRandomRate.Add(s);
        }
        return sumRandomRate;
    }

}