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

    public SkillButton eButton;

    protected PlayerController playerController;
    protected float cooldown = 0;
    protected bool isActive = false;
    protected bool isTransmitted = false;
    protected bool inCooldown = false;
    protected List<Skill> skillsToRemove;

    public abstract void Init(PlayerController pc);
    public virtual void Execute(List<Skill> _skillsToRemove)
    {
        if(inCooldown || isActive)
        {
            return;
        }

        isActive = true;
        inCooldown = true;

        skillsToRemove = _skillsToRemove;

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

    public virtual void HasBeenTransmitted()
    {
        isTransmitted = true;
        Destroy(this);
    }
}
