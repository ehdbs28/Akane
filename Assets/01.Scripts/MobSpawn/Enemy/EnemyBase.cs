using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyBase : Poolable
{
    public EnemyData enemy;
    [SerializeField] Transform target;
    [SerializeField] LayerMask layer;

    [SerializeField] private GameObject bulletPrefabs;

    private bool isDie;
    public bool IsDie { get => isDie; set => isDie = value; }

    public override void Reset()
    {
        Debug.Log($"{this.name} : excute method Reset");
        StartCoroutine(AttackRoutine());
    }

    private void Update()
    {
        EnemyMove();
    }

    private bool AttackCheck()
    {
        if (enemy.isFar)
        {
            if (target != null && Vector2.Distance(transform.position, target.position) <= 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (target != null && Vector2.Distance(transform.position, target.position) <= 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private void FixedUpdate()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, enemy.checkRadius, layer);
        print(hit.name);
        if (hit != null && !AttackCheck())
        {
            target = hit.transform;
        }
    }

    private void EnemyMove()
    {
        if (target != null && !AttackCheck() && !isDie)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, enemy.speed * Time.deltaTime);
        }
    }

    private void Attack()
    {
        if (!IsDie)
        {
            if (enemy.isFar)
            {
                float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg + -90;
                print(angle);
                GameObject bullet = Instantiate(bulletPrefabs, transform.position, Quaternion.Euler(0, 0, angle));
            }
            else
            {
                Collider2D hit = Physics2D.OverlapCircle(transform.position, 2, layer);
                hit.transform.GetComponent<PlayerController>().hp -= enemy.damage;
            }
        }
    }

    private IEnumerator AttackRoutine()
    {
        while (!IsDie)
        {
            if (AttackCheck())
            {
                yield return new WaitForSeconds(0.5f);
                Attack();

                yield return new WaitForSeconds(1f);
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
                continue;
            }
        }
    }
}
