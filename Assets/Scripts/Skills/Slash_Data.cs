using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Slash_Data : ScriptableObject
{
    public GameObject SlashGameObject;
	public float Amplitude = 120f;
	public float HitSpeed = 0.2f;
	public SkillButton eButton = SkillButton.X;
}
