using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class LogPanel : MonoBehaviour
{
    public AccountManager accountManager;

    [Header("Login")]
    public GameObject LoginObject;
    public TMP_InputField LoginUsername;
    public TMP_InputField LoginPassword;
    public Button LoginButton;

    [Header("Register")]
    public GameObject RegisterObject;
    public TMP_InputField RegisterUsername;
    public TMP_InputField RegisterPassword;
    public TMP_InputField RegisterRe_Password;
    public Button RegisterButton;
    void Start()
    {
        LoginButton.onClick.AddListener(LoginButtonClick);
        RegisterButton.onClick.AddListener(RegisterButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnToRegister()
    {
        LoginObject.SetActive(false);
        RegisterObject.SetActive(true);
    }

    public void TurnToLogin()
    {
        LoginObject.SetActive(true);
        RegisterObject.SetActive(false);
    }

    public void BackgroundClick()
    {
        gameObject.SetActive(false);
    }
    public void LoginButtonClick()
    {
        accountManager.LoginAccount(this.LoginUsername.text, this.LoginPassword.text);
    }

    public void RegisterButtonClick()
    {
        if(this.RegisterPassword.text.Length < 6)
        {
            InformManager.Instance.Initialize_FloatingInform("Password must longer than 5 characters");
            return;
        }


        if (this.RegisterPassword.text != this.RegisterRe_Password.text)
        {
            InformManager.Instance.Initialize_FloatingInform("Password and re-Password is not duplicated!");
            return;
        }

        accountManager.RegisterAccount(this.RegisterUsername.text, this.RegisterPassword.text);
    }
}
