using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour {

    public static SkillManager instance;


    public Gun_Data gun_data;

    public Slash_Data slash_data;
    public Stomp_Data stomp_data;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }
}
