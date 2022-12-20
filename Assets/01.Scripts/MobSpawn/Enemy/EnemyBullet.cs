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
            Destroy(gameObject);
        }
    }
}
