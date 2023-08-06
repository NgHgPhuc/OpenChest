using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AllyInfomationUI : MonoBehaviour
{
    [Header("Infomation Panel")]
    public StarListPanel StarList;
    public TextMeshProUGUI Quality;
    public TextMeshProUGUI Name;

    [Header("Character Panel")]
    public Image characterAvatar;
    public Animator characterAnimator;

    [Header("Tier Effect")]
    public Image TierEffect;

    [Header("Compain Panel")]
    public Image CompainIcon;
    public TextMeshProUGUI CompainButtonText;
    public Button CompainArrangeButton;

    [Header("Unlock Button")]
    public GameObject UnlockButton;

    [Header("Level Up Effect")]
    public GameObject levelUpEffect;

    public void SetAllyUI(Character character)
    {
        if (character == null)
            return;

        Set_InfomationPanel(character);
        Set_CharacterPanel(character);
        Set_IsCompain(character);
    }

    public void Set_InfomationPanel(Character character)
    {
        if (characterAvatar == null)
            return;

        int currentStarCount = character.currentStarCount;
        int maxStarCount = character.maxStarCount;
        StarList.SetStarCount(currentStarCount, maxStarCount,5);


        string tier = character.tier.ToString();
        Quality.SetText("[" + tier + "]");
        Quality.color = character.GetColor();

        Name.SetText(character.Name);

        TierEffect.color = character.GetColor();
    }

    public void Set_CharacterPanel(Character character)
    {
        if (this.characterAvatar == null)
            return;

        this.characterAnimator.runtimeAnimatorController = null;
        this.characterAvatar.sprite = character.Icon;
        if (character.IsOwn == false)
            this.characterAvatar.color = new Color(120f / 255, 120f / 255, 120f / 255);
        else
            this.characterAvatar.color = Color.white;

        string animatorLink = "Animator/" + character.Name + "/" + character.Name;
        this.characterAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(animatorLink);
    }

    public void Set_IsCompain(Character character)
    {
        bool IsOwnChar = character.IsOwn;
        CompainIcon.gameObject.SetActive(IsOwnChar);
        CompainButtonText.transform.parent.gameObject.SetActive(IsOwnChar);
        UnlockButton.SetActive(!IsOwnChar);

        if (IsOwnChar == false)
            return;

        bool IsInTeamChar = character.IsInTeam;
        string compainTypeText = !IsInTeamChar ? "Compain" : "Uncompain";
        CompainButtonText.SetText(compainTypeText);
        CompainIcon.gameObject.SetActive(IsInTeamChar);
        CompainArrangeButton.gameObject.SetActive(IsInTeamChar);
    }

    public void LevelUpEffect()
    {
        levelUpEffect.SetActive(false);
        levelUpEffect.SetActive(true);
    }
}
