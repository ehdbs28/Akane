using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsStun : AIDecision
{
    public override bool MakeADecision()
    {
        if(_brain.Boss.IsPhaseCutScene) return true;
        return _brain.Boss.IsStun;
    }
}
