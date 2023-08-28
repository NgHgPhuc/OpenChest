using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Attack
{
    public FightingUnit Causer;
    public float DamageCause;
    public bool IsCrit = false;
    public bool IsStun = false;
    public bool IsHaveEffect = true;
}

public class Defense
{
    public float TakenDmgPercent;
    public bool IsDodge = false;
    public bool IsCounter = false;
    public bool IsHaveEffect = true;
}


public class FightingUnit : MonoBehaviour, IPointerClickHandler
{
    public Character Character;

    public Character CharacterClone;
    public Character basicStatsCharacter;

    public enum StateData
    {
        None,
        NotHaveChamp,
        HaveChamp
    }
    public StateData stateData = StateData.NotHaveChamp;

    public enum Position
    {
        TopBack,
        MiddleFront,
        DownBack
    }
    public Position position;

    public enum Team
    {
        Player,
        Enemy
    }
    public Team team;

    public enum StateFighting
    {
        Alive,
        Death,
    }
    public StateFighting stateFighting = StateFighting.Alive;

    public bool IsStuning = false;
    public bool IsTaunted = false;
    public bool IsBlock = false;

    public bool IsActioned = false;

    public int Range;

    public bool IsInTurn;

    public Animator animator;
    GameObject Shield;

    public bool IsTarget;
    public List<int> CurrentCooldown;

    //UI
    Slider healthBar;
    TextMeshProUGUI IndexHealthBar;
    float MaxHP;
    public float CurrentHP;
    Image CharacterIcon;
    Image CharacterOuterLineSignal;
    Image TargetSignal;
    Image InTurnSignal;
    Transform EffectPanel;
    Dictionary<EffectPos, Transform> EffectDict = new Dictionary<EffectPos, Transform>();
    BuffOfUnit buffOfUnit;
    EffectIcon effectIcon;

    //Event Catcher
    public delegate void ActionCatcher(FightingUnit currentUnit, FightingUnit targetUnit, Attack currentAttack, Defense targetDefense);
    public ActionCatcher Unit_Attack;
    public ActionCatcher Unit_BeAttacked;
    //public UnityEvent Unit_Block;

    int speedAction;
    private void Awake()
    {
        if (healthBar == null)
        {
            healthBar = transform.Find("Health Bar").GetComponent<Slider>();
            IndexHealthBar = healthBar.transform.Find("Index").GetComponent<TextMeshProUGUI>();
        }

        if (TargetSignal == null)
            TargetSignal = transform.Find("Target Signal").GetComponent<Image>();
        TargetSignal.gameObject.SetActive(false);

        if (Shield == null)
            Shield = transform.Find("Shield Effect").gameObject;

        if (EffectPanel == null)
            EffectPanel = transform.Find("Effect Panel");

        if (EffectDict.Count == 0)
            for (int i = 0; i < EffectPanel.childCount; i++)
                EffectDict.Add((EffectPos)i, EffectPanel.GetChild(i));

        if (buffOfUnit == null)
            buffOfUnit = transform.Find("Buff Of Unit").GetComponent<BuffOfUnit>();

        if (effectIcon == null)
            effectIcon = transform.Find("Effect Icon").GetComponent<EffectIcon>();

        ShowBuff();

        if (InTurnSignal == null)
            InTurnSignal = transform.Find("In Turn Signal").GetComponent<Image>();
        InTurnSignal.gameObject.SetActive(false);

        if (CharacterOuterLineSignal == null)
            CharacterOuterLineSignal = transform.Find("Character Outliner").GetComponent<Image>();
        CharacterOuterLineSignal.gameObject.SetActive(false);
    }

    private void Update()
    {
        //if (!CharacterOuterLineSignal.IsActive())
        //    return;

        //CharacterOuterLineSignal.material.SetTexture("_MainTex", ConvertSpriteToTexture(CharacterIcon.sprite));

        //Material newMat = new Material(Shader.Find("Shader Graphs/Out"));
        //newMat.CopyPropertiesFromMaterial(CharacterOuterLineSignal.material);
        //CharacterOuterLineSignal.material = newMat;
    }

