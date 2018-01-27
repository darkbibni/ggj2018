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
        
    }
}
