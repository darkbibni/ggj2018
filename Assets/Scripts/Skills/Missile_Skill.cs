using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Missile_Skill : Skill {

	private Missile_Data data;
	private bool transmitted = false;
	GameObject instanceMissile;
	Renderer PlayerRenderer;
	BoxCollider PlayerCollider;

	public override void Init(PlayerController pc){
		playerController = pc;
		PlayerRenderer = GetComponent<Renderer>();
		PlayerCollider = GetComponent<BoxCollider>();
		data = SkillManager.instance.missile_data;
		eButton = data.eButton;
		instanceMissile = Instantiate(data.MissileGameObject);
		instanceMissile.transform.parent = transform;
		instanceMissile.transform.localPosition = Vector3.zero;
		instanceMissile.transform.localRotation =  Quaternion.Euler(Vector3.zero);
	}
	public override void Execute(){
		if(!isActive){
			ShowPlayer(false);
			instanceMissile.SetActive(true);
			isActive = true;
		}
	}

	void ShowPlayer(bool value){
		PlayerCollider.enabled = value;
		PlayerRenderer.enabled = value;
		playerController.ShowTrails(value);
	}

	void OnTriggerEnter(Collider other)
	{
		if(gameObject.activeSelf && other.tag == "Player" && other.transform != transform && !transmitted){
			transmitted = true;
			ShowPlayer(true);
			instanceMissile.SetActive(false);
			PlayerController pc = other.GetComponent<PlayerController>();
			pc.AddSkill(this);
			pc.SkillMove.Stun(2.0f);
			playerController.RemoveSkill(this, eButton);
			Destroy(this);
		}else{
			ShowPlayer(true);
			instanceMissile.SetActive(false);
			isActive = false;
		}
	}

	void OnDestroy()
	{
		Destroy(instanceMissile);
	}
}
