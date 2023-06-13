using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChestHandlerManager : MonoBehaviour
{
    public GetEquipmentPanel getEquipmentPanel;
    public static ChestHandlerManager Instance { get; private set; }
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


    public void RandomEquipment()
    {
        Equipment NewEquipment = ChestManager.Instance.RandomEquipment();

        getEquipmentPanel.gameObject.SetActive(true);
        getEquipmentPanel.SetNewEquipment(NewEquipment);

        Equipment OldEquipment = EquipmentPanelManager.Instance.GetEquipmentSlot(NewEquipment.type);
        getEquipmentPanel.SetOldEquipment(OldEquipment);


    }
}
