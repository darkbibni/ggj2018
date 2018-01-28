using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray_Projectile : MonoBehaviour {

    [HideInInspector]
    public Ray_Skill skill;

    public bool isActive = true;

    private void OnTriggerEnter(Collider other)
    {
        if (isActive)
        {
            if (other.tag == "Player")
            {
                PlayerController caster = skill.gameObject.GetComponent<PlayerController>();
                PlayerController otherPlayer = other.GetComponent<PlayerController>();

                if (caster.playerId != otherPlayer.playerId)
                {
                    skill.EnemyTouched(otherPlayer);
                    isActive = false;
                    Destroy(gameObject);
                }
            }
        }
    }
}
