using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public List<Texture2D> l;
    public Image m;
    public Shader s;
    int i = 0;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            m.material.SetTexture("_MainTex", l[i++]);

            Material newMat = new Material(s);
            newMat.CopyPropertiesFromMaterial(m.material);
            m.material = newMat;
            if (i >= l.Count)
                i = 0;
        }
    }
}
