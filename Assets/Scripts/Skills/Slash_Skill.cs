using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Slash_Skill : Skill {

	private Slash_Data data;
	GameObject instanceSlash;

	public override void Init(PlayerController pc){
		caster = pc;
		data = SkillManager.instance.slash_data;
		eButton = data.eButton;
		instanceSlash = Instantiate(data.SlashGameObject);
		instanceSlash.transform.parent = transform;
		instanceSlash.transform.localPosition = Vector3.zero;
		instanceSlash.transform.localRotation =  Quaternion.Euler(Vector3.zero);
	}
	public override void Execute(List<Skill> _skillsToRemove)
    {

        if (isActive)
        {
            return;
        }

        base.Execute(_skillsToRemove);

		instanceSlash.transform.localRotation = Quaternion.Euler(0f, (data.Amplitude/2), 0f);
		instanceSlash.SetActive(true);
		instanceSlash.transform.DOLocalRotate(new Vector3(0f, -(data.Amplitude/2f), 0f), data.HitSpeed).OnComplete(()=>{
			instanceSlash.SetActive(false);
            End();
		});
	}

	void OnTriggerEnter(Collider other)
	{
		if(isActive && other.tag == "Player" && other.transform != transform && !isTransmitted){
			isTransmitted = true;
			PlayerController enemy = other.GetComponent<PlayerController>();
            caster.TransmitToEnemy(skillsToRemove, eButton, enemy);
            Destroy(this);
		}
    }

    void OnDestroy()
	{
		Destroy(instanceSlash);
	}
}
