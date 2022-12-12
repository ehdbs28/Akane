using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackDelay = 1f;

    private Animator _weaponAnim;
    private PlayerController _controller;

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
        _weaponAnim = GetComponent<Animator>();

        StartCoroutine(Attack());
    }

    private void Update()
    {
        if (isAttack)
        {
            lerpTime += Time.deltaTime * speed;
            weaponPos.rotation = CalculateMovementOfPendulum();

            weaponPos.position = Vector3.LerpUnclamped(weaponPos.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), 1);
        }
        else
        {
            weaponPos.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, transform.rotation.z), Quaternion.Euler(0, 0, 135), 0.1f);
        }
    }

    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.GetMouseButton(0));
            slash.Play();
            isAttack = true;
            IsRotate = false;
            yield return new WaitForSeconds(0.1f);
            isAttack = false;
            RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(3, 5), attackPos.GetComponent<WeaponRotation>().angle,transform.forward,LayerMask.GetMask("Enemy"));
            if(hit)
            {
                //때릴고야?
                //나 때릴고야?
                //응 미안.
            }

            yield return new WaitForSeconds(attackDelay);
            IsRotate = true;
        }
    }
}
