using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WardPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject OpenChestPanel;
    public GameObject DungeonPanel;
    public GameObject ShopPanel;
    public GameObject AllyPanel;
    public GameObject OpenChestButton;

    GameObject CurrentGameObject;
    GameObject CurrentButton;

    public static WardPanel Instance { get; private set; }
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
        CurrentGameObject = OpenChestPanel;
        CurrentButton = OpenChestButton;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimationButtonChosen(GameObject button)
    {
        if (CurrentButton == button)
            return;

        CurrentButton.GetComponent<Animator>().Play("Close");
        CurrentButton = button;
        CurrentButton.GetComponent<Animator>().Play("Open");
    }

    public void DungeonWarp()
    {
        SetCurrentGameObject(DungeonPanel);
    }

    public void OpenChestWarp()
    {
        SetCurrentGameObject(OpenChestPanel);
    }

    public void ShopWarp()
    {
        SetCurrentGameObject(ShopPanel);
    }

    public void AllyWarp()
    {
        SetCurrentGameObject(AllyPanel);
    }

    public void SetCurrentGameObject(GameObject g)
    {
        if (CurrentGameObject == g)
            return;

        if (CurrentGameObject != null)
        {
            CurrentGameObject.SetActive(false);
        }

        CurrentGameObject = g;
        CurrentGameObject.SetActive(true);
    }
}
