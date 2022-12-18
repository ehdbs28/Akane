using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public bool IsStun {get; set;}

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Bullet")){
            BigBullet bigBullet = other.GetComponent<BigBullet>();
            
            if(bigBullet != null){
                if(bigBullet.Turn == BounceTurn.Boss){
                    bigBullet.Bounce();
                }
            }
            
        }
    }
}
