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
using System.Globalization;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
    public float LoadingTimes;

    int CurrentLoading = 0;

    string LoadingState;
    int index = 5;

    public UnityEvent LoadingEvent;

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
        LoadingEvent?.Invoke();
    }



    #region SAVE DATA IN HERE
    ////SAVE
    
    public void ChangeValue(Item.Type type, float changeMount, TemporaryData.ChangeType changeType)
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
