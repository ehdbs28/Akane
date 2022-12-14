using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : Poolable
{
    [SerializeField] private float _bulletSpeed;
    private Rigidbody2D _rigid;

    private void Awake() {
        _rigid = GetComponent<Rigidbody2D>();
    }

    public void SetVelocity(Vector3 dir){
        _rigid.velocity = dir * _bulletSpeed;
    }

    public override void Reset()
    {
        
    }
}
