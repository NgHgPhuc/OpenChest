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

public class FightingDataManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnGetDataSuccess, OnGetDataFail);
    }
    private void OnGetDataFail(PlayFabError obj)
    {
        print(obj.ToString());
    }

    private void OnGetDataSuccess(GetUserDataResult DataReceive)
    {
        print(DataReceive.Data["Gold"]);
    }
}
