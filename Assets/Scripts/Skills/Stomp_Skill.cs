using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Stomp_Skill : Skill {

	private Stomp_Data data;
	GameObject instanceStomp;
    GameObject stompVfx;

    public float stunDuration = 0.5f;

	public override void Init(PlayerController pc){
		caster = pc;
		data = SkillManager.instance.stomp_data;
		eButton = data.eButton;
        stompVfx = data.StompVfx;

        instanceStomp = Instantiate(data.StompGameObject);
		instanceStomp.transform.parent = transform;
		instanceStomp.transform.localPosition = Vector3.zero;
		instanceStomp.transform.localRotation =  Quaternion.Euler(Vector3.zero);
	}

	public override void Execute(List<Skill> _skillsToRemove)
    {
        if (isActive)
        {
            return;
        }

        base.Execute(_skillsToRemove);
        
		instanceStomp.transform.localRotation = Quaternion.Euler(-20f, 0f, 0f);
		instanceStomp.SetActive(true);
		instanceStomp.transform.DOLocalRotate(new Vector3(90f, 0f, 0f), data.HitSpeed).OnComplete(()=>{
			instanceStomp.SetActive(false);
            SpawnHammerEffect(transform.position + transform.forward * 2f);
            End();
        });
    }

	void OnTriggerEnter(Collider other)
    {
        if (isActive && other.tag == "Player" && other.transform != transform && !isTransmitted){
			isTransmitted = true;
			PlayerController enemy = other.GetComponent<PlayerController>();
            enemy.SkillMove.Stun(stunDuration);
            caster.TransmitToEnemy(skillsToRemove, eButton, enemy);
            Destroy(this);
		}
	}

    private void SpawnHammerEffect(Vector3 pos)
    {
        GameObject vfx = Instantiate(stompVfx, pos, stompVfx.transform.rotation);
        Destroy(vfx, 5f);
    }

    void OnDestroy()
	{
		Destroy(instanceStomp);
	}
}
