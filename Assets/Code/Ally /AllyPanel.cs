using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllyPanel : MonoBehaviour
{
    public Transform AllyListTranform;
    List<AllyObject> AllyList = new List<AllyObject>();

    void Start()
    {
        
    }

    void Attr()
    {
        if (AllyList.Count == 0)
            for (int i = 0; i < AllyListTranform.childCount; i++)
            {
                Transform Horizontal = AllyListTranform.GetChild(i);

                for (int j = 0; j < Horizontal.childCount; j++)
                    AllyList.Add(Horizontal.GetChild(j).GetComponent<AllyObject>());
            }
    }

    void UpdateAllyShow()
    {
        Attr();

        Dictionary<string, Character> allies = new Dictionary<string, Character>(AllyOwnManager.Instance.GetAllAlly());
        for (int i = 0; i < AllyList.Count; i++)
            if(i < allies.Count)
            {
                AllyList[i].gameObject.SetActive(true);
                AllyList[i].SetAlly(allies.ElementAt(i).Value,i);
            }
            else
                AllyList[i].gameObject.SetActive(false);

    }

    public void Initialize()
    {
        gameObject.SetActive(true);
        UpdateAllyShow();
    }
}
