using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mission", menuName = "ScriptableObjects/Mission")]
public class Mission : ScriptableObject
{
    public int Index;
    public string Name;
    public Reward reward;

    public enum Type
    {
        LevelUp,
        EarnChest,
        DefeatChapter,
        UseGold,
        UseDiamond,
        UpgradeChest
    }
    public Type type;

    public float Goal;
    public float Current;

    public bool IsDone;

    public bool CheckDoneMission()
    {
        if (Current < Goal)
            return false;

        IsDone = true;
        return true;
    }

    public void EarnReward()
    {
        if (IsDone == false)
            return;

        reward.Earning();

    }
    public void SetCurrent(float current)
    {
        this.Current = current;
        CheckDoneMission();
    }

    public void AddCurrent(float mount)
    {
        this.Current += mount;
        CheckDoneMission();
    }

    public string GetProgress()
    {
        return Current + "/" + Goal;
    }
}
