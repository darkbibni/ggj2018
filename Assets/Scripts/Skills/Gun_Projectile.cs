using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Projectile : MonoBehaviour {

    [HideInInspector]
    public Gun_Skill skill;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerController caster = skill.gameObject.GetComponent<PlayerController>();
            PlayerController otherPlayer = other.GetComponent<PlayerController>();
            
            if (caster.playerId != otherPlayer.playerId)
            {
                otherPlayer.AddSkill(skill);
                caster.RemoveSkill(skill, skill.eButton);
                Destroy(gameObject);
                Destroy(skill);
            }
        }
        else
        {
            skill.End();
            Destroy(gameObject);
        }
    }
}
