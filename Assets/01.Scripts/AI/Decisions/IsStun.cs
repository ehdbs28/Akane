using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsStun : AIDecision
{
    public override bool MakeADecision()
    {
        return _brain.Boss.IsStun;
    }
}
