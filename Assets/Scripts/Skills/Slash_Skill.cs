using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Slash_Skill : Skill {

	private Slash_Data data;
	GameObject instanceSlash;

	public override void Init(PlayerController pc){
		playerController = pc;
		data = SkillManager.instance.slash_data;
		eButton = data.eButton;
		instanceSlash = Instantiate(data.SlashGameObject);
		instanceSlash.transform.parent = transform;
		instanceSlash.transform.localPosition = Vector3.zero;
		instanceSlash.transform.localRotation =  Quaternion.Euler(Vector3.zero);
	}
	public override void Execute(){

        if (inCooldown || isActive)
        {
            return;
        }

        base.Execute();

        isActive = true;
		instanceSlash.transform.localRotation = Quaternion.Euler(0f, (data.Amplitude/2), 0f);
		instanceSlash.SetActive(true);
		instanceSlash.transform.DOLocalRotate(new Vector3(0f, -(data.Amplitude/2f), 0f), data.HitSpeed).OnComplete(()=>{
			instanceSlash.SetActive(false);
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
		Destroy(instanceSlash);
	}
}
