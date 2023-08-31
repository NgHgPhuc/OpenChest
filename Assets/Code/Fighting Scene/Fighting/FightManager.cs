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

    public int GameSpeed;
    public bool IsAuto;

    public TotalDamageObject playerTotalDamage;
    public TotalDamageObject enemyTotalDamage;

    public bool IsTest;

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


        if(IsTest)
        {
            CurrentChapter = Resources.Load<Chapter>("Chapter/" + "Chapter Test");
        }
        else if(PlayerPrefs.HasKey("Current Chapter Name"))
        {
            string currentChapterName = PlayerPrefs.GetString("Current Chapter Name");
            CurrentChapter = Resources.Load<Chapter>("Chapter/" + currentChapterName);
            PlayerPrefs.DeleteKey("Current Chapter Name");
        }
        else
            CurrentChapter = Resources.Load<Chapter>("Chapter/" + "Chapter Test");

        for (int i = 0; i < 4; i++)
        {
            if (i < CurrentChapter.MyTeam.Count && CurrentChapter.MyTeam[i] != null)
            {
                PlayerTeam[i].gameObject.SetActive(true);
                PlayerTeam[i].Character = CurrentChapter.MyTeam[i];
                PlayerTeam[i].stateData = FightingUnit.StateData.HaveChamp;
                PlayerTeam[i].team = FightingUnit.Team.Player;
                PlayerTeam[i].position = (FightingUnit.Position)i;
                PlayerTeam[i].Instantiate();

                PlayerCount++;
            }
            else
            {
                PlayerTeam[i].gameObject.SetActive(false);
                PlayerTeam[i].stateData = FightingUnit.StateData.None;
            }

            if (i < CurrentChapter.EnemyTeam.Count && CurrentChapter.EnemyTeam[i] != null)
            {
                EnemyTeam[i].gameObject.SetActive(true);
                EnemyTeam[i].Character = CurrentChapter.EnemyTeam[i];
                EnemyTeam[i].stateData = FightingUnit.StateData.HaveChamp;
                EnemyTeam[i].team = FightingUnit.Team.Enemy;
                EnemyTeam[i].position = (FightingUnit.Position)i;
                EnemyTeam[i].Instantiate();

                EnemyCount++;
            }
            else
            {
                EnemyTeam[i].gameObject.SetActive(false);
                EnemyTeam[i].stateData = FightingUnit.StateData.None;
            }
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
        IsPlayerTurn = true;

        PlayerAction = "";
        ActionPanel.SetActive(!IsAuto);

        for(int i = 0; i < 3; i++)
            if(i < All[currentTurn].CharacterClone.skills.Count)
                SkillButtonList[i].SetSkill(All[currentTurn].CharacterClone.skills[i], All[currentTurn].CurrentCooldown[i]);
            else
                SkillButtonList[i].SetSkill(null, 0);

        //continue in chooseAction(string action) by click skill or action slot

        if(IsAuto)
        {
            for(int i = 0; i < All[currentTurn].CurrentCooldown.Count; i++)
            {
                if (All[currentTurn].CharacterClone.skills[i].skillType == BaseSkill.SkillType.Passive)
                    continue;

                if (All[currentTurn].CurrentCooldown[i] > 0)
                    continue;

                ChooseAction("Skill " + (i + 1).ToString());
                return;
            }

            int currentHP = All[currentTurn].GetPercentHP();
            float r = UnityEngine.Random.Range(0, 100);
            if (r > currentHP)
                ChooseAction("Block");
            else ChooseAction("Strike");
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

    void ChooseAction()
    {
        if (PlayerAction == "")
            return;

        UntargetAllEnemyUI();
        UntargetAllAllyUI();

        switch (PlayerAction)
        {
            case "Strike":
                {
                    TargetEnemyUI(EnemyTeam);
                    Player_AttackAction();
                    break;
                }

            case "Block":
                {
                    UntargetAllEnemyUI();
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

    private void OnUsingSkillUI(int skillIndex)
    {
        if (All[currentTurn].CurrentCooldown[skillIndex] > 0)
            return;

        if (All[currentTurn].CharacterClone.skills[skillIndex].skillType == BaseSkill.SkillType.Passive)
            return;

        switch (All[currentTurn].CharacterClone.skills[skillIndex].range)
        {
            case BaseSkill.Range.OnEnemy:
                TargetEnemyUI(EnemyTeam);
                break;

            case BaseSkill.Range.OnEnemyTeam:
                TargetEnemyUI(EnemyTeam);
                break;

            case BaseSkill.Range.OnAlly:
                TargetAllyUI(PlayerTeam);
                break;

            case BaseSkill.Range.OnMySelf:
                TargetAllyUI(new List<FightingUnit>() { All[currentTurn] });
                break;

            case BaseSkill.Range.OnAllyTeam:
                TargetAllyUI(PlayerTeam);
                break;
        }

        Player_SkillAction(skillIndex);
    }


    #region Attack Action
    //PLAYER CHOOSE ATTACK
    void Player_AttackAction()
    {
        IsPlayerTurn = true;    //Set this is player turn

        if (!IsAuto)//if not auto - must choose target
            ChosenTarget = null;   //enemy target set null
        else
        {
            int currentEnemyCount = EnemyTeam.Count;
            int r = UnityEngine.Random.Range(0, currentEnemyCount);
            ChosenTarget = EnemyTeam[r];
        }

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
        IsPlayerTurn = true;    //Set this is player turn

        ChosenTarget = null;   //enemy target set null
        if(IsAuto)
        {
            var targetTeam = EnemyTeam;
            if (All[currentTurn].CharacterClone.skills[skillIndex].range == BaseSkill.Range.OnAlly)
                targetTeam = PlayerTeam;

            int currentTargetCount = targetTeam.Count;
            if (currentTargetCount == 0)
                return;
            int r = UnityEngine.Random.Range(0, currentTargetCount);
            ChosenTarget = targetTeam[r];
        }

        //Start coroutine to wait to gamer chosen enemy
        IEnumerator coroutine = ChooseTarger_NoCoroutine(skillIndex);
        StartCoroutine(coroutine);
    }
    IEnumerator ChooseTarger_NoCoroutine(int skillIndex)
    {
        while (ChosenTarget == null)
            yield return null;

        UntargetAllEnemyUI();
        UntargetAllAllyUI();

        IsPlayerTurn = false;
        ActionPanel.SetActive(false);

        switch (All[currentTurn].CharacterClone.skills[skillIndex].range)
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

        yield break;

    }


    //ENEMY ATTACK
    void Enemy_AttackAction()
    {
        if (!All[currentTurn].IsTaunted)//not taunted - attack random target
            CurrentPlayerTargeted = EnemyRandomTarget();

        //TurnManager.Instance.Attack(All[currentTurn], CurrentPlayerTargeted);

        int currentHP = All[currentTurn].GetPercentHP();
        float r = UnityEngine.Random.Range(0, 100);
        if (r > currentHP)
            All[currentTurn].Block();
        else TurnManager.Instance.Attack(All[currentTurn], CurrentPlayerTargeted);
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


    public void AnotherTurn()
    {
        playerTotalDamage.End();
        enemyTotalDamage.End();

        All[currentTurn].IsActioned = true;

        if (All[currentTurn].stateFighting == FightingUnit.StateFighting.Death)
        {
            Endturn();
            return;
        }

        if (All[currentTurn].team == FightingUnit.Team.Player)
            Player_ChooseAction();

        if (All[currentTurn].team == FightingUnit.Team.Enemy)
            Enemy_AttackAction();
    }

    void ActionInTurn()
    {
        if (All[currentTurn].stateFighting == FightingUnit.StateFighting.Death)
        {
            Endturn();
            return;
        }

        bool isStuning = All[currentTurn].IsStuning;
        All[currentTurn].ItsMyTurn();

        ProrityPanel.Instance.SetUnitTurn(currentTurn);

        if (isStuning)
            return;

        if (All[currentTurn].team == FightingUnit.Team.Player)
            Player_ChooseAction();

        if (All[currentTurn].team == FightingUnit.Team.Enemy)
            Enemy_AttackAction();
    }

    public void Endturn()
    {
        if (EnemyCount == 0 || PlayerCount == 0)
            return;

        playerTotalDamage.End();
        enemyTotalDamage.End();

        for (int i = 0; i < All.Count; i++)
        {
            if (All[i].stateFighting == FightingUnit.StateFighting.Death)
                continue;

            if (All[i].IsActioned)
                continue;

            currentTurn = i;
            //All[currentTurn].IsActioned = true;
            ActionInTurn();
            return;
        }

        ResetActionOfAll();
        IncreaseRound();
    }
    void ResetActionOfAll()
    {
        foreach (FightingUnit fU in All)
            fU.IsActioned = false;
    }
    void IncreaseRound()
    {
        RoundIndex += 1;
        RoundText.SetText("Round " + RoundIndex.ToString());
        Endturn(); //run round again
    }


    public void TargerDie(FightingUnit unit)
    {
        ProrityPanel.Instance.Initialize(All);

        if (unit.team == FightingUnit.Team.Enemy)
        {
            EnemyCount--;
            EnemyTeam = EnemyTeam.Where(x => x.stateFighting == FightingUnit.StateFighting.Alive).ToList();
            if (EnemyCount == 0)
            {
                Invoke("victoryPanel", 1f/GameSpeed);
                return;
            }
            return;
        }
        if (unit.team == FightingUnit.Team.Player)
        {
            PlayerCount--;
            PlayerTeam = PlayerTeam.Where(x => x.stateFighting == FightingUnit.StateFighting.Alive).ToList();
            if (PlayerCount == 0)
            {
                Invoke("failPanel", 1f/ GameSpeed);
                return;
            }
            return;
        }

    }

    void UntargetAllEnemyUI()
    {
        foreach (FightingUnit unit in EnemyTeam)
            unit.UntargetUI();
    }
    void TargetEnemyUI(List<FightingUnit> EnemyTeam)
    {
        foreach (FightingUnit unit in EnemyTeam)
            unit.IsTargetUI();
    }
    void UntargetAllAllyUI()
    {
        foreach (FightingUnit unit in PlayerTeam)
            unit.UntargetUI();
    }
    void TargetAllyUI(List<FightingUnit> PlayerTeam)
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

    public void SetGameSpeed(int speed)
    {
        GameSpeed = speed;
        foreach(FightingUnit fU in All)
        {
            fU.SetAnimationSpeed(speed);
        }
    }
    public void SetAuto(bool isAuto)
    {
        this.IsAuto = isAuto;
        if(IsPlayerTurn)
            Player_ChooseAction();
    }

    public void SetTotalDamage(FightingUnit.Team team,float value)
    {
        if(team == FightingUnit.Team.Player)
            playerTotalDamage.AddValue(value);

        if (team == FightingUnit.Team.Enemy)
            enemyTotalDamage.AddValue(value);
    }
}
