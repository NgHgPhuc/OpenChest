using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChestHandlerManager : MonoBehaviour
{
    public GetEquipmentPanel getEquipmentPanel;
    public static ChestHandlerManager Instance { get; private set; }

    int ChestCount;
    public TextMeshProUGUI ChestCountShow;
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
    void Start()
    {
    }

    public void LoadChest()
    {
        ChestCount = Convert.ToInt32(PlayerPrefs.GetString("ChestCount"));
        ChestCountShow.SetText(ChestCount.ToString());
    }


    public void RandomEquipment()
    {
        if (ChestCount <= 0)
        {
            InformManager.Instance.Initialize_FloatingInform("You dont have anymore chest");
            return;
        }

        Equipment NewEquipment = ChestManager.Instance.RandomEquipment();

        getEquipmentPanel.gameObject.SetActive(true);
        getEquipmentPanel.animator.Play("Open");
        getEquipmentPanel.SetNewEquipment(NewEquipment);

        Equipment OldEquipment = EquipmentPanelManager.Instance.GetEquipment(NewEquipment.type);
        getEquipmentPanel.SetOldEquipment(OldEquipment);

        getEquipmentPanel.NewPanel.SetActive(true);

        ChestCount -= 1;
        ChestCountShow.SetText(ChestCount.ToString());

        DataManager.Instance.SaveData("ChestCount", ChestCount.ToString());
    }

    public void GetChest(int Mount)
    {
        ChestCount += Mount;
        ChestCountShow.SetText(ChestCount.ToString());
        DataManager.Instance.SaveData("ChestCount", ChestCount.ToString());
    }
}
