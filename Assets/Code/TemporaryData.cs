using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;

[CreateAssetMenu(fileName = "TemporaryData", menuName = "ScriptableObjects/TemporaryData")]
public class TemporaryData : ScriptableObject
{
    public Dictionary<string, string> s = new Dictionary<string, string>();

    public bool LoadValueFromCloud(string Name,string value)
    {
        if (s.ContainsKey(Name))
            return false;

        s.Add(Name, value);
        return true;
    }

    public float GetValue(string Name)
    {
        if (!s.ContainsKey(Name))
            return 0f;

        float v = (float)Convert.ToDouble(s[Name]);
        return v;
    }

    public void WriteValue(string Name,string value)
    {
        if (s.ContainsKey(Name))
            s.Add(Name, value.ToString(CultureInfo.InvariantCulture));
        else
            s[Name] = value.ToString(CultureInfo.InvariantCulture);

    }
}
