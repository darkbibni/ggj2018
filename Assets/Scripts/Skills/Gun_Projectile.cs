using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Projectile : MonoBehaviour {

    public Gun_Skill skill;

    private PlayerController caster;

    private void Awake()
    {
        caster = skill.gameObject.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerController otherPlayer = other.GetComponent<PlayerController>();

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
