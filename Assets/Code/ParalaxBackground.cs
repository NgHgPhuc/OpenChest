using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxBackground : MonoBehaviour
{
    public float dist;
    public List<GameObject> Part=new List<GameObject>();
    Vector3 LoopPos;
    void Start()
    {
        Part.Add(transform.GetChild(0).gameObject);
        Part.Add(transform.GetChild(1).gameObject);
        Part.Add(transform.GetChild(2).gameObject);

        LoopPos = Part[2].transform.position+new Vector3(720,0,0);
        Debug.Log(Part[2].transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < 3; i++)
        {
            Part[i].transform.position = new Vector3(Part[i].transform.position.x - dist, Part[i].transform.position.y, Part[i].transform.position.z);
        }
          

        for (int i = 0; i < 3; i++)
            if (Part[i].transform.position.x <= -1440)
            {
                Part[i].transform.position = LoopPos;
            }
    }
}
