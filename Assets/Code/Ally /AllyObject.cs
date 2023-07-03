using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class AllyObject : MonoBehaviour, IPointerClickHandler
{
    public Character character;
    public int index;
    Image Icon;
    TextMeshProUGUI Name;
    TextMeshProUGUI StarCount;
    TextMeshProUGUI Level;
    Transform StarListPanel;
    List<GameObject> StarShow = new List<GameObject>();
    GameObject TierEffect;
    Image TierEffect_Image;
    Animator TierEffect_Animator;

    CanvasGroup canvasGroup;

    void Attr()
    {
        Icon = transform.Find("Character Panel").Find("Character").GetComponent<Image>();
        Name = transform.Find("Name Panel").GetChild(0).GetComponent<TextMeshProUGUI>();
        StarCount = transform.Find("Star Count").GetComponent<TextMeshProUGUI>();
        Level = transform.Find("Level").GetComponent<TextMeshProUGUI>();
        StarListPanel = transform.Find("Star List Panel");
        TierEffect = transform.Find("Tier Effect").gameObject;
        TierEffect_Image = TierEffect.GetComponent<Image>();
        TierEffect_Animator = TierEffect.GetComponent<Animator>();

        canvasGroup = GetComponent<CanvasGroup>();

        for (int i = 0; i < StarListPanel.childCount; i++)
        {
            GameObject star = StarListPanel.GetChild(i).gameObject;
            if (star != null)
                StarShow.Add(star);
        }

    }

    public void SetAlly(Character character,int index)
    {
        if (character == null)
            return;

        //this.character = character.Clone();
        this.character = character;
        this.index = index;
        Attr();

        if (!character.IsOwn)
            canvasGroup.alpha = 0.7f;
        else
            canvasGroup.alpha = 1f;

        Icon.sprite = this.character.Icon;
        StarCount.SetText(this.character.StarCount.ToString());
        Name.SetText(this.character.Name);
        Name.color = character.GetColor();
        Level.SetText(this.character.Level.ToString());

        for (int i = 0; i < StarListPanel.childCount; i++)
            if (i < this.character.StarCount)
                StarShow[i].GetComponent<Image>().color = Color.white;
            else
                StarShow[i].GetComponent<Image>().color = Color.black;

        if((int)character.tier >= 2)
        {
            TierEffect.SetActive(true);
            TierEffect_Image.color = character.GetColor();
        }
        else
            TierEffect.SetActive(false);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AllySingleton.Instance.ShowDetailAlly(this.character,this.index);
    }
}
