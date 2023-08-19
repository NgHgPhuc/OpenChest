using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using Unity.VisualScripting.Antlr3.Runtime.Collections;

public class AllyObject : MonoBehaviour, IPointerClickHandler
{
    public Character character;
    public int index;

    public Image Background;
    public Image Icon;
    public Image Shadow;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Level;
    public StarListPanel StarList;
    public Image TierEffect;
    public Image Role;
    public GameObject LockIcon;
    public Slider SharpBarProgress;
    public TextMeshProUGUI TextProgress;
    public GameObject InTeamIcon;

    public CanvasGroup canvasGroup;

    public void SetAlly(Character character,int index)
    {
        if (character == null)
            return;

        this.character = character;
        this.index = index;

        if (character.IsOwn)
            OpenAllyUI();
        else
            LockAllyUI();

        InTeamIcon.SetActive(character.IsInTeam);

        Icon.sprite = this.character.Icon;
        Shadow.sprite = this.character.Icon;
        Name.SetText(this.character.Name);
        Level.SetText("Lv. " + this.character.Level.ToString());

        int currentStarCount = character.currentStarCount;
        int maxStarCount = character.maxStarCount;
        StarList.SetStarCount(currentStarCount, maxStarCount, 5);

        TierEffect.color = character.GetColor();
        Role.sprite = Resources.Load<Sprite>("Character Role/" + character.role);

        int currentSharp = character.CurrentSharp;
        int needSharp = character.NeedSharp;
        TextProgress.SetText(currentSharp + "/" + needSharp);
        SharpBarProgress.value = (float)currentSharp / needSharp;
    }

    void OpenAllyUI()
    {
        Level.gameObject.SetActive(true);
        Icon.color = Color.white;
        LockIcon.SetActive(false);
        Background.color = new Color(161f / 255, 200f / 255, 224f / 255);
        canvasGroup.alpha = 1f;
    }
    void LockAllyUI()
    {
        Level.gameObject.SetActive(false);
        Icon.color = new Color(180f / 255, 180f / 255, 180f / 255);
        LockIcon.SetActive(true);
        Background.color = new Color(120f / 255, 120f / 255, 120f / 255);
        canvasGroup.alpha = 0.85f;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AllySingleton.Instance.ShowDetailAlly(this.character,this.index);
    }
}
