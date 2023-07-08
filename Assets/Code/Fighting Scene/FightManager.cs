using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
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

    FightingUnit ChosenTarget;
    bool IsPlayerTurn;

    string PlayerAction;

    public FloatingObject floatingObject;

    int EnemyCount;
    public GameObject VictoryPanelObject;

    int PlayerCount;
    public GameObject FailPanelObject;

    public List<ButtonSkillUI> SkillButtonList;
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

        TurnManager.Instance.SetTeam(PlayerTeam, EnemyTeam);
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

        for(int i = 0; i < 3; i++)
            if(i < All[currentTurn].character.skill.Count)
                SkillButtonList[i].SetSkill(All[currentTurn].character.skill[i], All[currentTurn].CoolDown[i]);
            else
                SkillButtonList[i].SetSkill(null, 0);

    }
    void ChooseAction()
    {
        if(PlayerAction == "")
            return;

        UntargetAllEnemy();
        UntargetAllAlly();

        switch (PlayerAction)
        {
            case "Strike":
            {
                TargetAllEnemy();
                Player_AttackAction();
                break;
            }

            case "Block":
            {
                UntargetAllEnemy();
                Player_BlockAction();
                break;
            }

            case "Skill 1":
            {
                if (All[currentTurn].CoolDown[0] > 0)
                    break;

                switch (All[currentTurn].character.skill[0].range)
                {
                    case BaseSkill.Range.OnEnemy:
                        TargetAllEnemy();
                        break;

                    case BaseSkill.Range.OnEnemyTeam:
                        TargetAllEnemy();
                        break;

                    case BaseSkill.Range.OnAlly:
                        TargetAllAlly();
                        break;

                }
                Player_SkillAction();
                break;
            }
        }
    }

    public void ChooseAction(string action)
    {
        if (!IsPlayerTurn)
            return;

        if (action == PlayerAction)
            return;

        PlayerAction = action;

        StopAllCoroutines();

        ChooseAction();
    }


    #region Attack Action
    //PLAYER CHOOSE ATTACK
    void Player_AttackAction()
    {
        ChosenTarget = null;   //enemy target set null
        IsPlayerTurn = true;    //Set this is player turn

        //Start coroutine to wait to gamer chosen enemy
        StartCoroutine(ChooseTarget());
    }
    //COROUTINE FUNCTION
    IEnumerator ChooseTarget()
    {
        //if haven't chosen target yet
        while (ChosenTarget == null)
            yield return null;  //Continue wait

        //Go to this - mean have chosen enemy
        foreach (FightingUnit unit in EnemyTeam)
            unit.UntargetUI();

        //character in this turn attack enemy whom chosen
        TurnManager.Instance.AttackOneEnemy(All[currentTurn], ChosenTarget);

        //end player turn
        IsPlayerTurn = false;

        ActionPanel.SetActive(false);

        //out of coroutine function
        yield break;

    }
    //CHOSEN ENEMY - CALL FROM ENEMY OBJECT
    public void ChooseTarget(FightingUnit unit)
    {
        if (!IsPlayerTurn)
            return;

        ChosenTarget = unit;
    }
    #endregion

    void Player_BlockAction()
    {
        IsPlayerTurn = false;
        ActionPanel.SetActive(false);
        All[currentTurn].Block();
    }

    void Player_SkillAction()
    {
        ChosenTarget = null;   //enemy target set null
        IsPlayerTurn = true;    //Set this is player turn

        //Start coroutine to wait to gamer chosen enemy
        StartCoroutine(ChooseTarger_NoCoroutine());
    }
    IEnumerator ChooseTarger_NoCoroutine()
    {
        while (ChosenTarget == null)
            yield return null;

        UntargetAllEnemy();
        UntargetAllAlly();

        switch (All[currentTurn].character.skill[0].range)
        {
            case BaseSkill.Range.OnEnemy: 
                All[currentTurn].Skill(new List<FightingUnit>() { ChosenTarget },1);
                break;

            case BaseSkill.Range.OnEnemyTeam:
                All[currentTurn].Skill(EnemyTeam,1);
                break;

            case BaseSkill.Range.OnAlly:
                All[currentTurn].Skill(new List<FightingUnit>() { ChosenTarget },1);
                break;
            
        }

        IsPlayerTurn = false;

        ActionPanel.SetActive(false);

        yield break;

    }


    //ENEMY ATTACK
    void Enemy_AttackAction()
    {
        IsPlayerTurn = false;

        PlayerAction = "";
        ActionPanel.SetActive(false);

        TurnManager.Instance.AttackOneEnemy(All[currentTurn], PlayerTeam[CurrentPlayerTargeted]);
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

    void UntargetAllEnemy()
    {
        foreach (FightingUnit unit in EnemyTeam)
            unit.UntargetUI();
    }
    void TargetAllEnemy()
    {
        foreach (FightingUnit unit in EnemyTeam)
            unit.IsTargetUI();
    }
    void UntargetAllAlly()
    {
        foreach (FightingUnit unit in PlayerTeam)
            unit.UntargetUI();
    }
    void TargetAllAlly()
    {
        foreach (FightingUnit unit in PlayerTeam)
            unit.IsAllyUI();
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
