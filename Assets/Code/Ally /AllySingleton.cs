using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AllySingleton : MonoBehaviour
{
    AllyPanel allyPanel;
    public DetailAllyPanel detailAllyPanel { get; private set; } //call in item handle manager

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
    }
    private void OnEnable()
    {
        Initialize();
    }

    public void Initialize()
    {
        if(allyPanel == null || detailAllyPanel == null)
        {
            allyPanel = transform.Find("Ally UI Panel").GetComponent<AllyPanel>();
            detailAllyPanel = transform.Find("Detail Ally Panel").GetComponent<DetailAllyPanel>();
        }

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
