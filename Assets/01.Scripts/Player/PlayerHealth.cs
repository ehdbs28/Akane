using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxHP;
    private float _currentHP;

    [SerializeField] private float _damageDelay;

    [SerializeField] private Material _originMat;
    [SerializeField] private Material _whiteFlashMat;

    private SpriteRenderer _spriteRenderer;
    private WaitForSeconds _damageDelayTime;

    private PlayerController _playerController;

    public bool IsDie {get; set;}

    private void Awake() {
        _spriteRenderer = transform.Find("PlayerSprite").GetComponent<SpriteRenderer>();
        _currentHP = _maxHP;
        _damageDelayTime = new WaitForSeconds(_damageDelay);
        _playerController = GetComponent<PlayerController>();
    }

    private void Start() {
        UIManager.Instance.SetPlayerHP(_currentHP);
    }

    public void OnDamage(float damage)
    {   
        if(IsDie) return;
        
        _currentHP -= damage;
        UIManager.Instance.SetPlayerHP(_currentHP);
        SoundManager.Instance.PlayOneShot(GameManager.Instance.PlayerSource, "PlayerOuch");
        CameraManager.Instance.CameraShake(5f, 0.1f);

        StartCoroutine(DamageCoroutine());

        PoolingParticle attackParticle = PoolManager.Instance.Pop("AttackParticle") as PoolingParticle;
        attackParticle.SetPosition(transform.position);
        attackParticle.Play();

        if(_currentHP <= 0){
            OnDie();
        }
    }

    private IEnumerator DamageCoroutine(){
        _spriteRenderer.material = _whiteFlashMat;
        yield return _damageDelayTime;
        _spriteRenderer.material = _originMat;
    }

    public void OnDie()
    {
        IsDie = true;

        _playerController.Animator.SetBool("IsDie", true);
    }
}
