using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WavePoint")]
public class WavePoint : ScriptableObject
{
    public Vector3[] wavePosition;

    public int enemyCount;
}
