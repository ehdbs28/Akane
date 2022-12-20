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
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void Start() {
        enemy = gameObject.GetComponent<EnemyBase>().enemy;
        hp = enemy.hp;
    }

    public void OnDamage(float damage)
    {
        SoundManager.Instance.PlayOneShot(GameManager.Instance.PlayerSource, "MonsterHit");
        PoolingParticle attackParticle = PoolManager.Instance.Pop("AttackParticle") as PoolingParticle;
        attackParticle.SetPosition(transform.position);
        attackParticle.Play(); 
        
        StartCoroutine(ColorChanger()); 
        
        hp -= damage;
        if(hp <= 0){
            OnDie();
        }
    }

    public void OnDie(){
        EnemyBase enemyBase = GetComponent<EnemyBase>();
        enemyBase.IsDie = true;
        MobSpawner.Instance.enemys.Remove(enemyBase);

        enemyBase.DissolveOffFunc();
    }

    private IEnumerator ColorChanger(){
        
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = Color.white;
    }
}
