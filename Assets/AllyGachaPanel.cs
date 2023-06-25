using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Equipment;

public class AllyGachaPanel : MonoBehaviour
{
    Dictionary<Equipment.Quality, float> RateInfomation = new Dictionary<Equipment.Quality, float>()
    {   { Equipment.Quality.Common, 70} ,
        { Equipment.Quality.Rare,20},
        { Equipment.Quality.Epic, 9},
        { Equipment.Quality.Legendary, 1},
    };
    List<float> RandomProb = new List<float>() { 70, 90, 99, 100 };
    // 0 - 70 white , 70 - 90 - green , 90 - 99 - blue , 99 - 100 purple


    void Start()
    {
        
    }

    public void RandomAlly(int times)
    {
        if (!ResourceManager.Instance.CheckEnough_Diamond(times * 160))
            return;

        ResourceManager.Instance.ChangeDiamond(-times * 160);
        for(int i = 0; i < times; i++)
        {
            float r = UnityEngine.Random.Range(0f, 100f);

            int Quality = GetAlly(r);
            print(RateInfomation.ElementAt(Quality).Key);
        }
    }

    int GetAlly(float accuracy)
    {
        for (int i = 0; i < RandomProb.Count; i++)
        {
            if (accuracy < RandomProb[i])
            {
                return i;
            }
        }
        return -1;
    }
}
