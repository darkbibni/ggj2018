using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Gun_Skill : Skill
{
    private Gun_Data data;
    private GameObject bullet;
    
    public override void Execute(List<Skill> _skillsToRemove)
    {
        if (isActive)
        {
            return;
        }

        base.Execute(_skillsToRemove);

        bullet = Instantiate(data.Gun_ProjectilePrefab);
        bullet.transform.position = transform.position;
        bullet.transform.eulerAngles = new Vector3(90, transform.eulerAngles.y, 0);
        
        bullet.GetComponent<Gun_Projectile>().skill = this;

        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * data.speed);
    }

    public override void Init(PlayerController pc){
        playerController = pc;
        data = SkillManager.instance.gun_data;
    }

    public void EnemyTouched(PlayerController enemy)
    {
        if (!isTransmitted)
        {
            isTransmitted = true;
            playerController.TransmitToEnemy(skillsToRemove, eButton, enemy);
            Destroy(this);
        }
    }

    public override void HasBeenTransmitted()
    {
        Destroy(bullet);
        Destroy(this);
    }
}
