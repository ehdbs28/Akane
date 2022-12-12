using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIBrain : MonoBehaviour
{
    [SerializeField] private AIState _currentState;

    public int SkillWave = 0;

    private AIStateInfo _stateInfo;

    public Animator Animator;
    public Rigidbody2D Rigid;
    //private AgentMovement _agentMovement;

    //private Dictionary<Skill>

    private void Awake() {
        _stateInfo = transform.Find("AI").GetComponent<AIStateInfo>();
        Animator = GetComponent<Animator>();
        Rigid = GetComponent<Rigidbody2D>();
    }

    public void ChangeToState(AIState nextState){
        _currentState = nextState;
    }

    protected virtual void Update(){
        _currentState.UpdateState();
    }
}
