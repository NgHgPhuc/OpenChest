using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllyPanel : MonoBehaviour
{
    public Transform AllyListTranform;
    List<AllyObject> AllyList = new List<AllyObject>();
    List<GameObject> hoizontals = new List<GameObject>();
    int Top = 0;
    int Bot = 2;

    void Attr()
    {
        if (AllyList.Count == 0)
            for (int i = 0; i < AllyListTranform.childCount; i++)
            {
                Transform Horizontal = AllyListTranform.GetChild(i);
                hoizontals.Add(Horizontal.gameObject);

                for (int j = 0; j < Horizontal.childCount; j++)
                    AllyList.Add(Horizontal.GetChild(j).GetComponent<AllyObject>());

                if (i > Bot)
                    Horizontal.gameObject.SetActive(false);
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

    public void InShowZone()
    {
        RectTransform r = (RectTransform)AllyListTranform.parent;
        RectTransform items = (RectTransform)hoizontals[Top].transform;
        float len = AllyListTranform.parent.position.y + r.rect.height/2 + items.rect.height/2;

        if (hoizontals[Top].transform.position.y > len)
        {
            hoizontals[Top].SetActive(false);
            Top += 1;
            hoizontals[Bot + 1].SetActive(true);
            Bot += 1;

            return;
        }

        if (Top == 0)
            return;

        if(hoizontals[Top-1].transform.position.y < len)
        {
            hoizontals[Top-1].SetActive(true);
            Top -= 1;
            hoizontals[Bot].SetActive(false);
            Bot -= 1;

            return;
        }

    }
}
