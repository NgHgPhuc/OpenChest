using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CompainPanel : MonoBehaviour
{
    //public List<CompainSlot> compainSlotUIs = new List<CompainSlot>();

    //public DetailAllyPanel detailAllyPanel;
    //Character AllyChosing;

    //public TextMeshProUGUI CompainText;
    //public Image CompainIcon;

    //bool IsChosing;

    //public Image dragObject;
    //int indexDragged;


    //void Start()
    //{

    //}

    //public void CompainButton()
    //{
    //    if (detailAllyPanel.character.IsInTeam == false)
    //    {
    //        ChooseAllyToChange();
    //        return;
    //    }

    //    //if have
    //    string charName = detailAllyPanel.character.Name;
    //    int index = AllyOwnManager.Instance.GetAllAlly()[charName].PositionInTeam;
    //    TeamManager.Instance.RemoveAlly(index);
    //    CompainText.SetText("Compain");
    //    CompainIcon.gameObject.SetActive(false);

    //    IsChosing = true;
    //}

    //public void ChooseAllyToChange()
    //{
    //    gameObject.SetActive(true);

    //    for (int i = 0; i < 4; i++)
    //    {
    //        Character c = TeamManager.Instance.GetAllyInTeam()[i];
    //        if (c != null)
    //            compainSlotUIs[i].SetCharacterInSlot(c);
    //        else
    //            compainSlotUIs[i].DontHaveCharacterInSlot();
    //    }

    //    AllyChosing = detailAllyPanel.character;
    //}

    //public void BackgroundClick()
    //{
    //    gameObject.SetActive(false);
    //}

    //public void AllyCompainClick(int index)
    //{
    //    if (IsChosing == false)
    //        return;

    //    compainSlotUIs[index - 1].SetCharacterInSlot(AllyChosing);
    //    TeamManager.Instance.GetAllyInTeam()[index - 1] = AllyChosing.Clone();
    //    TeamManager.Instance.GetAllyInTeam()[index - 1].IsInTeam = true;
    //    IsChosing = false;
    //    InformManager.Instance.Initialize_FloatingInform("Compain successfully ally!");
    //    CompainText.SetText("Uncompain");
    //    CompainIcon.gameObject.SetActive(true);
    //}


    //public void CompainSlotClick(Sprite sprite,int index)
    //{
    //    dragObject.sprite = sprite;
    //    indexDragged = index;
    //}

    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    if(dragObject.sprite != null)
    //        dragObject.gameObject.SetActive(true);
    //}
    //public void OnDrag(PointerEventData eventData)
    //{
    //    if (dragObject.sprite != null)
    //        dragObject.transform.position = eventData.position;
    //}
    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    if (dragObject.sprite != null)
    //        dragObject.gameObject.SetActive(false);
    //}

    //public void SwapCompainer(int indexSwaping)
    //{
    //    if (indexDragged == -1)
    //        return;

    //    Character t = compainSlotUIs[indexDragged - 1].character.Clone();
    //    compainSlotUIs[indexDragged - 1].SetCharacterInSlot(compainSlotUIs[indexSwaping - 1].character);
    //    compainSlotUIs[indexSwaping - 1].SetCharacterInSlot(t);
    //    indexDragged = -1;
    //}
}
