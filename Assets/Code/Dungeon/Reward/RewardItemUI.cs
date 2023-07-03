using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RewardItemUI : MonoBehaviour
{
    Image Icon;
    TextMeshProUGUI Mount;
    void Start()
    {
        Icon = transform.Find("Item Icon").GetComponent<Image>();
        Mount = transform.Find("Amount").GetComponent<TextMeshProUGUI>();
    }

    public void SetRewardItem(Reward reward)
    {
        if(Icon == null || Mount == null)
        {
            Icon = transform.Find("Item Icon").GetComponent<Image>();
            Mount = transform.Find("Amount").GetComponent<TextMeshProUGUI>();
        }

        gameObject.SetActive(true);
        Icon.sprite = reward.item.Icon;
        Mount.SetText(reward.Mount.ToString());
    }

    public void NoneReward()
    {
        gameObject.SetActive(false);
    }
}
