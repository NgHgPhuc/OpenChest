using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BundleManager : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        
    }



    public void BuyGold_by_Diamond(float valueDiamond, float GetGold)
    {
        string paragraph = "Are you sure buying " + GetGold + " with " + valueDiamond + " ?\n If you confirm, both of us are happy happy happyyy!";
        InformManager.Instance.Initialize_QuestionObject("Buy gold", paragraph, () =>
        {
            if (ResourceManager.Instance.CheckEnough_Diamond(valueDiamond))
            {
                InformManager.Instance.Initialize_FloatingInform("You dont enough diamond");
                return;
            }

            InformManager.Instance.Initialize_FloatingInform("Buying successfully");
            ResourceManager.Instance.ChangeDiamond(-valueDiamond);
            ResourceManager.Instance.ChangeGold(GetGold);
        });
    }
}
