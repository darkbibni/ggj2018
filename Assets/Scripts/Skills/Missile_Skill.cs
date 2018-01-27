using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Missile_Skill : Skill {

	private Missile_Data data;
	private bool transmitted = false;
	GameObject instanceMissile;
	GameObject playerModel;
	Collider PlayerCollider;

	public override void Init(PlayerController pc){
		playerController = pc;
        playerModel = transform.GetChild(0).gameObject;
		PlayerCollider = GetComponent<Collider>();
		data = SkillManager.instance.missile_data;
		eButton = data.eButton;
		instanceMissile = Instantiate(data.MissileGameObject);
		instanceMissile.transform.parent = transform;
        instanceMissile.transform.localPosition = Vector3.zero;
		instanceMissile.transform.localRotation =  Quaternion.Euler(Vector3.zero);
	}

	public override void Execute(List<Skill> _skillsToRemove)
    {
		if(!isActive){
			ShowPlayer(false);
			instanceMissile.SetActive(true);
			isActive = true;
		}
	}

	void ShowPlayer(bool value){
		PlayerCollider.enabled = value;
        playerModel.gameObject.SetActive(value);
		playerController.ShowTrails(value);
	}

	void OnTriggerEnter(Collider other)
	{
        if(other.tag == "Ground" || other.tag == "Exit")
        {
            return;
        }

		if(gameObject.activeSelf && other.tag == "Player" && other.transform != transform && !transmitted){
			transmitted = true;
			ShowPlayer(true);
			instanceMissile.SetActive(false);
			PlayerController enemy = other.GetComponent<PlayerController>();
            enemy.SkillMove.Stun(2.0f);
            playerController.TransmitToEnemy(skillsToRemove, eButton, enemy);
            Destroy(this);
		}

        else {
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
