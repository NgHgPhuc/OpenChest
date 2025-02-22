using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BuffOfUnit : MonoBehaviour
{
    Character allyStats = new Character();
    public List<Buff> buffs = new List<Buff>();
    void Start()
    {
           
    }

    public void SetCloneAllyStats(Character character)
    {
        allyStats = character.Clone();
    }

    public void AddBuff(Buff buff)
    {
        buffs.Add(buff);

        if (buff.Activation != null)
            buff.Activation();
    }

    public void EndBuff(Buff buff)
    {
        if (buff.Deactivation != null)
            buff.Deactivation();
    }

    public int OnTurnBuff(Buff buff)
    {
        buff.duration -= 1;

        if (buff.duration == 0)
        {
            EndBuff(buff);
            return buff.duration;
        }

        return buff.duration;
    }

    public void CheckBuff()
    {
        buffs.RemoveAll(buff => OnTurnBuff(buff) == 0);
    }

    public void OnactivationBuff()
    {
        foreach(Buff buff in buffs)
            if (buff.Onactivation != null)
                buff.Onactivation();
    }
}
