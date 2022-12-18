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
    public Collider2D Collider;

    public Transform Player;
    public Boss Boss;
    //private AgentMovement _agentMovement;

    //private Dictionary<Skill>

    private void Awake() {
        _stateInfo = transform.Find("AI").GetComponent<AIStateInfo>();
        Player = GameObject.Find("Player").transform;
        Animator = GetComponent<Animator>();
        Rigid = GetComponent<Rigidbody2D>();
        Collider = GetComponent<Collider2D>();
        Boss = GetComponent<Boss>();
    }

    private void Start() {
        ChangeToState(_currentState);
    }

    public void ChangeToState(AIState nextState){
        _currentState = nextState;
        ActionReset();
    }

    private void ActionReset(){
        AIAction currentAction = _currentState.GetComponent<AIAction>();
        currentAction?.Reset();
    }

    protected virtual void Update(){
        if(Boss.IsDie) return;
        
        _currentState.UpdateState();
    }
}
