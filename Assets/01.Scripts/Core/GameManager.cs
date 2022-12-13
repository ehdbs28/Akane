using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private List<Poolable> _poolList;

    private void Awake() {
        foreach(Poolable poolable in _poolList){
            PoolManager.Instance.CreatePool(poolable, transform);
        }
    }
}
