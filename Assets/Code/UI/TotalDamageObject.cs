using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalDamageObject : MonoBehaviour
{
    float TotalValue;
    public TextMeshProUGUI TotalText;
    public Animator animator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetValue()
    {
        int totalInt = Mathf.CeilToInt(TotalValue);
        TotalText.SetText(totalInt.ToString());
    }
    public void AddValue(float value)
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        TotalValue += value;
        SetValue();
    }

    public void End()
    {
        //animator.Play("Total Damage Disable");
        //Invoke("Disable", 0.15f);
        TotalValue = 0;
        gameObject.SetActive(false);
    }
    void Disable()
    {
        gameObject.SetActive(false);
    }
}
