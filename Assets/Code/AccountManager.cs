using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class AccountManager : MonoBehaviour
{
    public GameObject loginPanel;
    public Button LoginButton;
    void Start()
    {
        Application.targetFrameRate = 60;
        LoginButton.onClick.AddListener(() => { loginPanel.SetActive(true); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    //REGISTER ACCOUNT
    public void RegisterAccount(string username,string password)
    {
        var request = new RegisterPlayFabUserRequest
        {
            Username = username,
            Password = password,

            RequireBothUsernameAndEmail = false,
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFail);
    }

    private void OnRegisterFail(PlayFabError obj)
    {
        print(obj.ToString());
        InformManager.Instance.Initialize_FloatingInform(obj.ErrorMessage);
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult obj)
    {
        InformManager.Instance.Initialize_FloatingInform("Register Successfully");
    }


    //LOGIN ACCOUNT
    public void LoginAccount(string username,string password)
    {
        var request = new LoginWithPlayFabRequest
        {
            Username = username,
            Password = password,
        };

        PlayerPrefs.SetString("Username", username);
        PlayerPrefs.SetString("Password", password);

        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFail);
    }

    private void OnLoginFail(PlayFabError obj)
    {
        InformManager.Instance.Initialize_FloatingInform("Wrong Password or Username! Try Again!");
    }

    private void OnLoginSuccess(LoginResult obj)
    {
        
        InformManager.Instance.Initialize_FloatingInform("Login Successfully");
        LoadDataWhenLogin.Instance.LoadData();
    } 
}
