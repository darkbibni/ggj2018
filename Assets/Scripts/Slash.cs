using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Slash : Skill {

	public Transform SlashGameObject;
	private ScriptableObject data;
	public float Amplitude = 120f;
	public float HitSpeed = 0.2f;
	public override void Execute(){
		if(!isActive){
			isActive = true;
			SlashGameObject.localRotation = Quaternion.Euler(0f, (Amplitude/2), 0f);
			SlashGameObject.gameObject.SetActive(true);
			SlashGameObject.DOLocalRotate(new Vector3(0f, -(Amplitude/2f), 0f), HitSpeed).OnComplete(()=>{ 
				SlashGameObject.gameObject.SetActive(false);
				isActive = false;
			});
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		if(gameObject.activeSelf && other.tag == "Player"){
			Debug.Log("Enemy touched");
		}
	}
}
