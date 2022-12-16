using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : Poolable
{
    [SerializeField] private float _bulletSpeed;
    private Rigidbody2D _rigid;
    private MeshRenderer _meshRenderer;

    private void Awake() {
        _rigid = GetComponent<Rigidbody2D>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetVelocity(Vector3 dir){
        _rigid.velocity = dir * _bulletSpeed;
    }

    public void SetBulletColor(Color color){
        MaterialManager.Instance.InstancingShader(_meshRenderer, color);
    }

    public override void Reset()
    {
        
    }
}
