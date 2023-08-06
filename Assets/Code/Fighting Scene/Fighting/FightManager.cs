using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;

public class FightManager : MonoBehaviour
{
    public GameObject ActionPanel;
    public List<FightingUnit> PlayerTeam;
    public List<FightingUnit> EnemyTeam;

    public List<FightingUnit> All;

    Chapter CurrentChapter;

    int currentTurn;

    FightingUnit CurrentEnemyTargeted; //enemy target of player when being taunted
    FightingUnit CurrentPlayerTargeted;//player target of enemy when being taunted

    FightingUnit ChosenTarget;
    bool IsPlayerTurn;

    string PlayerAction;

    int EnemyCount;
    public GameObject VictoryPanelObject;

    int PlayerCount;
    public GameObject FailPanelObject;

    public List<ButtonSkillUI> SkillButtonList;

    public TextMeshProUGUI RoundText;
    int RoundIndex = 1;

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
        CurrentChapter = Resources.Load<Chapter>("Chapter/" + currentChapterName);

        //CurrentChapter = Resources.Load<Chapter>("Chapter/" + "Chapter 1");
        PlayerPrefs.DeleteKey("Current Chapter Name");

        for (int i = 0; i < 3; i++)
        {
            if (i < CurrentChapter.MyTeam.Count)
            {
                PlayerTeam[i].gameObject.SetActive(true);
                PlayerTeam[i].CharacterClone = CurrentChapter.MyTeam[i].Clone();
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
                EnemyTeam[i].CharacterClone = CurrentChapter.EnemyTeam[i].Clone();
                EnemyTeam[i].stateData = FightingUnit.StateData.HaveChamp;
                EnemyTeam[i].team = FightingUnit.Team.Enemy;
                EnemyTeam[i].position = (FightingUnit.Position)i;
                EnemyTeam[i].Instantiate();

                EnemyCount++;
            }
            else EnemyTeam[i].gameObject.SetActive(false);
        }

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

