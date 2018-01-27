using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillButton{
	A = 0,
	B = 1,
	X = 2,
	Y = 3 
}


public abstract class Skill : MonoBehaviour {

	public PlayerController playerController;
	public SkillButton eButton;
	public bool isActive = false;
	public float cooldown;
	public abstract void Execute();
	public abstract void Init(PlayerController pc);
}
