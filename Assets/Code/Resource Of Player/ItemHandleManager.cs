using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandleManager : MonoBehaviour
{
    public static ItemHandleManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    public void Using(Item.Type type)
    {
        switch(type)
        {
            case Item.Type.SmallExpPotion:
                AllySingleton.Instance.detailAllyPanel.UsingExpPotion(Item.Type.SmallExpPotion, 100);
                break;

            case Item.Type.MediumExpPotion:
                AllySingleton.Instance.detailAllyPanel.UsingExpPotion(Item.Type.MediumExpPotion, 500);
                break;

            case Item.Type.LargeExpPotion:
                AllySingleton.Instance.detailAllyPanel.UsingExpPotion(Item.Type.LargeExpPotion, 2000);
                break;

            case Item.Type.SuperExpPotion:
                AllySingleton.Instance.detailAllyPanel.UsingExpPotion(Item.Type.SuperExpPotion, 10000);
                break;

            default:
                break;
        }
    }
}