    //public Texture2D ConvertSpriteToTexture(Sprite sprite)
    //{
    //    try
    //    {
    //        if (sprite.rect.width != sprite.texture.width)
    //        {
    //            Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
    //            Color[] colors = newText.GetPixels();
    //            Color[] newColors = sprite.texture.GetPixels((int)System.Math.Ceiling(sprite.textureRect.x),
    //                                                         (int)System.Math.Ceiling(sprite.textureRect.y),
    //                                                         (int)System.Math.Ceiling(sprite.textureRect.width),
    //                                                         (int)System.Math.Ceiling(sprite.textureRect.height));
    //            newText.SetPixels(newColors);
    //            newText.Apply();
    //            return newText;
    //        }
    //        else
    //            return sprite.texture;
    //    }
    //    catch
    //    {
    //        return sprite.texture;
    //    }
    //}

    //INSTANTIATE VALUE OF THIS
    public void Instantiate()
    {
        if(Character != null)
        {
            CharacterClone = Character.Clone();
            basicStatsCharacter = CharacterClone.Clone();
        }

        if (healthBar == null)
        {
            healthBar = transform.Find("Health Bar").GetComponent<Slider>();
            IndexHealthBar = healthBar.transform.Find("Index").GetComponent<TextMeshProUGUI>();
        }

        MaxHP = CharacterClone.HealthPoint;
        CurrentHP = MaxHP;
        UpdateHealthBar();

        if (CharacterIcon == null)
            CharacterIcon = transform.Find("Character Icon").GetComponent<Image>();

        CharacterIcon.sprite = CharacterClone.Icon;

        try
        {
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animator/" + CharacterClone.Name + "/" + CharacterClone.Name);
        }
        catch (Exception)
        {

        }

        CurrentCooldown = new List<int>(new int[this.CharacterClone.skills.Count]);

    }
    public void SetAnimationSpeed(int speed)
    {
        animator.speed = speed;
        speedAction = speed;
        InTurnSignal.GetComponent<Animator>().speed = speed;
    }

    void UpdateHealthBar()
    {
        if (CurrentHP > MaxHP)
            CurrentHP = MaxHP;

        IndexHealthBar.SetText(Math.Ceiling(CurrentHP) + "/" + MaxHP);
        healthBar.maxValue = MaxHP;
        healthBar.value = CurrentHP;
    }

    public void Floating(string msg, Color color)
    {
        int Times = msg.Split("\n").Length;
        EffectManager.Instance.FloatingText(msg, color, this.transform,Times);
    }

    public Image GetIconImage()
    {
        return CharacterIcon;
    }

    //SET UI OF TARGET BY fightManager.Instance
    void SetTargetSignal()
    {
        if (TargetSignal == null)
            TargetSignal = transform.Find("Target Signal").GetComponent<Image>();

        if (InTurnSignal == null)
            InTurnSignal = transform.Find("In Turn Signal").GetComponent<Image>();

        if (CharacterOuterLineSignal == null)
            CharacterOuterLineSignal = transform.Find("Character Outliner").GetComponent<Image>();
    }
    public void IsTargetUI()
    {
        SetTargetSignal();

        TargetSignal.gameObject.SetActive(true);
        TargetSignal.color = Color.red;

        CharacterOuterLineSignal.gameObject.SetActive(true);
        CharacterOuterLineSignal.material.SetColor("_BaseColor", Color.red);

        IsTarget = true;
    }
    public void UntargetUI()
    {
        SetTargetSignal();

        TargetSignal.gameObject.SetActive(false);
        TargetSignal.color = Color.white;

        CharacterOuterLineSignal.gameObject.SetActive(false);
        CharacterOuterLineSignal.material.SetColor("_BaseColor", Color.white);

        IsTarget = false;
    }
    public void IsAllyUI()
    {
        SetTargetSignal();

        TargetSignal.gameObject.SetActive(true);
        TargetSignal.color = Color.green;

        CharacterOuterLineSignal.gameObject.SetActive(true);
        CharacterOuterLineSignal.material.SetColor("_BaseColor", Color.green);

        IsTarget = true;
    }
    public void EndTurnUI()
    {
        SetTargetSignal();

        InTurnSignal.gameObject.SetActive(false);
    }
    public void InTurnUI()
    {
        SetTargetSignal();

        InTurnSignal.gameObject.SetActive(true);
    }

    public void AttackUI()
    {

    }

    public void BeingAttackUI()
    {
        this.CharacterIcon.color = Color.red;
        InitializeEffect(EffectPos.InMiddle, EffectManager.EffectName.BeingAttack, 0.5f);

        animator.Play("Hit " + CharacterClone.Name);
    }
    public void CritUI()
    {
        Floating("Critical", Color.red);
    }