        RoundText.SetText("Round " + RoundIndex.ToString());
    }

    public void SortSpeed()
    {
        All.Sort((FightingUnit x, FightingUnit y) => y.CharacterClone.Speed.CompareTo(x.CharacterClone.Speed));
        ProrityPanel.Instance.Initialize(All);
    }

    void Player_ChooseAction()
    {
        if (All[currentTurn].IsStuning)
            return;

        IsPlayerTurn = true;

        PlayerAction = "";
        ActionPanel.SetActive(true);

        for(int i = 0; i < 3; i++)
            if(i < All[currentTurn].CharacterClone.skill.Count)
                SkillButtonList[i].SetSkill(All[currentTurn].CharacterClone.skill[i], All[currentTurn].CoolDown[i]);
            else
                SkillButtonList[i].SetSkill(null, 0);

    }

    private void OnUsingSkillUI(int skillIndex)
    {
        if (All[currentTurn].CoolDown[skillIndex] > 0)
            return;

        switch (All[currentTurn].CharacterClone.skill[skillIndex].range)
        {
            case BaseSkill.Range.OnEnemy:
                TargetEnemy(EnemyTeam);
                break;

            case BaseSkill.Range.OnEnemyTeam:
                TargetEnemy(EnemyTeam);
                break;

            case BaseSkill.Range.OnAlly:
                TargetAlly(PlayerTeam);
                break;

            case BaseSkill.Range.OnMySelf:
                TargetAlly(new List<FightingUnit>() { All[currentTurn] });
                break;

            case BaseSkill.Range.OnAllyTeam:
                TargetAlly(PlayerTeam);
                break;
        }

        Player_SkillAction(skillIndex);
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
                TargetEnemy(EnemyTeam);
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
                int skillIndex = 0;
                OnUsingSkillUI(skillIndex);
                break;
            }
            case "Skill 2":
            {
                int skillIndex = 1;
                OnUsingSkillUI(skillIndex);
                break;
            }
            case "Skill 3":
            {
                int skillIndex = 2;
                OnUsingSkillUI(skillIndex);
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
        TurnManager.Instance.Attack(All[currentTurn], ChosenTarget);

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

    void Player_SkillAction(int skillIndex)
    {
        ChosenTarget = null;   //enemy target set null
        IsPlayerTurn = true;    //Set this is player turn

        //Start coroutine to wait to gamer chosen enemy
        IEnumerator coroutine = ChooseTarger_NoCoroutine(skillIndex);
        StartCoroutine(coroutine);
    }
    IEnumerator ChooseTarger_NoCoroutine(int skillIndex)
    {
        while (ChosenTarget == null)
            yield return null;

        UntargetAllEnemy();
        UntargetAllAlly();

        switch (All[currentTurn].CharacterClone.skill[skillIndex].range)
        {
            case BaseSkill.Range.OnEnemy: 
                All[currentTurn].Skill(new List<FightingUnit>() { ChosenTarget }, skillIndex);
                break;

            case BaseSkill.Range.OnEnemyTeam:
                All[currentTurn].Skill(EnemyTeam, skillIndex);
                break;

            case BaseSkill.Range.OnAlly:
                All[currentTurn].Skill(new List<FightingUnit>() { ChosenTarget }, skillIndex);
                break;

            case BaseSkill.Range.OnMySelf:
                All[currentTurn].Skill(new List<FightingUnit>() { All[currentTurn] }, skillIndex);
                break;

            case BaseSkill.Range.OnAllyTeam:
                All[currentTurn].Skill(PlayerTeam, skillIndex);
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

        if (All[currentTurn].IsTaunted == false)
            CurrentPlayerTargeted = EnemyRandomTarget();

        if (All[currentTurn].IsStuning == false)
            TurnManager.Instance.Attack(All[currentTurn], CurrentPlayerTargeted);
    }

    FightingUnit EnemyRandomTarget()
    {
        int c = PlayerTeam.Count;
        int TargetIndex = UnityEngine.Random.Range(0, c);
        return PlayerTeam[TargetIndex];
    }
    public void SetPlayerTargeted(FightingUnit f)
    {
        this.CurrentPlayerTargeted = f;
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
        {
            currentTurn = 0;
            RoundIndex += 1;
            RoundText.SetText("Round "+RoundIndex.ToString());
        }
        ActionInTurn();
    }

    public void TargerDie(FightingUnit unit)
    {
        ProrityPanel.Instance.Initialize(All);

        if (unit.team == FightingUnit.Team.Enemy)
        {
            EnemyCount--;
            print(EnemyCount);
            EnemyTeam = EnemyTeam.Where(x => x.stateFighting == FightingUnit.StateFighting.Alive).ToList();
            if (EnemyCount == 0)
            {
                Invoke("victoryPanel", 1f);
                return;
            }
            return;
        }
        if (unit.team == FightingUnit.Team.Player)
        {
            PlayerCount--;
            print(PlayerCount);
            PlayerTeam = PlayerTeam.Where(x => x.stateFighting == FightingUnit.StateFighting.Alive).ToList();
            if (PlayerCount == 0)
            {
                Invoke("failPanel", 1f);
                return;
            }
            return;
        }

    }

    void UntargetAllEnemy()
    {
        foreach (FightingUnit unit in EnemyTeam)
            unit.UntargetUI();
    }
    void TargetEnemy(List<FightingUnit> EnemyTeam)
    {
        foreach (FightingUnit unit in EnemyTeam)
            unit.IsTargetUI();
    }
    void UntargetAllAlly()
    {
        foreach (FightingUnit unit in PlayerTeam)
            unit.UntargetUI();
    }
    void TargetAlly(List<FightingUnit> PlayerTeam)
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

    public void SaveData(Dictionary<string, string> dict)
    {
        var request = new UpdateUserDataRequest
        {
            Data = dict
        };

        PlayFabClientAPI.UpdateUserData(request, OnSaveSuccess, OnSaveFail);
    }

    private void OnSaveFail(PlayFabError obj)
    {
    }

    private void OnSaveSuccess(UpdateUserDataResult obj)
    {
    }
}
