using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Missile_Data : ScriptableObject
{
    public Sprite sprite;
    public GameObject MissileGameObject;
    public GameObject explosionVfx;

	public SkillButton eButton = SkillButton.Y;

    public float MoveSpeed = 50f;
    public float AngleSpeed = 25f;
    public float missileDuration = 10f;
}
