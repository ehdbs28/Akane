using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsActionEnd : AIDecision
{
    [SerializeField] private float _actionTime = 3f;
    private float _currentTime = 0f;

    public override bool MakeADecision()
    {
        _currentTime += Time.deltaTime;

        return _currentTime >= _actionTime;
    }
}
