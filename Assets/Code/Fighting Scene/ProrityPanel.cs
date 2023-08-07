using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProrityPanel : MonoBehaviour
{
    List<Image> PriorityList = new List<Image>();

    public static ProrityPanel Instance { get; private set; }

    Image Current;
    private void Awake()
    {
        for(int i = 1; i <= 8; i++)
        {
            Transform p = transform.Find("Priority " + i).GetChild(1);
            Image allyImage = p.GetChild(0).GetComponentInChildren<Image>();
            PriorityList.Add(allyImage);
        }

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(List<FightingUnit> fightingUnitList)
    {
        int c = fightingUnitList.Count;
        for (int i = 0 ; i < 8; i++)
        {
            if (i < c)
            {
                PriorityList[i].transform.parent.parent.gameObject.SetActive(true);
                PriorityList[i].sprite = fightingUnitList[i].CharacterClone.Avatar;
                if (fightingUnitList[i].stateFighting == FightingUnit.StateFighting.Death)
                    PriorityList[i].color = Color.gray;
            }
            else
                PriorityList[i].transform.parent.parent.gameObject.SetActive(false);
        }
    }

    public void SetUnitTurn(int index)
    {
        if(Current != null)
            Current.color = Color.white;

        PriorityList[index].color = Color.green;
    }
}
