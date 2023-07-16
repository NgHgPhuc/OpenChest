using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ActionButtonPanel : MonoBehaviour
{
    public GameObject chosenEffect;
    Button CurrentChosenButton;
    public List<Button> actionButton;
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
    }
}
