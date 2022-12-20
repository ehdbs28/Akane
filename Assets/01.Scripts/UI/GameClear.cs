using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClear : MonoBehaviour
{
    private void Update() {
        if(Input.anyKeyDown){
            Debug.Log("Game Exit");
            Application.Quit();
        }
    }
}
