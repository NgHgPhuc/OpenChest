using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Globalization;

public class ZoomButton : MonoBehaviour,IPointerClickHandler
{
    public TextMeshProUGUI ZoomTimes;
    float zoomTimes = 1;
    public Transform Map;
    void Start()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (zoomTimes == 2)
            zoomTimes = 0.5f;
        else zoomTimes *= 2;

        ZoomTimes.SetText("x" + zoomTimes.ToString(CultureInfo.InvariantCulture));
        Map.transform.localScale = new Vector3(zoomTimes, zoomTimes, 1);
    }
}
