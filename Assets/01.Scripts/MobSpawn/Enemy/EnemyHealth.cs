using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float hp;
    [SerializeField] private EnemyData enemy;

    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start() {
        hp = enemy.hp;
    }

    public void OnDamage(float damage)
    {
        PoolingParticle attackParticle = PoolManager.Instance.Pop("AttackParticle") as PoolingParticle;
        PoolingParticle bossBrokenParticle = PoolManager.Instance.Pop("BossBrokenEffect") as PoolingParticle;

        StartCoroutine(ColorChanger());

        attackParticle.SetPosition(transform.position);
        bossBrokenParticle.SetPosition(transform.position);

        attackParticle.Play(); bossBrokenParticle.Play();
        
        hp -= damage;
        if(hp <= 0){
            OnDie();
        }
    }

    public void OnDie(){
        EnemyBase enemyBase = GetComponent<EnemyBase>();
        enemyBase.IsDie = false;
    }

    private IEnumerator ColorChanger(){
        
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = Color.white;
    }
}
