using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProrityPanel : MonoBehaviour
{
    List<Transform> PriorityList = new List<Transform>();

    public static ProrityPanel Instance { get; private set; }

    Image Current;
    private void Awake()
    {
        for(int i = 0; i < 6; i++)
        {
            PriorityList.Add(transform.GetChild(i));
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
        for (int i = 0 ; i < 6; i++)
        {
            if (i < c)
            {
                PriorityList[i].gameObject.SetActive(true);
                PriorityList[i].Find("Icon").GetComponent<Image>().sprite = fightingUnitList[i].CharacterClone.Icon;
                if (fightingUnitList[i].stateFighting == FightingUnit.StateFighting.Death)
                    PriorityList[i].Find("Icon").GetComponent<Image>().color = Color.gray;
            }
            else
                PriorityList[i].gameObject.SetActive(false);
        }
    }

    public void SetUnitTurn(int index)
    {
        if(Current != null)
            Current.color = Color.white;

        Current = PriorityList[index].GetComponent<Image>();
        Current.color = Color.green;

    }
}
