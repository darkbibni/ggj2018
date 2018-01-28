using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stomp_Data : ScriptableObject
{
    public Sprite sprite;
    public GameObject StompGameObject;
    public GameObject StompVfx;
	public float HitSpeed = 0.2f;
	public SkillButton eButton = SkillButton.X;
}
