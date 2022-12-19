using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    [SerializeField] private List<WavePoint> point = new List<WavePoint>();

    public List<EnemyBase> enemys = new List<EnemyBase>();
    public EnemyData[] datas;

    int[,] waveEnemyDatas = new int[12,5]{
        {0,0,0,0,0},//wave1 cnt3
        {0,1,0,0,0},//wave2 cnt3
        {0,1,1,0,0},//wave3 cnt3
        {1,1,1,0,0},//wave4 cnt3
        {0,1,0,0,0},//wave6 cnt4
        {0,1,0,1,0},//wave7 cnt4
        {0,1,1,1,0},//wave8 cnt4
        {1,1,1,1,0},//wave9 cnt4
        {1,1,1,1,0},//wave11 cnt5
        {0,1,1,0,0},//wave12 cnt5
        {0,1,1,1,0},//wave13 cnt5
        {0,1,1,1,1},//wave14 cnt5
    };

    int waveIndex = 1;

    private float nextStageTime = 5f;

    private void Start() {
        StartCoroutine(Spawn());
    }

    [ContextMenu("ASd")]
    private void WaveSet(){
        print(waveEnemyDatas[waveIndex,0]);
        print(waveEnemyDatas[waveIndex,1]);
        print(waveEnemyDatas[waveIndex,2]);
        for(int i = 0; i < point[waveIndex].enemyCount; i++){
            print(waveEnemyDatas[waveIndex,i]);
        }
    }

    private void EnemySet(int waveIndex){
        for(int i = 0; i < point[waveIndex].enemyCount; i++){
            EnemyBase enemy = PoolManager.Instance.Pop("Enemy") as EnemyBase;
            enemy.enemy = datas[waveEnemyDatas[waveIndex,i]];
            enemys.Add(enemy);
            enemy.transform.position = point[waveIndex].wavePosition[i];
        }
    }

    /* public void SetEnemy(float scale, Vector2 pos,float angle){
        transform.position = pos;
        transform.localScale = transform.localScale * scale;
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, angle));
    } */

    IEnumerator Spawn(){
        while(true){
            print(EnemyCntCheck());
            if(EnemyCntCheck()){
                EnemySet(waveIndex);
                waveIndex++;
            }
            yield return new WaitForSeconds(nextStageTime);
        }
    }

    private bool EnemyCntCheck(){
        if(enemys.Count == 0){
            return true;
        }
        else{
            return false;
        }
    }
}