using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : Poolable
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletDamage;
    private Rigidbody2D _rigid;
    private MeshRenderer _meshRenderer;

    public float BulletDamage => _bulletDamage;
    public float BulletSpeed {get => _bulletSpeed; set => _bulletSpeed = value;}

    protected virtual void Awake() {
        _rigid = GetComponent<Rigidbody2D>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetVelocity(Vector3 dir){
        _rigid.velocity = dir * _bulletSpeed;
    }

    public void SetBulletColor(Color color){
        MaterialManager.Instance.InstancingShader(_meshRenderer, color);
    }

    public void DestroyBullet(){
        PoolingParticle deadParticle = PoolManager.Instance.Pop("BulletDeleteParticle") as PoolingParticle;
        deadParticle.SetPosition(transform.position);
        deadParticle.Play(1f);

        float normalScale = 0.2f;
        float particleScale = deadParticle.transform.localScale.x;
        float targetScale = transform.localScale.x; 
        float scale = particleScale * targetScale / normalScale;

        deadParticle.transform.localScale = Vector3.one * scale;

        PoolManager.Instance.Push(this);
    }

    public override void Reset()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == 6){
            DestroyBullet();
        }
    }
}
