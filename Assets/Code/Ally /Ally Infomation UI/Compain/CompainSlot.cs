using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CompainSlot : MonoBehaviour
{
    public int index;

    protected Image CharacterImage;
    protected TextMeshProUGUI CharacterName;
    protected StarListPanel starListPanel;
    protected GameObject LightPillar;
    protected GameObject AddIcon;

    public Character character { get; protected set; }

    void Start()
    {

    }

    protected void SetAttr()
    {
        if (CharacterImage == null)
            CharacterImage = transform.Find("Equipment Image").GetComponent<Image>();

        if (CharacterName == null)
            CharacterName = transform.Find("Equipment Level").GetComponent<TextMeshProUGUI>();

        if (starListPanel == null)
            starListPanel = transform.Find("Star List Panel").GetComponent<StarListPanel>();

        if (LightPillar == null)
            LightPillar = transform.Find("Light Pillar").gameObject;

        if (AddIcon == null)
            AddIcon = transform.Find("Add Icon").gameObject;
    }
    public void SetCharacterInSlot(Character character)
    {
        if (character == null)
        {
            DontHaveCharacterInSlot();
            return;
        }

        SetAttr();

        this.LightPillar.SetActive(false);
        this.AddIcon.SetActive(false);

        this.character = character.Clone();

        this.CharacterImage.gameObject.SetActive(true);
        this.CharacterImage.sprite = this.character.Icon;

        this.CharacterName.gameObject.SetActive(true);
        this.CharacterName.SetText(this.character.Name);

        this.starListPanel.gameObject.SetActive(true);
        starListPanel.SetStarCount(character.currentStarCount, 5, 5);
    }

    public void DontHaveCharacterInSlot()
    {
        this.character = null;

        SetAttr();
        this.CharacterImage.gameObject.SetActive(false);
        this.CharacterImage.sprite = null;

        this.CharacterName.gameObject.SetActive(false);

        this.starListPanel.gameObject.SetActive(false);

        this.LightPillar.SetActive(true);
        this.AddIcon.SetActive(true);
    }
}
