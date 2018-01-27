using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gun_Data : ScriptableObject
{
    public Sprite sprite;
    public GameObject Gun_ProjectilePrefab;
    public float speed;
    public float cooldown;
    public SkillButton eButton = SkillButton.X;
}
