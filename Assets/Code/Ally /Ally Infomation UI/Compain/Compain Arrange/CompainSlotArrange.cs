using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CompainSlotArrange : CompainSlot, IDropHandler, IPointerDownHandler
{
    public CompainPanelArrange compainPanelArrange;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (this.character == null)
        {
            InformManager.Instance.Initialize_FloatingInform("Dont have character in this slot!");
            compainPanelArrange.CompainSlotClick(null, -1);
        }
        else
        {
            compainPanelArrange.CompainSlotClick(this.CharacterImage.sprite, index);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        compainPanelArrange.SwapCompainer(index);
    }
}
