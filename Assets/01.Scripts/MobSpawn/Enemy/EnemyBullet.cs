using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    void Update()
    {
        transform.position += transform.up * 5 * Time.deltaTime;
   }

   private void FixedUpdate() {
    if(transform.position.x>10 || transform.position.x<-10||transform.position.y>10||transform.position.y<-10){
        Destroy(gameObject);
    }
   }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            other.GetComponent<PlayerHealth>().OnDamage(1);
            other.GetComponent<PlayerController>().Slow();

            PoolingParticle deadParticle = PoolManager.Instance.Pop("BulletDeleteParticle") as PoolingParticle;
            deadParticle.SetPosition(transform.position);
            deadParticle.Play(1f);

            float normalScale = 0.5f;
            float particleScale = deadParticle.transform.localScale.x;
            float targetScale = transform.localScale.x; 
            float scale = particleScale * targetScale / normalScale;

            deadParticle.transform.localScale = Vector3.one * scale;

            Destroy(gameObject);
        }
    }
}
