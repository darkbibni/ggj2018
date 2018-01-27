﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Stomp_Skill : Skill {

	private Stomp_Data data;
	GameObject instanceStomp;

	public override void Init(PlayerController pc){
		playerController = pc;
		data = SkillManager.instance.stomp_data;
		eButton = data.eButton;
		instanceStomp = Instantiate(data.StompGameObject);
		instanceStomp.transform.parent = transform;
		instanceStomp.transform.localPosition = Vector3.zero;
		instanceStomp.transform.localRotation =  Quaternion.Euler(Vector3.zero);
	}
	public override void Execute(){

        if (inCooldown || isActive)
        {
            return;
        }

        base.Execute();
        
		instanceStomp.transform.localRotation = Quaternion.Euler(-20f, 0f, 0f);
		instanceStomp.SetActive(true);
		instanceStomp.transform.DOLocalRotate(new Vector3(90f, 0f, 0f), data.HitSpeed).OnComplete(()=>{
			instanceStomp.SetActive(false);
            End();
        });
		
	}

	void OnTriggerEnter(Collider other)
	{
		if(gameObject.activeSelf && other.tag == "Player" && other.transform != transform && !isTransmitted){
			isTransmitted = true;
			PlayerController pc = other.GetComponent<PlayerController>();
			pc.AddSkill(this);
			playerController.RemoveSkill(this, eButton);
			Destroy(this);
		}
	}

	void OnDestroy()
	{
		Destroy(instanceStomp);
	}
}