    public void CounterUI()
    {
        Floating("Counter", Color.white);
    }

    public void DodgeUI()
    {
        Floating("Miss!", Color.white);
    }

    public void StunUI()
    {
        Floating("Stuning", Color.white);
    }
    public void HealUI()
    {
        InitializeEffect(EffectPos.InMiddle, EffectManager.EffectName.Healing, 0.5f);
    }
    //Effect UI
    public void InitializeEffect(EffectPos effectPos,EffectManager.EffectName effectName, float LivingTime)
    {
        float width = CharacterIcon.rectTransform.rect.width;
        float height = CharacterIcon.rectTransform.rect.height;
        EffectManager.Instance.InitializeEffect(EffectDict[effectPos], effectName, LivingTime,width,height);
    }



    //ACTIVITY FLOW ===== ItsMyTurn -> Action -> EndMyTurn
    public void ItsMyTurn(bool isAnotherTurn = false)
    {
        IsInTurn = true;
        InTurnUI();

        if (isAnotherTurn)
            return;

        for (int i = 0; i < CurrentCooldown.Count && CurrentCooldown[i] != 0; i++)
            CurrentCooldown[i] -= 1;

        if (IsBlock == true)
            EndShield();

        if (this.IsStuning)
        {
            StunUI();
            EndMyTurn();
            Invoke("EndTurn", 1f/speedAction);
        }

        OnactiveBuff();

        IsActioned = true;
    }

    public void EndMyTurn()
    {
        CheckBuff();
        ShowBuff();

        IsInTurn = false;
        EndTurnUI();

        animator.Play("Idle " + CharacterClone.Name);
    }

    void EndTurn()
    {
        FightManager.Instance.Endturn();
    }


    //SKILL
    public void Skill(List<FightingUnit> ChosenUnit,int skillCount)
    {
        Floating(this.CharacterClone.skills[skillCount].Name, Color.white);
        this.CurrentCooldown[skillCount] = this.CharacterClone.skills[skillCount].Cooldown +1;
        TurnManager.Instance.UsingSkill(this, ChosenUnit, skillCount);
    }


    //BLOCK - Decrease 50% taken damage
    public void Block()
    {
        IsBlock = true;
        Shield.SetActive(true);
        Shield.GetComponent<Animator>().speed = speedAction;

        //Unit_Block?.Invoke();

        EndMyTurn();

        Invoke("EndTurn", 1f/ speedAction);
    }

    public void EndShield()
    {
        IsBlock = false;
        Shield.SetActive(false);
        CharacterIcon.color = Color.white;
    }

    //ATTACK
    public Attack attack()
    {
        Attack attack = new Attack();

        attack.Causer = this;

        attack.DamageCause = this.CharacterClone.AttackDamage;

        float CriticalRate = UnityEngine.Random.Range(0f, 100f);
        if (CriticalRate < CharacterClone.PassiveList[BaseStats.Passive.CriticalChance])
        {
            attack.DamageCause *= (100 + CharacterClone.PassiveList[BaseStats.Passive.CriticalDamage]) * 2 / 100;
            attack.IsCrit = true;
        }

        float StunRate = UnityEngine.Random.Range(0f, 100f);
        if (StunRate < this.CharacterClone.PassiveList[BaseStats.Passive.Stun])
            attack.IsStun = true;

        //animator.Play("Attack " + CharacterClone.Name);

        return attack;
    }

    //LIFESTEAL - recover % damage cause
    public void LifeSteal(float DamageCause)
    {
        float mount = DamageCause * this.CharacterClone.PassiveList[BaseStats.Passive.LifeSteal] / 100;
        if (mount == 0)
            return;

        Heal(mount);
    }

    public void Heal(float mount)
    {
        CurrentHP += mount;

        string msg = "Healing" + "\n" + Math.Ceiling(mount).ToString();
        Floating(msg, Color.green);
        UpdateHealthBar();
        HealUI();
    }


    public void HealingPercentMaxHp(float percent)
    {
        Heal(GetPercentMaxHP(percent));
    }

