using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ResourceManager : MonoBehaviour
{
    public TextMeshProUGUI GoldMount;
    public TextMeshProUGUI DiamondMount;
    public TextMeshProUGUI Progress;
    public TextMeshProUGUI Level;
    public TextMeshProUGUI ChestCount;

    public FloatingObject floatingObject;

    public static ResourceManager Instance { get; private set; }
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

        UpdateShowUI();
    }

    public void UpdateShowUI()
    {
        GoldMount.SetText(DataManager.Instance.temporaryData.GetValue_String(Item.Type.Gold));
        DiamondMount.SetText(DataManager.Instance.temporaryData.GetValue_String(Item.Type.Diamond));

        float CurrentExp = DataManager.Instance.temporaryData.GetValue_Float(Item.Type.CurrentExp);
        float NeedExp = DataManager.Instance.temporaryData.GetValue_Float(Item.Type.NeedExp);
        float progress = CurrentExp * 100 / NeedExp;
        Progress.SetText(Math.Round(progress, 1) + "%");

        Level.SetText(DataManager.Instance.temporaryData.GetValue_String(Item.Type.PlayerLevel));

        ChestCount.SetText(DataManager.Instance.temporaryData.GetValue_String(Item.Type.Chest));
    }

    public void ChangeGold(float Mount, TemporaryData.ChangeType changeType)
    {
        DataManager.Instance.ChangeValue(Item.Type.Gold, Mount, changeType);

        ShowFloating(Mount, changeType, GoldMount.transform);
        UpdateShowUI();

        MissionManager.Instance.DoingMission_UseGold(Mount);
    }

    public void ChangeDiamond(float Mount, TemporaryData.ChangeType changeType)
    {
        DataManager.Instance.ChangeValue(Item.Type.Diamond, Mount, changeType);

        ShowFloating(Mount, changeType, DiamondMount.transform);
        UpdateShowUI();

        MissionManager.Instance.DoingMission_UseDiamond(Mount);
    }

    public void ChangeExp(float Mount, TemporaryData.ChangeType changeType)
    {
        DataManager.Instance.ChangeValue(Item.Type.CurrentExp, Mount, changeType);

        ShowFloating(Mount, changeType, Progress.transform);
        UpdateShowUI();

        if (DataManager.Instance.temporaryData.GetValue_Float(Item.Type.CurrentExp) > DataManager.Instance.temporaryData.GetValue_Float(Item.Type.NeedExp))
            LevelUp();

    }
    public void LevelUp()
    {
        DataManager.Instance.ChangeValue(Item.Type.PlayerLevel, 1, TemporaryData.ChangeType.ADDING);

        float NeedExp = DataManager.Instance.temporaryData.GetValue_Float(Item.Type.NeedExp);
        int PlayerLevel = DataManager.Instance.temporaryData.GetValue_Int(Item.Type.PlayerLevel);

        ChangeExp(NeedExp, TemporaryData.ChangeType.USING);
        float IncreaseNeedExp = NeedExp * (10 + PlayerLevel) / 100;
        DataManager.Instance.ChangeValue(Item.Type.NeedExp, IncreaseNeedExp, TemporaryData.ChangeType.ADDING);

        Level.SetText(PlayerLevel.ToString());

        MissionManager.Instance.DoingMission_LevelUp();
    }

    public void ChangeChest(float Mount, TemporaryData.ChangeType changeType)
    {
        if(DataManager.Instance.temporaryData.GetValue_Int(Item.Type.Chest) <= 0)
            OpenChestPanel.Instance.InitializeChest();

        DataManager.Instance.ChangeValue(Item.Type.Chest, Mount, changeType);

        ShowFloating(Mount, changeType, ChestCount.transform);
        UpdateShowUI();
    }

    public void ChangeExpPotion(Item.Type type,float Mount, TemporaryData.ChangeType changeType)
    {
    }

    void ShowFloating(float Mount, TemporaryData.ChangeType changeType, Transform trans)
    {
        FloatingObject f = Instantiate(floatingObject, trans.position, trans.rotation, trans);
        switch (changeType)
        {
            case TemporaryData.ChangeType.USING:
                f.Iniatialize("-" + Mount.ToString(), UnityEngine.Color.red, "Floating Down");
                break;
            case TemporaryData.ChangeType.ADDING:
                f.Iniatialize("+" + Mount, UnityEngine.Color.green, "Floating On");
                break;

            default:
                break;
        }
    }
}
