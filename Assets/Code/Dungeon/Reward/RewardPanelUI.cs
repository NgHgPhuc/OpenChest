using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPanelUI : MonoBehaviour
{
    Transform RewardList;
    List<RewardItemUI> rewardItemUIList = new List<RewardItemUI>();
    Transform ConditionPanel;
    void Start()
    {
        RewardList = transform.Find("Reward List");
        for(int i = 0; i < 5; i++)
        {
            rewardItemUIList.Add(RewardList.GetChild(i).GetComponent<RewardItemUI>());
        }
    }

    public void SetRewardList(List<Reward> reward)
    {
        if(RewardList == null)
        {
            RewardList = transform.Find("Reward List");
            for (int i = 0; i < 5; i++)
            {
                rewardItemUIList.Add(RewardList.GetChild(i).GetComponent<RewardItemUI>());
            }
        }

        for (int i = 0; i < 5; i++)
        {
            if (i < reward.Count)
                rewardItemUIList[i].SetRewardItem(reward[i]);
            else rewardItemUIList[i].NoneReward();
        }

    }


}
