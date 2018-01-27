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
}
