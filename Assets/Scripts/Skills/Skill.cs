using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillButton{
	A = 0,
	B = 1,
	X = 2,
	Y = 3 
}

public abstract class Skill : MonoBehaviour {

    public PlayerController playerController;
    public SkillButton eButton;
	public bool isActive = false;
	public float cooldown;

    protected bool inCooldown;

    public abstract void Init(PlayerController pc);
    public virtual void Execute()
    {
        if(inCooldown || isActive)
        {
            return;
        }

        isActive = true;
        inCooldown = true;

        // TODO update an UI.

        Invoke("CooldownFinished", cooldown);
    }

    private void CooldownFinished()
    {
        inCooldown = false;
    }

    public void End()
    {
        isActive = false;
    }
}
