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

    private PlayerController _plaeyerController;

    public bool IsDie {get; set;}

    #region UI
    [SerializeField] private Transform _playerCanvas;
    private Vector2 _hpOriginPos = new Vector2(0.7f, 0.7f);
    private float _posDownValue = 0.4f;
    private GameObject _hpUI;

    private Stack<Image> _hps = new Stack<Image>();
    #endregion

    private void Awake() {
        _spriteRenderer = transform.Find("PlayerSprite").GetComponent<SpriteRenderer>();
        _currentHP = _maxHP;
        _damageDelayTime = new WaitForSeconds(_damageDelay);
        _hpUI = Resources.Load<GameObject>("Prefabs/UI/PlayerHPUI");
        _plaeyerController = GetComponent<PlayerController>();
    }

    private void Start() {
        for(int i = 0; i < _maxHP; i++){
            GameObject uiObj = GameObject.Instantiate(_hpUI);
            uiObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(_hpOriginPos.x, _hpOriginPos.y - (i * _posDownValue));
            uiObj.transform.SetParent(_playerCanvas);

            Image image = uiObj.GetComponent<Image>();
            _hps.Push(image);
            image.DOFade(0, 0.3f);
        }
    }

    public void OnDamage(float damage)
    {   
        if(IsDie) return;
        
        StartCoroutine(DestoryHP());

        StartCoroutine(DamageCoroutine());

        PoolingParticle attackParticle = PoolManager.Instance.Pop("AttackParticle") as PoolingParticle;
        attackParticle.SetPosition(transform.position);
        attackParticle.Play();

        _currentHP -= damage;
        if(_currentHP <= 0){
            OnDie();
        }
    }

    private IEnumerator DestoryHP(){
        foreach(Image img in _hps){
            img.DOFade(1f, 0.3f);
        }

        Image image = _hps.Pop();
        image.GetComponent<Animator>()?.Play("DestroyHP");

        yield return new WaitForSeconds(0.5f);

        Destroy(image.gameObject);

        foreach(Image img in _hps){
            img.DOFade(0f, 0.3f);
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

        _plaeyerController.Animator.SetBool("IsDie", true);
        Invoke("CallBack", 0.1f);
    }

    private void CallBack(){
        _plaeyerController.Animator.SetBool("IsDie", false);

        _plaeyerController.enabled = false;
    }
}
