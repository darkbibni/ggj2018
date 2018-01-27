using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stomp_Data : ScriptableObject
{
    public GameObject StompGameObject;
	public float HitSpeed = 0.2f;
	public SkillButton eButton = SkillButton.X;
}
