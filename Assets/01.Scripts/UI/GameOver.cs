using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : SelectMenu
{
    private void Update() {
        MoveCursor();
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)){
            MenuSelect(currentCursor);
        }    
    }

    protected override void MenuSelect(int cursorValue)
    {
        switch(cursorValue){
            case 0:
                //현재 씬 다시 로드 추가
                Debug.Log("Restart");
                break;
            case 1:
                Debug.Log("Game Exit");
                Application.Quit();
                break;
        }
    }
}
