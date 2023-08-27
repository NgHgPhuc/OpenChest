using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionButtonPanel : MonoBehaviour
{
    public GameObject chosenEffect;
    Button CurrentChosenButton;
    public List<Button> actionButton;
    public Button SpeedUpButton;
    TextMeshProUGUI SpeedUpText;
    int CurrentSpeedUp;

    public Button AutoButton;
    bool IsAuto;
    void Start()
    {
        foreach (Button b in actionButton)
        {
            b.onClick.AddListener(() =>
            {
                if (CurrentChosenButton != b)
                {
                    CurrentChosenButton = b;
                    Transform child = b.gameObject.transform.Find("Icon Button");
                    chosenEffect.transform.SetParent(child);
                    chosenEffect.transform.position = child.position;
                }

            });
        }

        SpeedUpButton.onClick.AddListener(SpeedUpButton_Click);
        SpeedUpText = SpeedUpButton.transform.GetComponentInChildren<TextMeshProUGUI>();
        CurrentSpeedUp = 1;
        FightManager.Instance.SetGameSpeed(CurrentSpeedUp);

        IsAuto = false;
        AutoButton.onClick.AddListener(AutoButton_Click);
    }

    void SpeedUpButton_Click()
    {
        CurrentSpeedUp = (CurrentSpeedUp < 3) ? CurrentSpeedUp+1 : 1;
        SpeedUpText.SetText("x"+CurrentSpeedUp);
        FightManager.Instance.SetGameSpeed(CurrentSpeedUp);
    }

    void AutoButton_Click()
    {
        IsAuto = !IsAuto;
        FightManager.Instance.SetAuto(IsAuto);
    }
}
