using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public Item item { get; private set; }
    Image Icon;
    int HaveNumber;
    TextMeshProUGUI HaveNumberText;
    int UsingNumber;
    TextMeshProUGUI UsingNumberText;

    void SetAttr()
    {
        if(HaveNumberText == null)
            HaveNumberText = transform.Find("Have Number").GetComponent<TextMeshProUGUI>();

        if (UsingNumberText == null)
            UsingNumberText = transform.Find("Using Number").GetComponent<TextMeshProUGUI>();

        if(Icon == null)
            Icon = transform.Find("Icon").GetComponent<Image>();
    }

    public void SetItem(Slot slot)
    {
        SetAttr();

        this.item = slot.item;
        this.HaveNumber = (int)slot.Mount;
        this.HaveNumberText.SetText(this.HaveNumber.ToString());
        this.UsingNumber = 0;
        this.UsingNumberText.SetText(this.UsingNumber.ToString());
        this.UsingNumberText.gameObject.SetActive(false);
        this.Icon.sprite = this.item.Icon;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(this.HaveNumber <= 0)
        {
            InformManager.Instance.Initialize_FloatingInform("You dont have any more!");
            return;
        }
        this.HaveNumber -= 1;
        this.HaveNumberText.SetText(this.HaveNumber.ToString());

        this.UsingNumber += 1;
        this.UsingNumberText.SetText("-" + this.UsingNumber.ToString());
        this.UsingNumberText.gameObject.SetActive(true);

        ItemHandleManager.Instance.Using(this.item.type);

    }
}
