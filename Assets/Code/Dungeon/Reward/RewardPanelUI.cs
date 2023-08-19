using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardPanelUI : MonoBehaviour
{
    Transform RewardList;
    List<RewardItemUI> rewardItemUIList = new List<RewardItemUI>();

    Transform ConditionPanel;
    List<Image> ConditionUIList = new List<Image>();

    void Start()
    {
    }

    public void SetRewardList(List<Reward> reward,int StarCount)
    {
        if (RewardList == null || ConditionPanel == null)
            SetAttr();

        SetReward(reward);
        SetCondition(StarCount);
    }

    public void SetReward(List<Reward> reward)
    {
        if (RewardList == null)
            SetAttrReward();

        for (int i = 0; i < 5; i++)
        {
            if (i < reward.Count)
                rewardItemUIList[i].SetRewardItem(reward[i]);
            else rewardItemUIList[i].NoneReward();
        }
    }
    public void SetCondition(int StarCount)
    {
        if (ConditionPanel == null)
            SetAttrCondition();

        for (int i = 0; i < 3; i++)
        {
            if (i < StarCount)
                ConditionUIList[i].color = Color.white;
            else ConditionUIList[i].color = Color.black;
        }
    }


    void SetAttrReward()
    {
        RewardList = transform.Find("Reward List");
        for (int i = 0; i < 5; i++)
        {
            rewardItemUIList.Add(RewardList.GetChild(i).GetComponent<RewardItemUI>());
        }
        RewardList = transform.Find("Reward List (1)");
        for (int i = 0; i < 5; i++)
        {
            rewardItemUIList.Add(RewardList.GetChild(i).GetComponent<RewardItemUI>());
        }

    }
    void SetAttrCondition()
    {
        ConditionPanel = transform.Find("Condition Panel");
        for (int i = 0; i < 3; i++)
        {
            ConditionUIList.Add(ConditionPanel.GetChild(i).Find("Star Icon").GetComponent<Image>());
        }
    }
    void SetAttr()
    {
        SetAttrReward();
        SetAttrCondition();
    }


    //For get Clear item
    public void ClickBackground()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
