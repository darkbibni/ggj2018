﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum testType{
	Stomp, Slash, Missile
}

public class WeaponTest : MonoBehaviour {

	public testType type;
	private PlayerController pc;

	void Start () {
		pc = GetComponent<PlayerController>();
		switch(type){
			case testType.Stomp : pc.AddSkill(new Stomp_Skill());
			break;
			case testType.Slash : pc.AddSkill(new Slash_Skill());
			break;
			case testType.Missile : pc.AddSkill(new Missile_Skill());
			break;
		}
	}
}
