using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhatIsNextSkill : AIDecision
{
    [SerializeField] private int _thisSkillCount;
    
    public override bool MakeADecision()
    {
        return _thisSkillCount == _brain.SkillWave % 4;
    }
}
