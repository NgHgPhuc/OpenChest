using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class GetEquipmentPanel : MonoBehaviour
{
    public SetEquipmentPanel NewEquipmentPanel;
    Equipment NewEquipment;
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
        this.NewEquipment = equipment;
        NewEquipmentPanel.SetEquipment(equipment);
        compareNewAndOld.Compare();
    }

    public void SetOldEquipment(Equipment equipment)
    {
        this.OldEquipment = equipment;
        OldEquipmentPanel.SetEquipment(equipment);
        compareNewAndOld.Compare();
    }

    public void DropFunc()
    {
        ResourceManager.Instance.ChangeGold(NewEquipment.PowerPoint);
        ResourceManager.Instance.GainExp(NewEquipment.PowerPoint/2);
        this.NewEquipment = null;
        this.OldEquipment = null;
        gameObject.SetActive(false);
    }

    public void EquipFunc()
    {
        EquipmentPanelManager.Instance.SetEquipmentSlot(this.NewEquipment);
        StatsPanelManager.Instance.Unequip(this.OldEquipment);
        StatsPanelManager.Instance.Equip(this.NewEquipment);

        if(this.OldEquipment == null)
        {
            this.NewEquipment = null;
            gameObject.SetActive(false);
            return;
        }
        Swap_OldAndNew();
        SetNewEquipment(this.NewEquipment);
        SetOldEquipment(this.OldEquipment);
        compareNewAndOld.Compare();
    }
    void Swap_OldAndNew()
    {
        Equipment s = new Equipment();
        s = this.NewEquipment;
        this.NewEquipment = this.OldEquipment;
        this.OldEquipment = s;
    }
}
