using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class OpenChestPanel : MonoBehaviour
{
    [HideInInspector]
    public int MaxHealth_MaxLimit=11;
    [HideInInspector]
    public int MaxHealth_MinLimit=5;

    int CurrentHealth;
    int MaxHealth;

    [HideInInspector]
    public int Damage_MaxLimit = 16;
    [HideInInspector]
    public int Damage_MinLimit = 8;


    public TextMeshProUGUI index;
    public Slider HealthBar;

    public Button LevelUpButton;
    public GameObject UpgradeChestPanel;
    public FloatingObject floatingObjectPref;
    GameObject shadow;
    Transform floatingPoint;
    GameObject Chest;
    Image ChestImage;
    Animator animator;

    GameObject ChestCollideGround_Effect;
    GameObject ClickOnChest_Effect;
    GameObject ChestBroken_Effect;

    List<Sprite> ChestImageList;

    public GetEquipmentPanel getEquipmentPanel;

    public static OpenChestPanel Instance { get; private set; }
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

        ChestImageList = new List<Sprite>(Resources.LoadAll<Sprite>("Chest Treasure Image"));
    }

    void Start()
    {
        MaxHealth = UnityEngine.Random.Range(MaxHealth_MinLimit, MaxHealth_MaxLimit) * 10;
        HealbarUI();

        floatingPoint = transform.Find("Floating Point");
        Chest = transform.Find("Chest").gameObject;
        ChestImage = Chest.GetComponent<Image>();
        animator = transform.GetComponent<Animator>();
        shadow = transform.Find("Shadow").gameObject;

        ChestCollideGround_Effect = transform.Find("Chest Collide Ground _Effect").gameObject;
        ClickOnChest_Effect = transform.Find("Click On Chest _Effect").gameObject;
        ChestBroken_Effect = transform.Find("Chest Broken _Effect").gameObject;

        LevelUpButton.onClick.AddListener(LevelUpFunc);

        Invoke("ChestAFK_animation", 10f);
    }

    public void ClickOnChest()
    {
        ClickOnChest_Effect.gameObject.SetActive(false);
        ClickOnChest_Effect.gameObject.SetActive(true);
        ClickOnChest_Effect.transform.position = Input.mousePosition;

        int AttackDamage = UnityEngine.Random.Range(Damage_MinLimit, Damage_MaxLimit);
        CurrentHealth -= AttackDamage;
        if (CurrentHealth > 0)
        {
            index.SetText(CurrentHealth + "/" + MaxHealth);
            HealthBar.value = CurrentHealth;
            FloatingObject foPref = Instantiate(floatingObjectPref, floatingPoint.position, floatingPoint.rotation, floatingPoint);
            foPref.Iniatialize(AttackDamage.ToString(), Color.red);

            animator.Play("Click");

            //if 10s dont click - play idle animation
            CancelInvoke("ChestAFK_animation");
            Invoke("ChestAFK_animation", 10f);
        }
        else
        {
            Chest.SetActive(false);
            HealthBar.gameObject.SetActive(false);
            shadow.SetActive(false);

            ChestBroken_Effect.SetActive(false);
            ChestBroken_Effect.SetActive(true);

            Invoke("EarnChest",0.5f);
        }
    }

    void ChestAFK_animation()
    {
        animator.Play("AFK");
    }

    public void InitializeChest()
    {
        if (DataManager.Instance.temporaryData.GetValue_Float(Item.Type.Chest) <= 0)
        {
            InformManager.Instance.Initialize_FloatingInform("You dont have anymore chest");
            return;
        }

        HealthBar.gameObject.SetActive(true);
        Chest.SetActive(true);
        ChestImage.sprite = ChestImageList[UnityEngine.Random.Range(0, ChestImageList.Count)];
        shadow.SetActive(true);

        animator.Play("Appear");

        MaxHealth = UnityEngine.Random.Range(MaxHealth_MinLimit, MaxHealth_MaxLimit) * 10;
        HealbarUI();
    }

    public void EarnChest()
    {
        ResourceManager.Instance.ChangeGold(20,TemporaryData.ChangeType.ADDING);
        RandomEquipment();

        MissionManager.Instance.DoingMission_EarnChest();
    }

    public void RandomEquipment()
    {
        Equipment NewEquipment = ChestManager.Instance.RandomEquipment();

        getEquipmentPanel.gameObject.SetActive(true);
        getEquipmentPanel.SetNewEquipment(NewEquipment);

        Equipment OldEquipment = EquipmentManager.Instance.Get(NewEquipment.type);
        getEquipmentPanel.SetOldEquipment(OldEquipment);

        getEquipmentPanel.NewPanel.SetActive(true);

        ResourceManager.Instance.ChangeChest(1, TemporaryData.ChangeType.USING);
    }

    void LevelUpFunc()
    {
        UpgradeChestPanel.SetActive(true);
    }

    public void ChestColliderWithGround()//in Animation
    {
        ChestCollideGround_Effect.SetActive(false);
        ChestCollideGround_Effect.SetActive(true);
    }

    void HealbarUI()
    {
        CurrentHealth = MaxHealth;
        HealthBar.maxValue = MaxHealth;
        HealthBar.value = CurrentHealth;
        index.SetText(CurrentHealth + "/" + MaxHealth);
    }
}
