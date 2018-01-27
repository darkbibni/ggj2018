using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	private PlayerController pc;
	void Start () {
		pc = GetComponent<PlayerController>();
		pc.AddSkill(new Slash_Skill());
	}
}
