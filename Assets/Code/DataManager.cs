using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public GameObject LoadingPanel;
    public TextMeshProUGUI LoadingTMP;
    public Slider LoadingProgress;
    GetUserDataResult DataReceive;

    public float LoadingTimes;

    int CurrentLoading = 0;

    string LoadingState;

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

        string username = PlayerPrefs.GetString("Username");
        string password = PlayerPrefs.GetString("Password");
        LoginAccount("qweasd", "qweasd");

        LoadingProgress.maxValue = 50;
        LoadingProgress.value = 0;
    }

    void Start()
    {
        
    }

    public void LoginAccount(string username, string password)
    {
        var request = new LoginWithPlayFabRequest
        {
            Username = username,
            Password = password,
        };

        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFail);
    }

    private void OnLoginFail(PlayFabError obj)
    {
    }

    private void OnLoginSuccess(LoginResult obj)
    {
        LoadData();
    }


    void LoadData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnGetDataSuccess, OnGetDataFail);
    }

    private void OnGetDataFail(PlayFabError obj)
    {
        print(obj.ToString());
    }

    private void OnGetDataSuccess(GetUserDataResult DataReceive)
    {
        this.DataReceive = DataReceive;
        Invoke("LoadAllData", 0.1f);
    }

    void LoadAllData()
    {
        ShowStateLoading("Loading Gold");

        StartCoroutine(LoadResource());

    }
    IEnumerator LoadResource()
    {
        if(PlayerPrefs.GetString("LoadingResourceState") == "Done")
        {
            ShowStateLoading("Loading Gold...");
            PlayerPrefs.SetString("Gold", DataReceive.Data["Gold"].Value);
            yield return new WaitForSeconds(LoadingTimes);

            ShowStateLoading("Loading Diamond...");
            PlayerPrefs.SetString("Diamond", DataReceive.Data["Diamond"].Value);
            yield return new WaitForSeconds(LoadingTimes);

            ShowStateLoading("Loading Level...");
            PlayerPrefs.SetString("Level", DataReceive.Data["Level"].Value);
            yield return new WaitForSeconds(LoadingTimes);

            ShowStateLoading("Loading Current Exp...");
            PlayerPrefs.SetString("CurrentExp", DataReceive.Data["CurrentExp"].Value);
            yield return new WaitForSeconds(LoadingTimes);

            ShowStateLoading("Loading Need Exp...");
            PlayerPrefs.SetString("NeedExp", DataReceive.Data["NeedExp"].Value);
            yield return new WaitForSeconds(LoadingTimes);

            ShowStateLoading("Loading Chest Count...");
            PlayerPrefs.SetString("ChestCount", DataReceive.Data["ChestCount"].Value);
            yield return new WaitForSeconds(LoadingTimes);

            ShowStateLoading("Loading Ticket...");
            PlayerPrefs.SetString("Ticket", DataReceive.Data["Ticket"].Value);
            yield return new WaitForSeconds(LoadingTimes);

            PlayerPrefs.SetString("LoadingResourceState", "Done");
        }

        ResourceManager.Instance.GetData();
        ChestHandlerManager.Instance.LoadChest();

        for (int i = 1; i <= 13; i++)
        {
            Equipment equipment = new Equipment();
            string type = ((Equipment.Type)i).ToString();
            string data = "";
            ShowStateLoading("Loading Equipement " + type + "...");
            
            try
            {
                data = DataReceive.Data[type].Value;
            }
            catch (Exception)
            {
                SaveData(type, "0-0-0-0-0-0");
                data = "0-0-0-0-0-0";
            }

            Equip(equipment.ExtractStringData(i, data));

            yield return new WaitForSeconds(LoadingTimes);
        }


        
        List<AllySO> chars = new List<AllySO>(Resources.LoadAll<AllySO>("Character"));  
        foreach(AllySO aSO in chars)
        {
            ShowStateLoading("Loading Ally...");

            if(DataReceive.Data.ContainsKey(aSO.character.Name))
            {
                try
                {
                    Character c = new Character();
                    string data = DataReceive.Data[aSO.character.Name].Value;
                    c.ExtractStringData(data);
                    aSO.character = c.Clone();
                }
                catch(Exception)
                {
                    string data = "20-100-5-10-"+aSO.character.tier+"-2-"+aSO.character.Name+"-1-0-500-0-50-True-False-0-0-0-0-0-0-1+0-2+0-3+0-4+0-5+0-6+0";
                    Character c = new Character();
                    c.ExtractStringData(data);
                    aSO.character = c.Clone();
                }
            }

            yield return new WaitForSeconds(LoadingTimes);
        }

        AllyOwnManager.Instance.SetAllAlly(chars);

        MissionManager.Instance.LoadNextMission();

        List<BaseSkill> skills = new List<BaseSkill>(Resources.LoadAll<BaseSkill>("Skill/"));
        foreach (BaseSkill s in skills)
        {
            ShowStateLoading("Loading Skill...");

            if (DataReceive.Data.ContainsKey(s.Name))
                s.ExtractString(DataReceive.Data[s.Name].Value);

            yield return new WaitForSeconds(LoadingTimes);
        }

        LoadingPanel.SetActive(false);
    }

    void Equip(Equipment equipment)
    {
        if (equipment == null)
            return;

        EquipmentPanelManager.Instance.LoadEquipment(equipment);
    }





    //SAVE
    public void SaveData(string dataName,string dataSave)
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

    void ShowStateLoading(string msg)
    {
        LoadingState = msg;
        LoadingTMP.SetText(LoadingState);

        LoadingProgress.value = CurrentLoading++;
    }
}
