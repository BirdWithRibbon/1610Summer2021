using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Vector3Data : ScriptableObject
{
    public Vector3 spawnPos;
    public void NewSpawnPos(Vector3 n)
    {
        spawnPos = n;
    }
    public void OffsetSpawn(Vector3 n)
    {
        spawnPos += n;
    }

}