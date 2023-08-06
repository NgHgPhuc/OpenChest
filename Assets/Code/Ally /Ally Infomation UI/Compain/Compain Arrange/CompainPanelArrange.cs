using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class CompainPanelArrange : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler
{
    public List<CompainSlot> compainSlotUIs = new List<CompainSlot>();

    public Image dragObject;
    Vector3 firstPos;
    int indexDragged;

    void Start()
    {
        firstPos = dragObject.transform.position;
    }

    public void BackgroundClick()
    {
        gameObject.SetActive(false);
    }

    public void CompainArrangeButton()
    {
        gameObject.SetActive(true);

        for (int i = 0; i < 4; i++)
        {
            Character c = TeamManager.Instance.GetAllyInTeam()[i];
            if (c != null)
                compainSlotUIs[i].SetCharacterInSlot(c);
            else
                compainSlotUIs[i].DontHaveCharacterInSlot();
        }
    }

    public void CompainSlotClick(Sprite sprite, int index)
    {
        dragObject.sprite = sprite;
        indexDragged = index;
    }


    public void SwapCompainer(int indexSwaping)
    {
        if (indexDragged == -1)
            return;

        print(indexDragged + "-->" + indexSwaping);
        Character t = compainSlotUIs[indexDragged - 1].character;
        compainSlotUIs[indexDragged - 1].SetCharacterInSlot(compainSlotUIs[indexSwaping - 1].character);
        compainSlotUIs[indexSwaping - 1].SetCharacterInSlot(t);
        TeamManager.Instance.Swap2Allies(indexDragged, indexSwaping);
        indexDragged = -1;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (dragObject.sprite != null)
            dragObject.gameObject.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragObject.sprite != null)
            dragObject.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragObject.sprite != null)
        {
            dragObject.gameObject.SetActive(false);
            dragObject.sprite =  null;
            indexDragged = -1;
        }
        dragObject.transform.position = firstPos;
    }

}
