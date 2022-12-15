using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsActionEnd : AIDecision
{
    [SerializeField] private float _actionTime = 3f;
    [SerializeField] private AIAction _thisAction;
    [SerializeField] private bool _isItSkill = false;

    private float _currentTime = 0f;

    public override bool MakeADecision()
    {
        _currentTime += Time.deltaTime;

        if(_currentTime >= _actionTime){
            if(_thisAction.IsPlayAction is true) _thisAction.IsPlayAction = false;
            _currentTime = 0f;
            if(_isItSkill) _brain.SkillWave++;
            return true;
        }
        else{
            return false;
        }
    }
}
