using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MobSpawner : MonoBehaviour
{
    public static MobSpawner Instance;

    public GameObject bullet;

    [SerializeField] private List<WavePoint> point = new List<WavePoint>();

    public List<EnemyBase> enemys = new List<EnemyBase>();
    public EnemyData[] datas;

    public Transform[] shootPos;

    int[,] waveEnemyDatas = new int[12, 5]{
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

    int waveIndex = 0;
    int realWaveIndex = 1;
    bool _specialPattern = false;
    private float nextStageTime = 5f;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        StartCoroutine(Spawn());
    }

    [ContextMenu("ASd")]
    private void WaveSet()
    {
        print(waveEnemyDatas[waveIndex, 0]);
        print(waveEnemyDatas[waveIndex, 1]);
        print(waveEnemyDatas[waveIndex, 2]);
        for (int i = 0; i < point[waveIndex].enemyCount; i++)
        {
            print(waveEnemyDatas[waveIndex, i]);
        }
    }

    private void EnemySet(int waveIndex)
    {
        for (int i = 0; i < point[waveIndex].enemyCount; i++)
        {
            EnemyBase enemy = PoolManager.Instance.Pop("Enemy") as EnemyBase;
            enemy.enemy = datas[waveEnemyDatas[waveIndex, i]];
            enemys.Add(enemy);
            enemy.transform.position = point[waveIndex].wavePosition[i];
        }
    }

    /* public void SetEnemy(float scale, Vector2 pos,float angle){
        transform.position = pos;
        transform.localScale = transform.localScale * scale;
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, angle));
    } */

    IEnumerator Spawn()
    {
        while (true)
        {
            if(realWaveIndex%5==0){
                _specialPattern = true;
            }

            if (EnemyCntCheck() && !_specialPattern)
            {
                EnemySet(waveIndex);
                waveIndex++;
            }

            if(_specialPattern){
                //총알 패턴 실시.

                StartCoroutine(ShootBulletPattern());
            }
            yield return new WaitUntil(()=>!_specialPattern);
            yield return new WaitForSeconds(nextStageTime);
        }
    }

    IEnumerator ShootBulletPattern(){
        int shootCnt = 0;
        while(shootCnt!=8){
            if(shootCnt < 8){
                for(int i = 0; i < 3; i++){
                    GameObject obj = Instantiate(bullet);
                    obj.transform.position = shootPos[shootCnt].position;
                    float angle = Mathf.Atan2(0-obj.transform.position.y,0-obj.transform.position.x) * Mathf.Rad2Deg-90;
                    obj.transform.rotation = Quaternion.Euler(0,0,angle);
                }
                shootCnt++;
            }
            if(shootCnt==4){
                _specialPattern = false;
            }
            yield return new WaitForSeconds(2f);
        }
        realWaveIndex++;
    }

    private bool EnemyCntCheck()
    {
        if (enemys.Count == 0)
        {
            realWaveIndex++;
            return true;
        }
        else
        {
            return false;
        }
    }
}