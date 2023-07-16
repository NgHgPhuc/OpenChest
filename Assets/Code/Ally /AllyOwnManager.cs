using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyOwnManager : MonoBehaviour
{
    Dictionary<string, Character> OwnAlly = new Dictionary<string, Character>();
    public List<AllySO> ListAllySO = new List<AllySO>();
    public static AllyOwnManager Instance { get; private set; }
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

    public void SetAllAlly(List<AllySO> ListAllySO)
    {
        //ListAllySO = new List<AllySO>(Resources.LoadAll<AllySO>("Character"));
        this.ListAllySO = ListAllySO;
        foreach (AllySO a in ListAllySO)
            OwnAlly[a.character.Name] = a.character;
    }

    public Dictionary<string,Character> GetAllAlly()
    {
        return OwnAlly;
    }

    public void SetAllyInTeam()
    {
        foreach (AllySO a in ListAllySO)
            if (a.character.IsInTeam)
                TeamManager.Instance.SetCompainAlly(a.character);
    }


}
