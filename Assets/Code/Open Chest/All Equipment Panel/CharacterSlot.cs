using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterSlot : MonoBehaviour,IPointerClickHandler
{
    Image BackgroundSlot;
    Image EquipmentImage;
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
        if (EquipmentImage == null || EquipmentLevel == null || BackgroundSlot == null)
        {
            EquipmentImage = transform.Find("Equipment Image").GetComponent<Image>();
            EquipmentLevel = transform.Find("Equipment Level").GetComponent<TextMeshProUGUI>();
            BackgroundSlot = transform.Find("Background Slot").GetComponent<Image>();

            Transform starPanel = transform.Find("Star Panel");
            for (int i = 0; i < starPanel.childCount; i++)
                StarList.Add(starPanel.GetChild(i).gameObject);
        }

        this.EquipmentImage.sprite = this.character.Icon;
        this.EquipmentLevel.SetText("lv." + this.character.Level);
        for (int i = 0; i < StarList.Count; i++)
            StarList[i].GetComponent<Image>().color = (i < character.StarCount) ? Color.white : Color.black;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CharInfoPanel.Instance == null)
            return;

        CharInfoPanel.Instance.ShowInfo(this.character);
    }
}
