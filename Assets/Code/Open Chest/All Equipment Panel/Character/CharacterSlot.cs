using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;

public class CharacterSlot : MonoBehaviour,IPointerClickHandler
{
    Image BackgroundSlot;
    Image EquipmentImage;
    Image AddIcon;
    TextMeshProUGUI EquipmentLevel;
    List<GameObject> StarList = new List<GameObject>();
    public Character character;

    void Start()
    {

    }

    public void SetCharacterInSlot(Character character)
    {
        if (character == null)
        {
            gameObject.SetActive(false);
            return;
        }
        this.character = character.Clone();
        gameObject.SetActive(true);

        SetAttr();
        this.EquipmentImage.sprite = this.character.Icon;
        this.EquipmentLevel.SetText("lv." + this.character.Level);
        for (int i = 0; i < StarList.Count; i++)
        {
            StarList[i].SetActive(true);
            StarList[i].GetComponent<Image>().color = (i < character.StarCount) ? Color.white : Color.black;
        }
        this.AddIcon.gameObject.SetActive(false);
    }
    public void DontHaveCharacterInSlot()
    {
        this.character = null;
        gameObject.SetActive(true);
        this.EquipmentImage.sprite = null;
        this.EquipmentLevel.SetText("");
        this.AddIcon.gameObject.SetActive(true);
        for (int i = 0; i < StarList.Count; i++)
            StarList[i].SetActive(false);
    }

    void SetAttr()
    {
        if (EquipmentImage == null || EquipmentLevel == null || BackgroundSlot == null)
        {
            EquipmentImage = transform.Find("Equipment Image").GetComponent<Image>();
            EquipmentLevel = transform.Find("Equipment Level").GetComponent<TextMeshProUGUI>();
            BackgroundSlot = transform.Find("Background Slot").GetComponent<Image>();
            AddIcon = transform.Find("Add Icon").GetComponent<Image>();

            Transform starPanel = transform.Find("Star Panel");
            for (int i = 0; i < starPanel.childCount; i++)
                StarList.Add(starPanel.GetChild(i).gameObject);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.character.Icon == null)
        {
            WardPanel.Instance.AllyWarp();
            return;
        }

        CharInfoPanel.Instance.ShowInfo(this.character);
    }

}
