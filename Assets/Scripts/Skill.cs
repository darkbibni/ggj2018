using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour {
	public bool isActive = false;
	public float cooldown;

    private float timerCooldown = 0;

    public virtual void Execute()
    {
        if(timerCooldown > 0 || isActive)
        {
            return;
        }

        isActive = true;
        timerCooldown = cooldown;
    }

    private void Update()
    {
        timerCooldown -= Time.deltaTime;
    }

    public virtual void End()
    {
        isActive = false;
    }
}
