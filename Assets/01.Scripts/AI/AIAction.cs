using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : MonoBehaviour
{
    protected AIBrain _brain;
    public bool IsPlayAction = false;

    private void Awake() {
        _brain = transform.parent.parent.GetComponent<AIBrain>();
    }

    public abstract void TakeAction();
}
