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

public class LoginDataWhenLogin : MonoBehaviour
{
    public GameObject LoadingPanel;
    public TextMeshProUGUI LoadingTMP;
    public Slider LoadingProgress;
    GetUserDataResult DataReceive;

    public float LoadingTimes;

    int CurrentLoading = 0;

    string LoadingState;
    int index = 5;

    public TemporaryData temporaryData;

    public static LoginDataWhenLogin Instance { get; private set; }
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
        print(username + "-" + password);
        LoginAccount(username, password);

        LoadingProgress.maxValue = 78;
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
        StartCoroutine(LoadResource());
    }
    IEnumerator LoadResource()
    {
        //load money
        StartCoroutine(LoadMoney());


        //Update Equipment
        for (int i = 1; i <= 12; i++)
        {
            Equipment equipment = new Equipment();
            string type = ((Equipment.Type)i).ToString();
            string data = "";
            ShowStateLoading("Loading Equipement " + type + ".....");

            if (DataReceive.Data.ContainsKey(type))
                data = DataReceive.Data[type].Value;
            else
            {
                data = "0-0-0-0-0-0";
                SaveData(type, data);
            }

            Equip(equipment.ExtractStringData(i, data));

            print("Equipment");
            yield return new WaitForSeconds(LoadingTimes);
        }


        //load Ally Data
        List<AllySO> charRawData = new List<AllySO>(Resources.LoadAll<AllySO>("Character Raw"));
        List<AllySO> charStoreData = new List<AllySO>(Resources.LoadAll<AllySO>("Character"));
        for (int i = 0; i < charStoreData.Count; i++)
        {
            ShowStateLoading("Loading Ally.....");

            charStoreData[i].character = charRawData[i].character.Clone();

            string AllyName = charStoreData[i].character.Name;
            if (DataReceive.Data.ContainsKey(AllyName))
            {
                string data = DataReceive.Data[AllyName].Value;
                charStoreData[i].character.ExtractStringData(DataReceive.Data[AllyName].Value);
            }
            //else
            //    SaveData(AllyName, aSO.character.ToStringData());

            yield return new WaitForSeconds(LoadingTimes);
        }


        //Set Ally to use in future
        //AllyOwnManager.Instance.SetAllAlly(charStoreData);

        //load mission
        MissionManager.Instance.LoadNextMission();

        //load all skill ==============
        List<BaseSkill> skills = new List<BaseSkill>(Resources.LoadAll<BaseSkill>("Skill/"));
        foreach (BaseSkill s in skills)
        {
            ShowStateLoading("Loading Skill.....");

            //if (DataReceive.Data.ContainsKey(s.Name))
            //    s.ExtractString(DataReceive.Data[s.Name].Value);

            yield return new WaitForSeconds(LoadingTimes);
        }


        //load equip skill
        List<string> skillsNameData = new List<string>();
        for (int i = 1; i <= 3; i++)
        {
            if (DataReceive.Data.ContainsKey("Skill " + i + " Slot"))
                skillsNameData.Add(DataReceive.Data["Skill " + i + " Slot"].Value);
            else
                SaveData("Skill " + i + " Slot", "");
        }
        PlayerManager.Instance.LoadSkillData(skillsNameData);

        print("All Loading:" + CurrentLoading);
        LoadingPanel.GetComponent<Animator>().Play("Loading Screen Ending");
        Invoke("Ending", 1.5f);
    }

    public void Ending()//call in animation Loading Screen Ending
    {
        LoadingPanel.SetActive(false);
    }

    void Equip(Equipment equipment)
    {
        if (equipment == null)
            return;

        EquipmentPanelManager.Instance.LoadEquipment(equipment);
    }

    //SAVE
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

    IEnumerator LoadMoney()
    {
        temporaryData.LoadAllItemSO();
        foreach (KeyValuePair<Item.Type, Slot> kvp in temporaryData.inventorys)
        {
            string Str = "Loading " + kvp.Key.ToString() + ".....";
            ShowStateLoading(Str);
            if (!DataReceive.Data.ContainsKey(kvp.Key.ToString()))
            {
                switch (kvp.Key)
                {
                    case Item.Type.PlayerLevel:
                        SaveData(kvp.Key.ToString(), "1");
                        temporaryData.LoadValueFromCloud(kvp.Key, 1);
                        break;

                    case Item.Type.NeedExp:
                        SaveData(kvp.Key.ToString(), "100");
                        temporaryData.LoadValueFromCloud(kvp.Key, 100);
                        break;

                    case Item.Type.CurrentExp:
                        SaveData(kvp.Key.ToString(), "0");
                        temporaryData.LoadValueFromCloud(kvp.Key, 0);
                        break;

                    case Item.Type.Chest:
                        SaveData(kvp.Key.ToString(), "20");
                        temporaryData.LoadValueFromCloud(kvp.Key, 20);
                        break;

                    case Item.Type.Gold:
                        SaveData(kvp.Key.ToString(), "100");
                        temporaryData.LoadValueFromCloud(kvp.Key, 100);
                        break;

                    case Item.Type.Diamond:
                        SaveData(kvp.Key.ToString(), "10");
                        temporaryData.LoadValueFromCloud(kvp.Key, 10);
                        break;

                    default:
                        SaveData(kvp.Key.ToString(), "0");
                        temporaryData.LoadValueFromCloud(kvp.Key, 0);
                        break;
                }
            }
            else
            {
                float Mount = (float)Convert.ToDouble(DataReceive.Data[kvp.Key.ToString()].Value);
                temporaryData.LoadValueFromCloud(kvp.Key, Mount);
            }

            yield return new WaitForSeconds(LoadingTimes);
        }
        ResourceManager.Instance.UpdateShowUI();
    }

    void ShowStateLoading(string msg)
    {
        string Str = msg;
        string subStr = Str.Substring(0, Str.Length - index);
        index = (index > 0) ? index -= 1 : 5;

        LoadingTMP.SetText(subStr);

        LoadingProgress.value = CurrentLoading++;
    }


}
