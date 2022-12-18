using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    [SerializeField] private List<WavePoint> point = new List<WavePoint>();


    private float nextStageTime = 5f;

    private void Start() {
        StartCoroutine(Spawn());
    }

    [ContextMenu("셋")]
    private void Set(){
        EnemySet(0,3);
    }

    private void EnemySet(int waveIndex,int index){
        for(int i = 0; i < index; i++){
            EnemyBase enemy = PoolManager.Instance.Pop("Enemy") as EnemyBase;
            enemy.transform.position = point[waveIndex].wavePosition[i];
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