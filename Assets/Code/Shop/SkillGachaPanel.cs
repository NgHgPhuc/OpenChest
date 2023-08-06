using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using static Equipment;

public class SkillGachaPanel : MonoBehaviour
{
    public ReceivePanel receivePanel;

    List<BaseSkill> RateInfomation = new List<BaseSkill>();

    List<float> RandomProb = new List<float>() { 70, 90, 99, 100 };
    // 0 - 70 white , 70 - 90 - green , 90 - 99 - blue , 99 - 100 purple

    int times;
    void Start()
    {
        List<BaseSkill> skillOfPlayers = new List<BaseSkill>(Resources.LoadAll<BaseSkill>("Skill"));
        skillOfPlayers = skillOfPlayers.FindAll(s => s.userType == BaseSkill.UserType.Player);
        foreach (BaseSkill s in skillOfPlayers)
            RateInfomation.Add(s);
    }



    public void RandomSkill(int times)
    {
        this.times = times;

        string paragraph = "Are you sure about play this gacha? \n You can some stronger Skill, but only when you lucky hehe\n Happy Happy Happy!";
        InformManager.Instance.Initialize_QuestionObject("Gacha " + times * 160 + " Diamond", paragraph, AcceptRandom);
    }

    void AcceptRandom()
    {
        float DiamondHave = DataManager.Instance.temporaryData.GetValue_Float(Item.Type.Diamond);
        if (DiamondHave < times * 160)
        {
            InformManager.Instance.Initialize_InformObject("No Enough Diamond", "you dont have enough diamond!", null);
            return;
        }

        List<BaseSkill> getSkillList = new List<BaseSkill>();
        ResourceManager.Instance.ChangeDiamond(times * 160, TemporaryData.ChangeType.USING);
        for (int i = 0; i < times; i++)
        {
            int r = UnityEngine.Random.Range(0, RateInfomation.Count);

            //if (RateInfomation[r].IsHave == false)
            //    RateInfomation[r].IsHave = true;
            //else RateInfomation[r].CurrentSharp += 20;

            getSkillList.Add(RateInfomation[r]);

            //DataManager.Instance.SaveData(RateInfomation[r].Name, RateInfomation[r].ToStringData());

        }

        receivePanel.SetReceiveList_Skill(getSkillList);
    }
}
