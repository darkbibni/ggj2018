using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Projectile : MonoBehaviour {

    public Gun_Skill skill;

    private void Awake()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerController caster = skill.gameObject.GetComponent<PlayerController>();
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
