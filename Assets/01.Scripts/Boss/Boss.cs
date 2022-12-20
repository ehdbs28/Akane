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
    private Collider2D _collider2D;
    private WaitForSeconds _damageDelayTime;
    private Animator _animator;

    public Rigidbody2D Rigid => _rigid;
    public Collider2D Collider2D => _collider2D;
    public Animator Animator => _animator;

    public bool IsStun {get; set;}
    public bool IsDie {get; set;}
    public bool IsPhaseCutScene {get; set;}

    public bool IsPhase {get; set;} = false;

    private void Awake() {
        _currentHP = _maxHP;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _damageDelayTime = new WaitForSeconds(_damageDelay);
        _animator = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
    }

    public void OnDamage(float damage)
    {
        if(IsDie || IsPhaseCutScene) return;

        _currentHP -= damage;
        UIManager.Instance.SetBossHP(_currentHP);

        StartCoroutine(DamageCoroutine());

        PoolingParticle attackParticle = PoolManager.Instance.Pop("AttackParticle") as PoolingParticle;
        PoolingParticle bossBrokenParticle = PoolManager.Instance.Pop("BossBrokenEffect") as PoolingParticle;

        attackParticle.SetPosition(transform.position);
        bossBrokenParticle.SetPosition(transform.position);

        attackParticle.Play(); 
        bossBrokenParticle.Play();

        if(_currentHP <= _phase2HP){
            if(!IsPhase){
                StartCoroutine(BossPhase2(3f));
                return;
            }
        }

        if(_currentHP <= 0){
            OnDie();
        }
    }

    public void OnDie(){
        IsDie = true;
        StartCoroutine(ShowOutLine(1f, false));
        _rigid.velocity = Vector2.zero;
        _rigid.bodyType = RigidbodyType2D.Static;
        _animator.SetBool("IsDie", true);
    }

    private IEnumerator ShowOutLine(float duration, bool show){
        float currentTime = 0f;
        float minValue = (show) ? 0f : 3f;
        float maxValue = (show) ? 3f : 0f;

        while(currentTime <= duration){
            currentTime += Time.deltaTime;
            _spriteRenderer.material.SetFloat("_OutLineThickness", Mathf.Lerp(minValue, maxValue, currentTime / duration));
            yield return null;
        }
    }

    private IEnumerator BossPhase2(float cutSceneDuration){
        IsPhaseCutScene = true;
        IsPhase = true;

        _animator.SetBool("IsPhaseTurn", true);
        _animator.Play("BossIdle");

        TimeScaleManager.Instance.SetTimeScale(0.4f, 0.5f);
        yield return new WaitForSeconds(0.1f);
        CameraManager.Instance.CameraShake(2f, cutSceneDuration);

        PoolingParticle phase2Particle = PoolManager.Instance.Pop("BossPhase2Particle") as PoolingParticle;
        phase2Particle.SetPosition(new Vector2(_collider2D.bounds.center.x, _collider2D.bounds.min.y));
        phase2Particle.Play(4f);

        _rigid.velocity = Vector2.zero;
        _rigid.bodyType = RigidbodyType2D.Static;

        CameraManager.Instance.CameraZoom(5f, cutSceneDuration);
        yield return StartCoroutine(ShowOutLine(cutSceneDuration, true));

        _rigid.bodyType = RigidbodyType2D.Dynamic;
        IsPhaseCutScene = false;
        _animator.SetBool("IsPhaseTurn", false);
    }

    private IEnumerator DamageCoroutine(){
        _originMat = _spriteRenderer.material;

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
