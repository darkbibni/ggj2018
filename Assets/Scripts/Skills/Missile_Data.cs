using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Missile_Data : ScriptableObject
{
    public Sprite sprite;
    public GameObject MissileGameObject;
	public float MoveSpeed = 50f;
	public float AngleSpeed = 25f;
	public SkillButton eButton = SkillButton.Y;
}
