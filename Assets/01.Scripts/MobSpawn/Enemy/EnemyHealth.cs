using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public float hp;
    public EnemyData enemy;

    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start() {
        enemy = gameObject.GetComponent<EnemyBase>().enemy;
        hp = enemy.hp;
    }

    public void OnDamage(float damage)
    {
        //PoolingParticle attackParticle = PoolManager.Instance.Pop("AttackParticle") as PoolingParticle;
        //PoolingParticle bossBrokenParticle = PoolManager.Instance.Pop("BossBrokenEffect") as PoolingParticle;

        StartCoroutine(ColorChanger());

        //attackParticle.SetPosition(transform.position);
        //bossBrokenParticle.SetPosition(transform.position);

        //attackParticle.Play(); bossBrokenParticle.Play();
        
        hp -= damage;
        if(hp <= 0){
            OnDie();
        }
    }

    public void OnDie(){
        EnemyBase enemyBase = GetComponent<EnemyBase>();
        MobSpawner.Instance.enemys.Remove(enemyBase);
        PoolManager.Instance.Push(enemyBase);
        enemyBase.IsDie = true;
    }

    private IEnumerator ColorChanger(){
        
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = Color.white;
    }
}
