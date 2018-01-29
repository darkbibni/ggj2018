using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Missile_Skill : Skill {

	private Missile_Data data;
	GameObject instanceMissile;
	GameObject playerModel;
	Collider PlayerCollider;
    GameObject explosionVfx;

    public float stunDuration = 2f;

    private MeshRenderer meshRenderer;
    private Color originalColor;

	public override void Init(PlayerController pc){
		caster = pc;
        playerModel = transform.GetChild(0).gameObject;
		PlayerCollider = GetComponent<Collider>();

		data = SkillManager.instance.missile_data;
		eButton = data.eButton;
        explosionVfx = data.explosionVfx;

        instanceMissile = Instantiate(data.MissileGameObject);
		instanceMissile.transform.parent = transform;
        instanceMissile.transform.localPosition = Vector3.zero;
		instanceMissile.transform.localRotation =  Quaternion.Euler(Vector3.zero);

        meshRenderer = instanceMissile.transform.GetChild(0).GetComponent<MeshRenderer>();
        originalColor = meshRenderer.material.color;
    }

    private void SpeedUp()
    {
        caster.SkillMove.SpeedMultiplicator += 0.25f;
    }

	public override void Execute(List<Skill> _skillsToRemove)
    {
        if (isActive)
        {
            return;
        }

        base.Execute(_skillsToRemove);

		ShowPlayer(false);
		instanceMissile.SetActive(true);
        AudioManager.singleton.PlayAt(AudioManager.singleton.GetSFXclip("missileLaunch"), caster.audioSource);
        AudioManager.singleton.PlayLoop(AudioManager.singleton.GetSFXclip("missileLoop"), caster.audioSource);
        
        InvokeRepeating("SpeedUp", 1f, 1f);

        meshRenderer.material.color = originalColor;

        Invoke("Blink", data.missileDuration / 2);
        Invoke("StopMissile", data.missileDuration);
    }

	void ShowPlayer(bool value){
		PlayerCollider.enabled = value;
        playerModel.gameObject.SetActive(value);
		caster.ShowTrails(value);
	}

	void OnTriggerEnter(Collider other)
	{
        if(other.tag == "Ground" || other.tag == "Exit")
        {
            return;
        }

		if(isActive && other.tag == "Player" && other.transform != transform && !isTransmitted){
			isTransmitted = true;

			ShowPlayer(true);
			instanceMissile.SetActive(false);

			PlayerController enemy = other.GetComponent<PlayerController>();
            enemy.SkillMove.Stun(stunDuration);
            enemy.SkillMove.KnockBack(transform.forward, 50f);

            caster.TransmitToEnemy(skillsToRemove, eButton, enemy);

            RestoreSpeed();

            SpawnExplosion();
            AudioManager.singleton.PlayAt(AudioManager.singleton.GetSFXclip("missileBoom"), caster.audioSource);

            CancelInvoke("Blink");
            CancelInvoke("StopMissile");

            Destroy(this);
        }

        else if(isActive) {
			ShowPlayer(true);
			instanceMissile.SetActive(false);

            RestoreSpeed();

            SpawnExplosion();
            AudioManager.singleton.PlayAt(AudioManager.singleton.GetSFXclip("missileBoom"), caster.audioSource);
            End();

            CancelInvoke("Blink");
            CancelInvoke("StopMissile");
        }
	}

    private void Blink()
    {
        meshRenderer.material.DOColor(Color.red, data.missileDuration / 2);
    }

    private void StopMissile()
    {
        ShowPlayer(true);
        instanceMissile.SetActive(false);

        RestoreSpeed();

        SpawnExplosion();
        AudioManager.singleton.PlayAt(AudioManager.singleton.GetSFXclip("missileBoom"), caster.audioSource);
        End();
    }

    private void RestoreSpeed()
    {
        CancelInvoke("SpeedUp");
        caster.SkillMove.SpeedMultiplicator = 1f;
    }

    private void SpawnExplosion()
    {
        GameObject vfx = Instantiate(explosionVfx, transform.position, explosionVfx.transform.rotation);
        Destroy(vfx, 5f);

        Camera.main.DOShakePosition(0.75f, 2f, 8);
    }

	void OnDestroy()
	{
		Destroy(instanceMissile);
	}
}
