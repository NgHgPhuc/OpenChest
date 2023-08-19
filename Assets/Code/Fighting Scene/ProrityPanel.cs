using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProrityPanel : MonoBehaviour
{
    List<Image> BackgroundList = new List<Image>();
    List<Image> IconList = new List<Image>();
    public static ProrityPanel Instance { get; private set; }

    Image Current;
    private void Awake()
    {
        for(int i = 0; i <= 7; i++)
        {
            Transform backgroundTrans = transform.GetChild(i).GetChild(1);
            Image backgroundImage = backgroundTrans.GetComponent<Image>();
            BackgroundList.Add(backgroundImage);

            Image IconImage = backgroundTrans.GetChild(0).GetComponent<Image>();
            IconList.Add(IconImage);
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
                BackgroundList[i].transform.parent.parent.gameObject.SetActive(true);
                IconList[i].sprite = fightingUnitList[i].CharacterClone.Avatar;
                if (fightingUnitList[i].stateFighting == FightingUnit.StateFighting.Death)
                    BackgroundList[i].color = Color.gray;
            }
            else
                BackgroundList[i].transform.parent.parent.gameObject.SetActive(false);
        }
    }

    public void SetUnitTurn(int index)
    {
        if(Current != null)
            Current.color = Color.white;

        BackgroundList[index].color = Color.green;
        Current = BackgroundList[index];
    }
}
