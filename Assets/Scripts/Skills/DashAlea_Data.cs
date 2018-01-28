using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DashAlea_Data : ScriptableObject
{
    public Sprite sprite;
    public float distance;
    public float duration;
    public SkillButton eButton = SkillButton.A;
}
