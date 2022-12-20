using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SelectMenu : MonoBehaviour
{
   [SerializeField] private RectTransform _cursor;
   [SerializeField] private RectTransform[] _menus;

   protected int currentCursor = 0;

   protected void MoveCursor(){
        float inputY = Input.GetAxisRaw("Vertical");
        if(inputY > 0){ //up
            if(currentCursor - 1 >= 0)
                currentCursor--;
        }
        else if(inputY < 0){ //down
            if(currentCursor + 1 <= _menus.Length - 1)
                currentCursor++;
        }

        _cursor.anchoredPosition = new Vector2(_cursor.anchoredPosition.x, _menus[currentCursor].anchoredPosition.y);
   } 

   protected abstract void MenuSelect(int cursorValue);
}
