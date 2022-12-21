using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using DG.Tweening;

public class MobSpawner : MonoBehaviour
{
    public static MobSpawner Instance;

    public GameObject laser;

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

    private void EnemySet(int waveIndex)
    {
        try{
            SoundManager.Instance.PlayOneShot(GameManager.Instance.PlayerSource, "MonsterSpawn");
            for (int i = 0; i < point[waveIndex].enemyCount; i++)
            {
                EnemyBase enemy = PoolManager.Instance.Pop("Enemy") as EnemyBase;
                enemy.enemy = datas[waveEnemyDatas[waveIndex, i]];
                enemy.GetComponentInChildren<EnemyAnimationChooser>().Chooser();
                enemys.Add(enemy);
                enemy.transform.position = point[waveIndex].wavePosition[i];
            }
        }
        catch(ArgumentOutOfRangeException){
            PlayerAttack.IsClear();
            Invoke("SceneTransToMain", 0.5f);
        }
    }

    private void SceneTransToMain() => SceneTransManager.Instance.SceneChange("Main");

    /* public void SetEnemy(float scale, Vector2 pos,float angle){
        transform.position = pos;
        transform
        catch(ArgumentOutOfRangeException){
            SceneTransManager.Instance.SceneChange("Main");
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
            if (!_specialPattern)
            {
                if(EnemyCntCheck()){
                    EnemySet(waveIndex);
                waveIndex++;
                }
            }
            if(realWaveIndex%5==0){
                _specialPattern = true;
            }

            if(_specialPattern){
                StartCoroutine(ShootBulletPattern());
            }
            yield return new WaitUntil(()=>!_specialPattern);
            yield return new WaitUntil(()=>EnemyCntCheck());
            realWaveIndex++;
        }
    }

    IEnumerator ShootBulletPattern(){
        int shootCnt = 0;
        while(shootCnt!=4){
            SoundManager.Instance.PlayOneShot(GameManager.Instance.PlayerSource, "Laser");
            GameObject obj = Instantiate(laser);
            float rand = UnityEngine.Random.Range(0, 360);

            obj.transform.position = shootPos[shootCnt].position;
            obj.transform.rotation = Quaternion.Euler(0,0,rand);
            yield return new WaitForSeconds(1f);
            shootCnt++;
        }
        realWaveIndex++;

        _specialPattern = false;
    }

    private bool EnemyCntCheck()
    {
        if (enemys.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}