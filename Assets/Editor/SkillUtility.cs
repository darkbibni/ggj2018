using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SkillUtility : MonoBehaviour {

    [MenuItem("Assets/Create/ScriptableObjects/Gun_Data")]
    public static void CreateGunDataAsset()
    {
        ScriptableObjectUtility.CreateAsset<Gun_Data>("/Data", "Gun_Data");
    }

    [MenuItem("Assets/Create/ScriptableObjects/Slash_Data")]
    public static void CreateSlashDataAsset()
    {
        ScriptableObjectUtility.CreateAsset<Slash_Data>("/Data", "Slash_Data");
    }

    [MenuItem("Assets/Create/ScriptableObjects/Stomp_Data")]
    public static void CreateStompDataAsset()
    {
        ScriptableObjectUtility.CreateAsset<Stomp_Data>();
    }

    [MenuItem("Assets/Create/ScriptableObjects/Missile_Data")]
    public static void CreateMissileDataAsset()
    {
        ScriptableObjectUtility.CreateAsset<Missile_Data>();
    }

    [MenuItem("Assets/Create/ScriptableObjects/DashAlea_Data")]
    public static void CreateDashAleaDataAsset()
    {
        ScriptableObjectUtility.CreateAsset<DashAlea_Data>();
    }

    [MenuItem("Assets/Create/ScriptableObjects/Ray_Data")]
    public static void CreateRayDataAsset()
    {
        ScriptableObjectUtility.CreateAsset<Ray_Data>();
    }
}
