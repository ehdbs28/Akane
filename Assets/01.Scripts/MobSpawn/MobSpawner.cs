using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPos = new List<Transform>();
    [SerializeField] private GameObject Enemy;
    [SerializeField] private Tilemap tiles;


    private float nextStageTime = 5f;

    private void Start() {
        StartCoroutine(Spawn());
    }

    [ContextMenu("맵 셋")]
    private void MapSet(){
        for(int i = 0; i < tiles.)
    }

    [ContextMenu("적 셋")]
    private void EnemySet(){
        for(int i = 0; i < 5; i++){
            EnemyBase enemy = PoolManager.Instance.Pop("Enemy") as EnemyBase;
        }
    }

    public void SetEnemy(float scale, Vector2 pos,float angle){
        transform.position = pos;
        transform.localScale = transform.localScale * scale;
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, angle));
    }

    IEnumerator Spawn(){
        while(true){
            yield return new WaitForSeconds(nextStageTime);
        }
    }
}