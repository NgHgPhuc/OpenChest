using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class InformObject : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Paragraph;
    public Button ConfirmRightButton;
    public Button ConfirmMiddleButton;
    public Button NoLeftButton;

    public void Initialize_InformObject(string Title,string Paragraph,UnityAction ConfirmEvent)
    {
        this.Title.SetText(Title);
        this.Paragraph.SetText(Paragraph);

        ConfirmRightButton.gameObject.SetActive(false);
        NoLeftButton.gameObject.SetActive(false);
        ConfirmMiddleButton.gameObject.SetActive(true);

        ConfirmMiddleButton.onClick.AddListener(ConfirmEvent);
    }

    public void Initialize_QuestionObject(string Title, string Paragraph, UnityAction ConfirmEvent, UnityAction NoEvent)
    {
        this.Title.SetText(Title);
        this.Paragraph.SetText(Paragraph);

        ConfirmRightButton.gameObject.SetActive(true);
        NoLeftButton.gameObject.SetActive(true);
        ConfirmMiddleButton.gameObject.SetActive(false);

        ConfirmRightButton.onClick.RemoveAllListeners();
        ConfirmRightButton.onClick.AddListener(ConfirmEvent);
        NoLeftButton.onClick.AddListener(NoEvent);
    }

}
