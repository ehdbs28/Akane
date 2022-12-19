using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    public bool isFar;

    public float checkRadius;
    public float damage;
    public float hp;
    public float speed;
}
