using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BundleObject : MonoBehaviour,IPointerClickHandler
{
    public float GetValue;
    public float Cost;
    void Start()
    {
        
    }

    public void BuyGold_by_Diamond()
    {
        string paragraph = "Are you sure buying " + GetValue + " Gold with " + Cost + " Diamond?\n If you confirm, both of us are happy happy happyyy!";
        InformManager.Instance.Initialize_QuestionObject("Buy gold", paragraph, () =>
        {
            if (!ResourceManager.Instance.CheckEnough_Diamond(Cost))
            {
                InformManager.Instance.Initialize_FloatingInform("You dont enough diamond");
                return;
            }

            InformManager.Instance.Initialize_FloatingInform("Buying successfully");
            ResourceManager.Instance.ChangeDiamond(-Cost);
            ResourceManager.Instance.ChangeGold(GetValue);
        });
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        BuyGold_by_Diamond();
    }
}
