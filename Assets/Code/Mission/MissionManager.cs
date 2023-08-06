using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager: MonoBehaviour
{
    public MissionPanel missionPanel;
    public Mission mission;
    public int currentMission = 0;
    public static MissionManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        
    }

    void SetMissionUI()
    {
        missionPanel.SetMissionUI(this.mission);
    }

    public void DoingMission_EarnChest()
    {
        if (mission.type != Mission.Type.EarnChest)
            return;

        this.mission.AddCurrent(1);
        SetMissionUI();

    }
    public void DoingMission_LevelUp()
    {
        if (mission.type != Mission.Type.LevelUp)
            return;

        this.mission.SetCurrent(DataManager.Instance.temporaryData.GetValue_Float(Item.Type.PlayerLevel));
    }
    public void DoingMission_DefeatChapter()
    {
        if (mission.type != Mission.Type.DefeatChapter)
            return;

    }
    public void DoingMission_UseDiamond(float Mount)
    {
        if (mission.type != Mission.Type.UseDiamond)
            return;

        if (Mount > 0)
            this.mission.AddCurrent(Mount);
        SetMissionUI();

    }
    public void DoingMission_UseGold(float Mount)
    {
        if (mission.type != Mission.Type.UseGold)
            return;

        if (Mount > 0)
            this.mission.AddCurrent(Mount);
        SetMissionUI();
    }


    public void LoadProgressMission()
    {
        switch(this.mission.type)
        {
            case Mission.Type.LevelUp:
                this.mission.SetCurrent(DataManager.Instance.temporaryData.GetValue_Float(Item.Type.PlayerLevel));
                break;

            case Mission.Type.DefeatChapter:
                Chapter c = Resources.Load<Chapter>("Chapter/Chapter " + mission.Goal);
                if (c.IsDone == true)
                    this.mission.SetCurrent(mission.Goal);
                else
                    this.mission.SetCurrent(0);
                break;

            case Mission.Type.UpgradeChest:
                break;

            default:
                this.mission.SetCurrent(0);
                break;
        }
    }
    public void LoadNextMission()
    {
        currentMission += 1;
        this.mission = Resources.Load<Mission>("Mission/Mission " + currentMission);
        LoadProgressMission();
        SetMissionUI();
    }

}
