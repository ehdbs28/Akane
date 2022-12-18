using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : Poolable
{
    [SerializeField] private float _speed = 5f;

    public override void Reset()
    {
        Debug.Log($"{this.name} : excute method Reset");
    }

    private void Shoot(){
        //transform.position += transform.up * _speed;
    }
}
