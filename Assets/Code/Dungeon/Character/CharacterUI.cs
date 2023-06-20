using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterUI : MonoBehaviour
{
    Image Border;
    Image CharacterIcon;
    TextMeshProUGUI Level;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCharacterUI(Character character)
    {
        if (CharacterIcon == null)
            SetAttr();

        gameObject.SetActive(true);
        CharacterIcon.sprite = character.Icon;
        Level.SetText("lv."+character.Level);
    }

    void SetAttr()
    {
        CharacterIcon = transform.Find("Character Icon").GetComponent<Image>();
        Level = transform.Find("Level").GetComponent<TextMeshProUGUI>();
    }

    public void NoneData()
    {
        gameObject.SetActive(false);
    }
}
