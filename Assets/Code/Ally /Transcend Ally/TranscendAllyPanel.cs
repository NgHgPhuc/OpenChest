using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TranscendAllyPanel : MonoBehaviour
{
    Character character;

    public StarListPanel currentStarList;
    public StarListPanel nextStarList;
    public TranscendPowerPanel transcendPowerPanel;
    public TranscendMaterialPanel transcendMaterialPanel;
    public Button transcendButton;

    public void SetTranscendUI(Character character)
    {
        if(character == null)
        {
            this.character = null;
            return;
        }

        if(character.IsOwn == false)
            transcendButton.gameObject.SetActive(false);
        else
            transcendButton.gameObject.SetActive(true);

        this.character = character;

        int currentStarCount = this.character.currentStarCount;
        int maxStarCount = this.character.maxStarCount;
        currentStarList.SetStarCount(currentStarCount, maxStarCount,5);
        nextStarList.SetStarCount(currentStarCount + 1, maxStarCount,5);

        transcendPowerPanel.SetStarPowerUI(this.character.DescriptionTranscendLevel);
        transcendPowerPanel.SetUpgradeStarUI(currentStarCount);
    }
}
