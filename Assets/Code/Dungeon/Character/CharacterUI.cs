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
            CharacterIcon = transform.Find("Character Icon").GetComponent<Image>();

        gameObject.SetActive(true);
        CharacterIcon.sprite = character.Icon;
    }

    public void NoneData()
    {
        gameObject.SetActive(false);
    }
}
