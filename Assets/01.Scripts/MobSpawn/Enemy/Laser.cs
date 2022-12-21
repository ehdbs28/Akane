using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    Material mat;
    private Collider2D _collider;

    private void Awake() {
        mat = GetComponent<SpriteRenderer>().material;
        _collider = GetComponent<Collider2D>();
    }

    private void Start() {
        _collider.enabled = false;
        StartCoroutine(Disappear());
    }

    private IEnumerator Disappear(){
        float _fade = 1.5f;
        while(true){
            _fade += 5;
            if(_fade >= 300){
                if(!_collider.enabled) _collider.enabled = true;
            }
            if(_fade>=1000){
                _fade = 1000;
                StopCoroutine(Disappear());

                Destroy(gameObject);
            }
            mat.SetFloat("_LaserThreadhold", _fade);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            other.GetComponent<PlayerHealth>().OnDamage(1);
        }
    }
}
