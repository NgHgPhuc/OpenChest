using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ActionButtonPanel : MonoBehaviour
{
    public GameObject CurrentChosenButtonEffect;
    void Start()
    {

    }

    public void OnButtonEffectClick(Transform ImageButton)
    {
        CurrentChosenButtonEffect.SetActive(true);
        CurrentChosenButtonEffect.transform.SetParent(ImageButton);
    }
}
