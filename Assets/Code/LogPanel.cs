using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class LogPanel : MonoBehaviour
{
    public AccountManager accountManager;
    public TemporaryData temporaryData;

    [Header("Login Method")]
    public GameObject LoginMethodForm;
    public Button LoginByGameAccount;
    public Button LoginByGoogle;
    public Button LoginByPlay;

    [Header("Login")]
    public Button LoginReturnButton;
    public GameObject LoginForm;
    public TMP_InputField LoginUsername;
    public TMP_InputField LoginPassword;
    public Button TurnToRegister;
    public Button LoginButton;

    [Header("Register")]
    public Button RegisterReturnButton;
    public GameObject RegisterForm;
    public TMP_InputField RegisterUsername;
    public TMP_InputField RegisterPassword;
    public TMP_InputField RegisterRe_Password;
    public Button TurnToLogin;
    public Button RegisterButton;
    void Start()
    {
        //LOGIN METHOD
        LoginByGameAccount.onClick.AddListener(LoginByGameAccount_Func);
        LoginByGoogle.onClick.AddListener(LoginByGoogle_Func);
        LoginByPlay.onClick.AddListener(LoginByPlay_Func);

        //LOGIN FORM
        LoginReturnButton.onClick.AddListener(LoginReturnButton_Func);
        TurnToRegister.onClick.AddListener(TurnToRegister_Func);
        LoginButton.onClick.AddListener(LoginButton_Func);

        //REGISTER FORM
        RegisterReturnButton.onClick.AddListener(LoginReturnButton_Func);//same
        TurnToLogin.onClick.AddListener(TurnToLogin_Func);
        RegisterButton.onClick.AddListener(RegisterButton_Func);
    }

    private void OnEnable()
    {
        LoginMethodForm.SetActive(true);
        LoginForm.SetActive(false);
        RegisterForm.SetActive(false);

    }

    //LOGIN METHOD BUTTON FUNCTION
    void LoginByGameAccount_Func()
    {
        LoginMethodForm.SetActive(false);
        LoginForm.SetActive(true);
    }
    void LoginByGoogle_Func()
    {
        InformManager.Instance.Initialize_FloatingInform("Login By Google is currently not working!");
    }
    void LoginByPlay_Func()
    {
        InformManager.Instance.Initialize_FloatingInform("Login By Play is currently not working!");
    }


    //LOGIN FORM
    void LoginReturnButton_Func()
    {
        LoginForm.SetActive(false);
        RegisterForm.SetActive(false);
        LoginMethodForm.SetActive(true);
    }
    void TurnToRegister_Func()
    {
        LoginForm.SetActive(false);
        RegisterForm.SetActive(true);
    }
    public void LoginButton_Func()
    {
        accountManager.LoginAccount(this.LoginUsername.text, this.LoginPassword.text);
    }


    //REGISTER FORM
    void TurnToLogin_Func()
    {
        LoginForm.SetActive(true);
        RegisterForm.SetActive(false);
    }

    public void RegisterButton_Func()
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

    public void BackgroundClick()
    {
        gameObject.SetActive(false);
    }
}
