using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FightManager : MonoBehaviour
{
    public Transform Canvas;
    public List<FightingUnit> PlayerTeam;
    public List<FightingUnit> EnemyTeam;

    public List<FightingUnit> All;

    Chapter CurrentChapter;

    int currentTurn;

    int CurrentEnemyTargeted; //enemy target of player
    int CurrentPlayerTargeted;//player target of enemy

    public static FightManager Instance { get; private set; }
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

        string currentChapterName = PlayerPrefs.GetString("Current Chapter Name");
        CurrentChapter = Resources.Load<Chapter>("Chapter/"+ "Chapter 1");
        PlayerPrefs.DeleteKey("Current Chapter Name");

        for (int i = 0; i < 3; i++)
        {
            if (i < CurrentChapter.MyTeam.Count)
            {
                PlayerTeam[i].gameObject.SetActive(true);
                PlayerTeam[i].character = CurrentChapter.MyTeam[i];
                PlayerTeam[i].stateData = FightingUnit.StateData.HaveChamp;
                PlayerTeam[i].team = FightingUnit.Team.Player;
                PlayerTeam[i].position = (FightingUnit.Position)i;
                PlayerTeam[i].Instantiate();
            }
            else PlayerTeam[i].gameObject.SetActive(false);

            if (i < CurrentChapter.EnemyTeam.Count)
            {
                EnemyTeam[i].gameObject.SetActive(true);
                EnemyTeam[i].character = CurrentChapter.EnemyTeam[i];
                EnemyTeam[i].stateData = FightingUnit.StateData.HaveChamp;
                EnemyTeam[i].team = FightingUnit.Team.Enemy;
                EnemyTeam[i].position = (FightingUnit.Position)i;
                EnemyTeam[i].Instantiate();
            }
            else EnemyTeam[i].gameObject.SetActive(false);
        }


        CurrentEnemyTargeted = (EnemyTeam[1].stateData == FightingUnit.StateData.NotHaveChamp) ? 0 : 1;
        CurrentPlayerTargeted = (PlayerTeam[1].stateData == FightingUnit.StateData.NotHaveChamp) ? 0 : 1;

        PlayerTeam = PlayerTeam.Where(x => x.stateData == FightingUnit.StateData.HaveChamp).ToList();
        EnemyTeam = EnemyTeam.Where(x => x.stateData == FightingUnit.StateData.HaveChamp).ToList();

        All.AddRange(PlayerTeam);
        All.AddRange(EnemyTeam);

    }
    void Start()
    {
        SortSpeed();
        Fight();
    }

    public void SortSpeed()
    {
        All.Sort((FightingUnit x, FightingUnit y) => y.character.Speed.CompareTo(x.character.Speed));
    }


    void Fight()
    {
        if (All[currentTurn].stateFighting == FightingUnit.StateFighting.Death)
        {
            Endturn();
            return;
        }
        if(All[currentTurn].team == FightingUnit.Team.Player)
            All[currentTurn].Attack(EnemyTeam[CurrentEnemyTargeted]);
        if (All[currentTurn].team == FightingUnit.Team.Enemy)
            All[currentTurn].Attack(PlayerTeam[CurrentPlayerTargeted]);
    }

    public void Endturn()
    {
        currentTurn++;
        if (currentTurn == All.Count)
            currentTurn = 0;
        Fight();
    }

    public void TargerDie(FightingUnit.Team team)
    {
        if (team == FightingUnit.Team.Enemy)
            CurrentEnemyTargeted = (CurrentEnemyTargeted - 1 < 0) ? 2 : 0;
        if (team == FightingUnit.Team.Player)
            CurrentPlayerTargeted = (CurrentPlayerTargeted - 1 < 0) ? 2 : 0;

    }
}
