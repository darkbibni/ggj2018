using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Gun_Skill : Skill
{
    private Gun_Data data;
    private List<GameObject> bullets = new List<GameObject>();
    
    public override void Execute()
    {
        if (inCooldown || isActive)
        {
            return;
        }

        base.Execute();

        GameObject bullet = Instantiate(data.Gun_ProjectilePrefab);
        bullets.Add(bullet);
        bullet.transform.position = transform.position;
        bullet.transform.eulerAngles = new Vector3(90, transform.eulerAngles.y, 0);
        
        bullet.GetComponent<Gun_Projectile>().skill = this;

        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * data.speed);
    }

    public override void Init(PlayerController pc){
        data = SkillManager.instance.gun_data;
        cooldown = data.cooldown;
    }
}
