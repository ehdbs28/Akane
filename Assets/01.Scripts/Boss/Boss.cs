using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxHP;
    [SerializeField] private float _phase2HP;
    private float _currentHP;

    [SerializeField] private float _damageDelay;

    [SerializeField] private Material _originMat;
    [SerializeField] private Material _whiteFlashMat;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigid;
    private WaitForSeconds _damageDelayTime;
    private Animator _animator;

    public bool IsStun {get; set;}
    public bool IsDie {get; set;}

    public bool IsPhase {get; set;} = false;

    private void Awake() {
        _currentHP = _maxHP;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _damageDelayTime = new WaitForSeconds(_damageDelay);
        _animator = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody2D>();
    }

    public void OnDamage(float damage)
    {
        StartCoroutine(DamageCoroutine());

        PoolingParticle attackParticle = PoolManager.Instance.Pop("AttackParticle") as PoolingParticle;
        PoolingParticle bossBrokenParticle = PoolManager.Instance.Pop("BossBrokenEffect") as PoolingParticle;

        attackParticle.SetPosition(transform.position);
        bossBrokenParticle.SetPosition(transform.position);

        attackParticle.Play(); bossBrokenParticle.Play();
        
        _currentHP -= damage;
        if(_currentHP <= _phase2HP){
            IsPhase = true;
        }
        if(_currentHP <= 0){
            OnDie();
        }
    }

    public void OnDie(){
        IsDie = true;
        _rigid.velocity = Vector2.zero;
        _animator.SetBool("IsDie", true);
        Invoke("AnimLoopLimit", 0.1f);
    }

    private void AnimLoopLimit(){
        _animator.SetBool("IsDie", false);
    }

    private IEnumerator DamageCoroutine(){
        _spriteRenderer.material = _whiteFlashMat;
        yield return _damageDelayTime;
        _spriteRenderer.material = _originMat;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Bullet")){
            BigBullet bigBullet = other.GetComponent<BigBullet>();
            
            if(bigBullet != null){
                if(bigBullet.Turn == BounceTurn.Boss){
                    bigBullet.Bounce();
                }
            }
        }
    }
}
