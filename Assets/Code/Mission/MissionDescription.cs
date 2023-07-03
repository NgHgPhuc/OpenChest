using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionDescription : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI MissionName;
    TextMeshProUGUI Progress;
    void Start()
    {
        SetAttr();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDescriptionUI(Mission mission)
    {
        SetAttr();

        MissionName.SetText(mission.Index +"."+ mission.Name);
        Progress.SetText(mission.GetProgress());

        if (mission.IsDone == true)
            Progress.color = Color.green;
        else Progress.color = Color.red;
    }

    void SetAttr()
    {
        if (MissionName != null && Progress != null)
            return;

        MissionName = transform.Find("Mission Name").GetComponent<TextMeshProUGUI>();
        Progress = transform.Find("Progress").GetComponent<TextMeshProUGUI>();
    }
}
