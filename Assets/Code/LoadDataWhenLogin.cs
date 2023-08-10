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
using Unity.VisualScripting;
using UnityEditor.U2D.Path;

public class LoadDataWhenLogin : MonoBehaviour
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

    public static LoadDataWhenLogin Instance { get; private set; }
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

        LoadingProgress.maxValue = 78;
        LoadingProgress.value = 0;
    }

    public void LoadData()
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
        LoadingPanel.SetActive(true);
        Invoke("LoadAllData", 0.1f);
    }

    void LoadAllData()
    {
        StartCoroutine(LoadResource());
    }

    public void Ending()//call in animation Loading Screen Ending
    {
        LoadingPanel.SetActive(false);
        SceneManager.LoadScene("Open Chest");
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
    #region Save Status Handle
    private void OnSaveFail(PlayFabError obj)
    {
    }
    private void OnSaveSuccess(UpdateUserDataResult obj)
    {
    }
    #endregion


    IEnumerator LoadResource()
    {
        temporaryData.LoadAllItemSO();
        foreach (KeyValuePair<Item.Type, Slot> kvp in temporaryData.inventorys)
        {
            ShowStateLoading("Loading " + kvp.Key.ToString() + ".....");
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

        yield return StartCoroutine(LoadEquipment());
    }
    IEnumerator LoadEquipment()
    {
        //Update Equipment
        for (int i = 1; i <= 12; i++)
        {
            Equipment equipment = new Equipment();
            string type = ((Equipment.Type)i).ToString();
            string data = "";
            ShowStateLoading("Loading Equipement " + type + ".....");

            EquipmentSO equipmentSO = Resources.Load<EquipmentSO>("Equipment/" + type);
            if (DataReceive.Data.ContainsKey(type))
            {
                data = DataReceive.Data[type].Value;
                equipmentSO.equipment = equipment.ExtractStringData(i, data);
                equipmentSO.IsNull = false;
            }
            else
            {
                data = "AttackDamage:0-HealthPoint:0-DefensePoint:0-Speed:0-quality:0-Level:0";
                //SaveData(type, data);
                equipmentSO.equipment = equipment.ExtractStringData(i, data);
                equipmentSO.IsNull = true;
            }

            yield return new WaitForSeconds(LoadingTimes);
        }
        yield return StartCoroutine(LoadAlly());
    }

    IEnumerator LoadAlly()
    {
        //load Ally Data
        List<AllySO> charRawData = new List<AllySO>(Resources.LoadAll<AllySO>("Character Raw"));
        List<AllySO> charStoreData = new List<AllySO>(Resources.LoadAll<AllySO>("Character"));
        for (int i = 0; i < charStoreData.Count; i++)
        {
            charStoreData[i].character = charRawData[i].character.Clone();
            string AllyName = charStoreData[i].character.Name;

            ShowStateLoading("Loading Ally " + AllyName+ " .....");

            if (DataReceive.Data.ContainsKey(AllyName))
            {
                string data = DataReceive.Data[AllyName].Value;
                charStoreData[i].character.ExtractStringData(DataReceive.Data[AllyName].Value);
            }

            yield return new WaitForSeconds(LoadingTimes);
        }
        yield return StartCoroutine(LoadMission());
    }

    IEnumerator LoadMission()
    {
        yield return StartCoroutine(LoadSkill());
    }

    IEnumerator LoadSkill()
    {
        //load all skill ==============
        List<BaseSkill> skills = new List<BaseSkill>(Resources.LoadAll<BaseSkill>("Skill/"));
        foreach (BaseSkill s in skills)
        {
            ShowStateLoading("Loading Skill " + s.Name + " .....");

            string data = "";
            if (DataReceive.Data.ContainsKey(s.Name))
            {
                data = DataReceive.Data[s.Name].Value;
            }
            else
            {
                data = "CurrentSharp:0-IsHave:false-IsEquip:false-SlotEquipIndex:0";
            }
            s.ExtractString(data);
            yield return new WaitForSeconds(LoadingTimes);
        }


        ////load equip skill
        //List<string> skillsNameData = new List<string>();
        //for (int i = 1; i <= 3; i++)
        //{
        //    if (DataReceive.Data.ContainsKey("Skill " + i + " Slot"))
        //        skillsNameData.Add(DataReceive.Data["Skill " + i + " Slot"].Value);
        //    else
        //        SaveData("Skill " + i + " Slot", "");
        //}
        //PlayerManager.Instance.LoadSkillData(skillsNameData);

        print("All Loading:" + CurrentLoading);
        LoadingPanel.GetComponent<Animator>().Play("Loading Screen Ending");
        Invoke("Ending", 1.5f);
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
