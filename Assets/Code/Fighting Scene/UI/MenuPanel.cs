using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    public GameObject menuPanel;

    public GameObject BGM_Button;
    Animator BGM_animator;
    string BGM_State = "On";

    public GameObject EfS_Button;
    Animator EfS_animator;
    string EfS_State = "On";

    public static MenuPanel Instance { get; private set; }
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

    private void Start()
    {
        BGM_animator = BGM_Button.GetComponent<Animator>();
        EfS_animator = EfS_Button.GetComponent<Animator>();
    }

    public void ClickBGM()
    {
        BGM_State = (BGM_State == "On") ? "Off" : "On";
        string animation = "Turn " + BGM_State + " BGM";
        BGM_animator.Play(animation);
    }

    public void ClickEfS()
    {
        EfS_State = (EfS_State == "On") ? "Off" : "On";
        string animation = "Turn " + EfS_State + " Effect Sound";
        EfS_animator.Play(animation);
    }

    public void ClickBackground()
    {
        menuPanel.SetActive(false);
    }

    public void ClickMenuButton()
    {
        menuPanel.SetActive(true);
    }

    public void ClickExitButton()
    {
        SceneManager.LoadScene("Open Chest");
    }
}
