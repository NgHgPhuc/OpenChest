using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllRewardPanel : MonoBehaviour
{
    public List<AllyReward> allyGetRewardList;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize()
    {
        for(int i = 0; i < 3; i++)
            allyGetRewardList[i].Initialize(FightManager.Instance.PlayerTeam[i]);
    }
}
