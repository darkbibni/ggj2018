using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Gun_Skill : Skill
{
    private Gun_Data data;

    private void Awake()
    {
        data = SkillManager.instance.gun_data;
    }

    public override void Execute()
    {
        base.Execute();

        GameObject bullet = Instantiate(data.Gun_ProjectilePrefab, transform.position, transform.rotation);
        bullet.GetComponent<Gun_Projectile>().skill = this;

        bullet.GetComponent<Rigidbody>().velocity = transform.forward * data.speed;
    }

    public override void Init(){
        
    }
}
