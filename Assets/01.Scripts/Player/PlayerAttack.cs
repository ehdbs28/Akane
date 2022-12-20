using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackDelay = 1f;

    private PlayerHealth _playerHealth;
    private WeaponRotation _weaponController;

    Animator anim;

    private LayerMask _targetLayer;

    private bool isAttack;
    public bool IsAttack
    {
        get => isAttack;
        set => isAttack = value;
    }

    public bool IsRotate { get; private set; }

    [SerializeField]
    private ParticleSystem slash;
    [SerializeField]
    private Transform weaponPos;
    [SerializeField]
    private Transform attackPos;
    [SerializeField] private float _playerDamage;

    public GameObject parti;

    public float angle = 0;
    public float speed = 2f;

    private float lerpTime = 0;

    Quaternion CalculateMovementOfPendulum()
    {
        return Quaternion.Lerp(Quaternion.Euler(Vector3.forward * angle),
            Quaternion.Euler(Vector3.back * angle), GetLerpTParam());
    }

    float GetLerpTParam()
    {
        return (Mathf.Sin(lerpTime) + 1) * 0.5f;
    }
    private void Awake()
    {
        _targetLayer = LayerMask.GetMask("Enemy");
        _playerHealth = FindObjectOfType<PlayerHealth>();
        _weaponController = transform.Find("AttackPos").GetComponent<WeaponRotation>();
        anim = GetComponent<Animator>();
        StartCoroutine(Attack());
        StartCoroutine(Dodge());
    }

    public void isDone(){
        anim.runtimeAnimatorController = null;
        GameObject obj = Instantiate(parti,transform.position, Quaternion.identity);
        obj.GetComponent<ParticleSystem>().Play();
    }

    IEnumerator Attack()
    {
        while (!_playerHealth.IsDie)
        {
            yield return new WaitUntil(() => Input.GetMouseButton(0));

            SoundManager.Instance.PlayOneShot(GameManager.Instance.PlayerSource, "PlayerAttack");

            slash.Play();
            _weaponController.WeaponAttack();
            _weaponController.IsWeaponSlash = true;

            isAttack = true;
            IsRotate = false;
            yield return new WaitForSeconds(0.1f);
            isAttack = false;
            _weaponController.IsWeaponSlash = false;

            Vector3 attackPos = transform.position + (Vector3)_weaponController.MouseInput.normalized;
            float rotation = Mathf.Atan2((attackPos - transform.position).y, (attackPos - transform.position).x) * Mathf.Rad2Deg;
            Collider2D[] hits = Physics2D.OverlapBoxAll(attackPos, new Vector3(5.5f, 2f), rotation, _targetLayer);
            if(hits.Length > 0){
                foreach(Collider2D hit in hits){
                    IDamageable damage = hit.GetComponent<IDamageable>();
                    if(damage != null){
                        CameraManager.Instance.CameraShake(7f, 0.1f);
                        UIManager.Instance.DodgeSliderValueSet(UIManager.Instance.DodgeSliderValue + 0.2f);
                        damage.OnDamage(_playerDamage);
                    }

                    BigBullet bigBullet = hit.GetComponent<BigBullet>();
                    if(bigBullet != null){
                        if(bigBullet.Turn == BounceTurn.Player){
                            bigBullet.Bounce();
                        }
                    }
                }
            }

            yield return new WaitForSeconds(attackDelay);
            IsRotate = true;
        }
    }

    private IEnumerator Dodge(){
        while(!_playerHealth.IsDie){
            yield return new WaitUntil(() => Input.GetMouseButtonDown(1));

            if(UIManager.Instance.DodgeSliderValue > 0){
                SoundManager.Instance.PlayOneShot(GameManager.Instance.PlayerSource, "Whoosh");

                UIManager.Instance.DodgeSliderValueSet(UIManager.Instance.DodgeSliderValue - 0.2f);

                slash.Play();
                _weaponController.WeaponAttack();
                _weaponController.IsWeaponSlash = true;

                IsRotate = false;

                yield return new WaitForSeconds(0.1f);

                _weaponController.IsWeaponSlash = false;

                Vector3 attackPos = transform.position + (Vector3)_weaponController.MouseInput.normalized;
                float rotation = Mathf.Atan2((attackPos - transform.position).y, (attackPos - transform.position).x) * Mathf.Rad2Deg;
                Collider2D[] hits = Physics2D.OverlapBoxAll(attackPos, new Vector3(5.5f, 2f), rotation, _targetLayer);
                if(hits.Length > 0){
                    foreach(Collider2D hit in hits){
                        BossBullet bullet = hit.GetComponent<BossBullet>();
                        bullet?.DestroyBullet();
                    }
                }

                yield return new WaitForSeconds(attackDelay);
                IsRotate = true;
            }
        }
    }
}
