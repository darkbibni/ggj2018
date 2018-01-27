using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour {
	public bool isActive = false;
	public float cooldown;
	public abstract void Execute();
}
