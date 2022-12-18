using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    #region UI
    [SerializeField] private Transform _playerCanvas;
    private Vector2 _hpOriginPos = new Vector2(0.7f, 0.7f);
    private float _posDownValue = 0.4f;
    private GameObject _hpUI;
    #endregion

    private void Awake() {
        _spriteRenderer = transform.Find("PlayerSprite").GetComponent<SpriteRenderer>();
        _currentHP = _maxHP;
        _damageDelayTime = new WaitForSeconds(_damageDelay);
        _hpUI = Resources.Load<GameObject>("Prefabs/UI/PlayerHPUI");
    }

    private void Start() {
        for(int i = 0; i < _maxHP; i++){
            GameObject uiObj = GameObject.Instantiate(_hpUI);
            uiObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(_hpOriginPos.x, _hpOriginPos.y - (i * _posDownValue));
            uiObj.transform.SetParent(_playerCanvas);
        }
    }

    public void OnDamage(float damage)
    {
        StartCoroutine(DamageCoroutine());

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
