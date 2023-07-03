using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MissionPanel : MonoBehaviour,IPointerClickHandler
{
    Mission mission;
    RewardItemUI rewardItemUI;
    MissionDescription missionDescription;
    void Start()
    {
        rewardItemUI = transform.Find("Reward").GetComponent < RewardItemUI>();
        missionDescription = transform.Find("Mission Description Panel").GetComponent<MissionDescription>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMissionUI(Mission mission)
    {
        this.mission = mission;
        rewardItemUI.SetRewardItem(mission.reward);
        missionDescription.SetDescriptionUI(mission);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!this.mission.CheckDoneMission())
            return;

        this.mission.EarnReward();
        MissionManager.Instance.LoadNextMission();
    }
}
