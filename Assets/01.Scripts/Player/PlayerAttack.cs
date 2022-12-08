using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackDelay = 1f;

    private Animator _weaponAnim;
    private PlayerController _controller;

    [SerializeField]
    private ParticleSystem slash;
    [SerializeField]
    private Transform weaponPos;
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
    }

    private void Update()
    {
        lerpTime += Time.deltaTime * speed;
        weaponPos.rotation = CalculateMovementOfPendulum();

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        slash.Play();

        yield return new WaitForSeconds(attackDelay);
    }
}
