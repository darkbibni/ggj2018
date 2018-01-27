using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gun_Data : ScriptableObject
{
    public string objectName = "New MyScriptableObject";
    public bool colorIsRandom = false;
    public Color thisColor = Color.white;
    public Vector3[] spawnPoints;
}
