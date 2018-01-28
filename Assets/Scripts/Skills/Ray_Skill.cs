using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray_Skill : Skill {

    private Ray_Data data;
    GameObject instanceRay;

    private bool canBeInterrupted;

    public override void Init(PlayerController pc)
    {
        caster = pc;
        data = SkillManager.instance.ray_data;
        eButton = data.eButton;
    }

    public override void Execute(List<Skill> _skillsToRemove)
    {
        if (isActive)
        {
            return;
        }

        base.Execute(_skillsToRemove);

        canBeInterrupted = true;

        caster.SkillMove.canMove = false;

        RaycastHit hit = new RaycastHit();
        Debug.Log("Test");
        if (Physics.Raycast(transform.position, transform.forward, out hit, data.layerMask.value))
        {
            instanceRay = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            instanceRay.GetComponent<Collider>().isTrigger = true;
            instanceRay.AddComponent<Ray_Projectile>().skill = this;

            Vector3 scale = instanceRay.transform.localScale * Vector3.Distance(transform.position, hit.point);
            scale.x = 0.8f;
            scale.y *= 0.5f;
            scale.z = 0.8f;

            instanceRay.transform.localScale = scale;
            instanceRay.transform.eulerAngles = new Vector3(90, transform.eulerAngles.y, 0);
            instanceRay.transform.position = Vector3.Lerp(transform.position, hit.point, 0.5f);

            instanceRay.GetComponent<Renderer>().material = new Material(instanceRay.GetComponent<Renderer>().material);
            instanceRay.GetComponent<Renderer>().sharedMaterial.color = data.rayColorCharging;

            Invoke("Attack", data.chargeDuration);
        }
    }

    private void Update()
    {
        if (canBeInterrupted)
        {
            if (caster.SkillMove.IsStun())
            {
                caster.SkillMove.canMove = true;
                CancelInvoke("Attack");
                Destroy(instanceRay);
                End();
            }
        }
    }

    private void Attack()
    {
        canBeInterrupted = false;
        instanceRay.GetComponent<Renderer>().sharedMaterial.color = data.rayColorAttacking;

        Invoke("AttackEnded", data.rayDuration);
    }

    private void AttackEnded()
    {
        Destroy(instanceRay);
        caster.SkillMove.canMove = true;
        End();
    }

    public void EnemyTouched(PlayerController enemy)
    {
        if (!isTransmitted)
        {
            caster.SkillMove.canMove = true;
            isTransmitted = true;
            caster.TransmitToEnemy(skillsToRemove, eButton, enemy);
            Destroy(this);
        }
    }

    public override void HasBeenTransmitted()
    {
        base.HasBeenTransmitted();
        Destroy(instanceRay);
    }
}
