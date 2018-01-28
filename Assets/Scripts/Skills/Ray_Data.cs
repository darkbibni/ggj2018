using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ray_Data : ScriptableObject
{ 
    public Sprite sprite;
    public float chargeDuration;
    public float rayDuration;
    public Color rayColorCharging;
    public Color rayColorAttacking;
    public LayerMask layerMask;
    public SkillButton eButton = SkillButton.A;
}
