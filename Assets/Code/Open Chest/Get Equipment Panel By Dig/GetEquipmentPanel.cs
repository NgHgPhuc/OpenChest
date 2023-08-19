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

    public GameObject NewPanel;

    public Button DropButton;
    public Button EquipButton;

    public Animator animator;

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
        if (this.OldEquipment == null || this.NewEquipment.PowerPoint() > this.OldEquipment.PowerPoint())
        {
            string paragraph = "The new equipment you found have stronger than you old equipment\nAre you sure to drop it?";
            InformManager.Instance.Initialize_QuestionObject("Warning!", paragraph, Drop);
        }
        else Drop();
            
    }
    void Drop()
    {
        if (this.NewEquipment == null)
            return;

        float Mount = NewEquipment.PowerPoint();
        ResourceManager.Instance.ChangeGold(Mount, TemporaryData.ChangeType.ADDING);
        ResourceManager.Instance.ChangeExp(Mount/2, TemporaryData.ChangeType.ADDING);
        this.NewEquipment = null;
        this.OldEquipment = null;
        Close();
    }

    public void EquipFunc()
    {
        EquipmentManager.Instance.EquipNewEquipment(this.NewEquipment);

        if(this.OldEquipment == null)
        {
            this.NewEquipment = null;
            Close();
            return;
        }
        Swap_OldAndNew();
        SetNewEquipment(this.NewEquipment);
        SetOldEquipment(this.OldEquipment);
        compareNewAndOld.Compare();

        NewPanel.SetActive(!NewPanel.activeSelf);
    }
    void Swap_OldAndNew()
    {
        Equipment s = new Equipment();
        s = this.NewEquipment;
        this.NewEquipment = this.OldEquipment;
        this.OldEquipment = s;
    }

    public void Close()
    {
        gameObject.SetActive(false);
        OpenChestPanel.Instance.InitializeChest();
    }
}
