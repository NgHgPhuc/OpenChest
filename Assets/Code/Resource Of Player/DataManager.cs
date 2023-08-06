using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Reflection;
using static TemporaryData;
using System.Globalization;

public class DataManager : MonoBehaviour
{
    public float LoadingTimes;

    int CurrentLoading = 0;

    string LoadingState;
    int index = 5;

    public TemporaryData temporaryData;

    public static DataManager Instance { get; private set; }
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

        temporaryData.LoadAllItemSO();
        LoadResource();
    }

    void LoadResource()
    {
        //load mission
        //MissionManager.Instance.LoadNextMission();

        //load all skill
        //List<BaseSkill> skills = new List<BaseSkill>(Resources.LoadAll<BaseSkill>("Skill/"));
        //temporaryData.skillList = skills;

        //load equip skill
        //List<string> skillsNameData = new List<string>();
        //for (int i = 1; i <= 3; i++)
        //{
        //    if (DataReceive.Data.ContainsKey("Skill " + i + " Slot"))
        //        skillsNameData.Add(DataReceive.Data["Skill " + i + " Slot"].Value);
        //    else
        //        SaveData("Skill " + i + " Slot", "");
        //}
        //TeamManager.Instance.LoadSkillData(skillsNameData);

        //print("All Loading:" + CurrentLoading);
        //LoadingPanel.GetComponent<Animator>().Play("Loading Screen Ending");
        //Invoke("Ending", 1.5f);
    }



    #region SAVE DATA IN HERE
    ////SAVE
    
    public void ChangeValue(Item.Type type, float changeMount, ChangeType changeType)
    {
        temporaryData.ChangeValue(type, changeMount, changeType);

        SaveData(type.ToString(), temporaryData.GetValue_String(type));
    }

    public void SaveData(string dataName, string dataSave)
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { dataName,dataSave }
            },
        };

        PlayFabClientAPI.UpdateUserData(request, OnSaveSuccess, OnSaveFail);
    }

    private void OnSaveFail(PlayFabError obj)
    {
    }

    private void OnSaveSuccess(UpdateUserDataResult obj)
    {
    }
    #endregion
}
