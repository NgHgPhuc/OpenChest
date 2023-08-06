using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CompainSlotChosen : CompainSlot, IPointerClickHandler
{
    public CompainPanelChosen compainPanelChosen;

    public void OnPointerClick(PointerEventData eventData)
    {
        print(gameObject.name + " Click");
        compainPanelChosen.AllyCompainClick(index);
    }

}
