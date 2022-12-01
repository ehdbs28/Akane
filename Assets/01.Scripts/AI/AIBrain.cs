using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIBrain : MonoBehaviour
{
    [SerializeField] private AIState _currentState;
    private AIStateInfo _stateInfo;
    //private AgentMovement _agentMovement;

    //private Dictionary<Skill>

    private void Awake() {
        _stateInfo = transform.Find("AI").GetComponent<AIStateInfo>();
    }

    public void ChangeToState(AIState nextState){
        _currentState = nextState;
    }

    protected virtual void Update(){
        _currentState.UpdateState();
    }
}
