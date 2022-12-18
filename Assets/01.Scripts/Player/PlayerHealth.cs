using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxHP;
    private float _currentHP;

    [SerializeField] private float _damageDelay;

    [SerializeField] private Material _originMat;
    [SerializeField] private Material _whiteFlashMat;

    private SpriteRenderer _spriteRenderer;
    private WaitForSeconds _damageDelayTime;

    public bool IsDie {get; set;}

    private void Awake() {
        _spriteRenderer = transform.Find("PlayerSprite").GetComponent<SpriteRenderer>();
        _currentHP = _maxHP;
    }

    public void OnDamage(float damage)
    {
        PoolingParticle attackParticle = PoolManager.Instance.Pop("AttackParticle") as PoolingParticle;
        attackParticle.SetPosition(transform.position);
        attackParticle.Play();

        _currentHP -= damage;
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
        Debug.Log("죽음");
    }
}
