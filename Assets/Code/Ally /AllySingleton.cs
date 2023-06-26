using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AllySingleton : MonoBehaviour
{
    AllyPanel allyPanel;
    DetailAllyPanel detailAllyPanel;

    public static AllySingleton Instance { get; private set; }

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
    }

    void Start()
    {
        allyPanel = transform.Find("Ally UI Panel").GetComponent<AllyPanel>();
        detailAllyPanel = transform.Find("Detail Ally Panel"). GetComponent<DetailAllyPanel>();
        Initialize();
    }

    public void Initialize()
    {
        detailAllyPanel.gameObject.SetActive(false);
        allyPanel.gameObject.SetActive(true);
        allyPanel.Initialize();
    }

    public void ShowDetailAlly(Character character,int index)
    {
        allyPanel.gameObject.SetActive(false);
        detailAllyPanel.gameObject.SetActive(true);
        detailAllyPanel.SetDetail(character,index);
    }
}
