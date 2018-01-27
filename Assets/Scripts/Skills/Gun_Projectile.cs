using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Projectile : MonoBehaviour {

    public Gun_Skill skill;

    private PlayerInputManager caster;

    private void Awake()
    {
        caster = skill.gameObject.GetComponent<PlayerInputManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerInputManager otherPlayer = other.GetComponent<PlayerInputManager>();

            if (caster.playerId == otherPlayer.playerId)
            {
                //TODO Add skills, remove skills
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
