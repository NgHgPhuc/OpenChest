using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class TicketClearPanel : MonoBehaviour
{
    public TextMeshProUGUI TicketShow;
    public FloatingObject floatingObject;

    public static TicketClearPanel Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        TicketShow.SetText(DataManager.Instance.temporaryData.GetValue_String(Item.Type.Ticket));
    }

    public void UsingTicket(int Mount)
    {
        DataManager.Instance.temporaryData.ChangeValue(Item.Type.Ticket,Mount,TemporaryData.ChangeType.USING);

        FloatingObject f = Instantiate(floatingObject, TicketShow.transform.position, TicketShow.transform.rotation, transform);
        if (Mount < 0)
            f.Iniatialize(Mount.ToString(), Color.red, "Floating Down");
        else
            f.Iniatialize("+" + Mount, Color.green, "Floating On");

        TicketShow.SetText(DataManager.Instance.temporaryData.GetValue_String(Item.Type.Ticket));
    }

    public bool IsEnoughTicket(int Mount)
    {
        return DataManager.Instance.temporaryData.GetValue_Float(Item.Type.Ticket) > Mount;
    }
}
