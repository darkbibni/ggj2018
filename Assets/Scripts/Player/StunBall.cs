using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunBall : MonoBehaviour {

	public float RotationSpeed = 5f;
	void FixedUpdate () {
		transform.Rotate(0f, RotationSpeed, 0f);
	}
}
