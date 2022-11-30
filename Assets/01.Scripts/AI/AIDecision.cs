using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIDecision : MonoBehaviour
{
    protected AIBrain _brain;
    protected AIStateInfo _state;

    private void Awake() {
        _brain = transform.parent.parent.parent.GetComponent<AIBrain>();
        _state = _brain.transform.Find("AI").GetComponent<AIStateInfo>();
    }

    public abstract bool MakeADecision();
}
