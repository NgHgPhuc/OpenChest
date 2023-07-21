using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingUnitAnimation : MonoBehaviour
{
    public void UnitAttackInAnimation(int Times = 1)
    {
        TurnManager.Instance.AttackInAnimation(Times);
    }

    public void EndAttackInAnimation()
    {
        TurnManager.Instance.EndCurrentTurn();
    }
}
