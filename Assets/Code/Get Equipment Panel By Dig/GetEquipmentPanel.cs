using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class GetEquipmentPanel : MonoBehaviour
{
    public SetEquipmentPanel NewEquipmentPanel;
    Equipment NewEquiment;
    public CompareStats compareNewAndOld;
    public SetEquipmentPanel OldEquipmentPanel;
    Equipment OldEquipment;

    public Button DropButton;
    public Button EquipButton;

    void Start()
    {
        DropButton.onClick.AddListener(DropFunc);
        EquipButton.onClick.AddListener(EquipFunc);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNewEquipment(Equipment equipment)
    {
        this.NewEquiment = equipment;
        NewEquipmentPanel.SetEquipment(equipment);
        compareNewAndOld.Compare();
        //try
        //{
        //    compareNewAndOld.Compare();
        //}
        //catch (Exception) { }
    }

    public void SetOldEquipment(Equipment equipment)
    {
        this.OldEquipment = equipment;
        OldEquipmentPanel.SetEquipment(equipment);
        compareNewAndOld.Compare();
    }

    public void DropFunc()
    {
        ResourceManager.Instance.ChangeGold(NewEquiment.PowerPoint);
        this.NewEquiment = null;
        this.OldEquipment = null;
        gameObject.SetActive(false);
    }

    public void EquipFunc()
    {
        EquipmentPanelManager.Instance.SetEquipmentSlot(this.NewEquiment);
        if(this.OldEquipment == null)
        {
            this.NewEquiment = null;
            gameObject.SetActive(false);
            return;
        }
        Swap_OldAndNew();
        SetNewEquipment(this.NewEquiment);
        SetOldEquipment(this.OldEquipment);
        compareNewAndOld.Compare();
    }
    void Swap_OldAndNew()
    {
        Equipment s = new Equipment();
        s = this.NewEquiment;
        this.NewEquiment = this.OldEquipment;
        this.OldEquipment = s;
    }
}
