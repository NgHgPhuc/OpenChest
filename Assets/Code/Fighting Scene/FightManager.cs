using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class FightManager : MonoBehaviour
{
    public GameObject ActionPanel;
    public List<FightingUnit> PlayerTeam;
    public List<FightingUnit> EnemyTeam;

    public List<FightingUnit> All;

    Chapter CurrentChapter;

    int currentTurn;

    int CurrentEnemyTargeted; //enemy target of player
    int CurrentPlayerTargeted;//player target of enemy

    FightingUnit EnemyTargeted;
    bool IsPlayerTurn;

    string PlayerAction;

    public FloatingObject floatingObject;

    int EnemyCount;
    public GameObject VictoryPanelObject;

    int PlayerCount;
    public GameObject FailPanelObject;

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

                PlayerCount++;
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

                EnemyCount++;
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
        ActionInTurn();
    }

    public void SortSpeed()
    {
        All.Sort((FightingUnit x, FightingUnit y) => y.character.Speed.CompareTo(x.character.Speed));
        ProrityPanel.Instance.Initialize(All);
    }

    void Player_ChooseAction()
    {
        IsPlayerTurn = true;

        PlayerAction = "";
        ActionPanel.SetActive(true);
    }
    IEnumerator ChooseAction()
    {
        while (PlayerAction == "")
            yield return null;

        if(PlayerAction == "Strike")
        {
            Player_AttackAction();
        }

        if(PlayerAction == "Block")
        {
            foreach (FightingUnit unit in EnemyTeam)
                unit.UntargetUI();

            Player_BlockAction();
        }
    }

    public void ChooseAction(string action)
    {
        if (!IsPlayerTurn)
            return;

        if (action == PlayerAction)
            return;

        StopCoroutine(ChooseTarget());
        StopCoroutine(ChooseAction());
        PlayerAction = action;
        StartCoroutine(ChooseAction());
    }


    #region Attack Action
    //PLAYER CHOOSE ATTACK
    void Player_AttackAction()
    {
        EnemyTargeted = null;   //enemy target set null
        IsPlayerTurn = true;    //Set this is player turn

        //Turn on a signal that enemy can be target
        foreach (FightingUnit unit in EnemyTeam)
            unit.IsTargetUI();

        //Start coroutine to wait to gamer chosen enemy
        StartCoroutine(ChooseTarget());
    }
    //COROUTINE FUNCTION
    IEnumerator ChooseTarget()
    {
        //if haven't chosen target yet
        while (EnemyTargeted == null)
            yield return null;  //Continue wait

        //Go to this - mean have chosen enemy
        foreach (FightingUnit unit in EnemyTeam)
            unit.UntargetUI();

        //character in this turn attack enemy whom chosen
        All[currentTurn].Attack(EnemyTargeted);

        //end player turn
        IsPlayerTurn = false;

        ActionPanel.SetActive(false);

        //out of coroutine function
        yield break;

    }
    //CHOSEN ENEMY - CALL FROM ENEMY OBJECT
    public void ChooseEnemy(FightingUnit unit)
    {
        if (!IsPlayerTurn)
            return;

        EnemyTargeted = unit;
    }
    #endregion

    void Player_BlockAction()
    {
        IsPlayerTurn = false;
        ActionPanel.SetActive(false);
        All[currentTurn].Block();
    }

    //ENEMY ATTACK
    void Enemy_AttackAction()
    {
        IsPlayerTurn = false;

        PlayerAction = "";
        ActionPanel.SetActive(false);

        All[currentTurn].Attack(PlayerTeam[CurrentPlayerTargeted]);
    }


    //CHOOSE WHAT ACTION WILL DO IN TURN
    void ActionInTurn()
    {
        if (All[currentTurn].stateFighting == FightingUnit.StateFighting.Death)
        {
            Endturn();
            return;
        }

        All[currentTurn].ItsMyTurn();
        ProrityPanel.Instance.SetUnitTurn(currentTurn);

        if (All[currentTurn].team == FightingUnit.Team.Player)
            Player_ChooseAction();

        if (All[currentTurn].team == FightingUnit.Team.Enemy)
            Enemy_AttackAction();
    }

    public void Endturn()
    {
        if (EnemyCount == 0 || PlayerCount == 0)
            return;


        currentTurn++;
        if (currentTurn == All.Count)
            currentTurn = 0;
        ActionInTurn();
    }

    public void TargerDie(FightingUnit unit)
    {
        ProrityPanel.Instance.Initialize(All);

        if (unit.team == FightingUnit.Team.Enemy)
        {
            EnemyCount--;
            if (EnemyCount == 0)
                Invoke("victoryPanel", 1f);
        }
        if (unit.team == FightingUnit.Team.Player)
        {
            PlayerCount--;
            if (PlayerCount == 0)
            {
                Invoke("failPanel", 1f);
                return;
            }
            CurrentPlayerTargeted = (CurrentPlayerTargeted - 1 < 0) ? 2 : 0;
        }

    }

    void victoryPanel()
    {
        StopAllCoroutines();
        VictoryPanelObject.SetActive(true);
        VictoryPanelObject.GetComponent<VictoryPanel>().Initialize(CurrentChapter);
    }

    void failPanel()
    {
        StopAllCoroutines();
        FailPanelObject.SetActive(true);
    }
}