    public void UsingPercentMaxHP(float percent)
    {
        float usingMount = GetPercentMaxHP(percent);
        CurrentHP -= usingMount;
        if (CurrentHP <= 0)
            Death();

        UpdateHealthBar();
        HealUI();
        Floating(Math.Ceiling(usingMount).ToString(), Color.red);
    }
    public float GetPercentMaxHP(float percent)
    {
        return MaxHP * percent / 100f;
    }
    public void IncreaseMaxHP(float IncreaseMaxHP)
    {
        this.CharacterClone.HealthPoint += IncreaseMaxHP;
        this.MaxHP = this.CharacterClone.HealthPoint;
        this.Heal(IncreaseMaxHP);
        UpdateHealthBar();
    }
    public void DecreaseMaxHP(float DecreaseMaxHP)
    {
        this.CharacterClone.HealthPoint -= DecreaseMaxHP;
        this.MaxHP = this.CharacterClone.HealthPoint;
        
        UpdateHealthBar();
    }

    public void UsingPercentCurrentHP(float percent)
    {
        float usingMount = GetPercentCurrentHP(percent);
        CurrentHP -= usingMount;
        if (CurrentHP <= 0)
            Death();

        UpdateHealthBar();
        HealUI();
        Floating(Math.Ceiling(usingMount).ToString(), Color.red);
    }
    public float GetPercentCurrentHP(float percent)
    {
        return CurrentHP * percent / 100f;
    }
    public int GetPercentHP()
    {
        float percent = CurrentHP / MaxHP;
        return Mathf.RoundToInt(percent*100);
    }

    //BEING ATTACKED - END BEING ATTACKED
    public Defense defense(float ArmorPenetration = 0)
    {
        Defense defense = new Defense();
        defense.TakenDmgPercent = this.CalculateDamage(ArmorPenetration);

        float CounterRate = UnityEngine.Random.Range(0f, 100f);
        if (CounterRate < CharacterClone.PassiveList[BaseStats.Passive.CounterAttack])
            defense.IsCounter = true;

        float DogdeRate = UnityEngine.Random.Range(0f, 100f);
        if (DogdeRate < this.CharacterClone.PassiveList[BaseStats.Passive.Dodge])
            defense.IsDodge = true;

        return defense;
    }

    //CALCULATE RECEIVE DAMAGE
    public float CalculateDamage(float ArmorPenetration)
    {
        float TakenDamagePercent = 100 / (100 + this.CharacterClone.DefensePoint*(1-ArmorPenetration/100f));

        if (IsBlock == true && ArmorPenetration < 100)
            TakenDamagePercent /= 2;

        return TakenDamagePercent;
    }


    public void OnlyTakenDamage(float DamageTaken,float Penetration)
    {
        float CurrentUsingGet = DamageTaken * this.CalculateDamage(Penetration);
        BeingAttacked(CurrentUsingGet);
    }

    public void BeingAttacked(float DamageTaken)
    {
        CurrentHP -= DamageTaken;
        FightManager.Instance.SetTotalDamage(team, DamageTaken);
        Floating(Math.Ceiling(DamageTaken).ToString(), Color.red);

        if (CurrentHP <= 0 && this.stateFighting == StateFighting.Alive)
        {
            Death();
            return;
        }
        
        BeingAttackUI();

        UpdateHealthBar();
    }


    //BUFF
    public void AddBuff(Buff buff)
    {
        buffOfUnit.AddBuff(buff);
        Floating(buff.type.ToString(), Color.white);
        ShowBuff();
    }
    void CheckBuff()//call endbuff in here and remove buff - before end turn
    {
        buffOfUnit.CheckBuff();
    }
    void ShowBuff()
    {
        effectIcon.SetBuffIcon(buffOfUnit.buffs);
    }
    void OnactiveBuff()//on active buff call in here - after in turn
    {
        buffOfUnit.OnactivationBuff();
    }

    void Death()
    {
        this.stateFighting = StateFighting.Death;

        float width = CharacterIcon.rectTransform.rect.width * (1+1/3f);
        float height = CharacterIcon.rectTransform.rect.height * (1 + 1 / 3f);
        EffectManager.Instance.InitializeEffect(EffectDict[EffectPos.InMiddle], EffectManager.EffectName.DeathEffect, 0.6f, width, height);
        FightManager.Instance.TargerDie(this);
        gameObject.SetActive(false);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if(this.IsTarget == true)
            FightManager.Instance.ChooseTarget(this);
    }

    public void SetCdSkill(int newCd,string skillName)
    {
        int i = 0;
        foreach(BaseSkill bS in CharacterClone.skills)
        {
            if (bS.Name == skillName)
            {
                CurrentCooldown[i] = newCd;
                return;
            }
            i++;
        }
    }    
}
