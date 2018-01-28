using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Projectile : MonoBehaviour {

    [HideInInspector]
    public Gun_Skill skill;

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
                    skill.EnemyTouched(otherPlayer, transform.up);
                    isActive = false;
                    Destroy(gameObject);
                }
            }

            else if(!(other.tag == "Ground" || other.tag =="Exit"))
            {
                skill.End();
                isActive = false;
                Destroy(gameObject);
            }
        }
    }
}
