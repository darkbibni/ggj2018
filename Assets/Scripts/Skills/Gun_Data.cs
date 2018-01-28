using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gun_Data : ScriptableObject
{
    public Sprite sprite;
    public GameObject Gun_ProjectilePrefab;
    public GameObject shootVfx;
    public float speed;
    public SkillButton eButton = SkillButton.X;
}
