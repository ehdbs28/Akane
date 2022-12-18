using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackDelay = 1f;

    private PlayerController _controller;
    private WeaponRotation _weaponController;

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
        _controller = FindObjectOfType<PlayerController>();
        _weaponController = transform.Find("AttackPos").GetComponent<WeaponRotation>();

        StartCoroutine(Attack());
    }

    private void Update()
    {
        //if (isAttack)
        //{
            // lerpTime += Time.deltaTime * speed;
            // weaponPos.rotation = CalculateMovementOfPendulum();

            // weaponPos.position = Vector3.LerpUnclamped(weaponPos.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), 1);
        //}
        // else
        // {
        //     weaponPos.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, transform.rotation.z), Quaternion.Euler(0, 0, 135), 0.1f);
        // }
    }

    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.GetMouseButton(0));
            slash.Play();
            _weaponController.WeaponAttack();
            _weaponController.IsWeaponSlash = true;

            isAttack = true;
            IsRotate = false;
            yield return new WaitForSeconds(0.1f);
            isAttack = false;
            _weaponController.IsWeaponSlash = false;

            RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(3, 5), attackPos.GetComponent<WeaponRotation>().angle,transform.forward,LayerMask.GetMask("Enemy"));
            if(hit)
            {
                Debug.Log("hit");
            }

            yield return new WaitForSeconds(attackDelay);
            IsRotate = true;
        }
    }
}
