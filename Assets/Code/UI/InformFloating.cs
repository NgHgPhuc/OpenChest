using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InformFloating : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI mes;
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize_InformFloating(string mes)
    {
        if (this.mes == null)
            this.mes = GetComponentInChildren<TextMeshProUGUI>();

        this.mes.SetText(mes);
    }

}
