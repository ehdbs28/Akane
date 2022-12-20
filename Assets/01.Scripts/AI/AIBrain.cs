using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIBrain : MonoBehaviour
{
    [SerializeField] private AIState _currentState;

    public int SkillWave = 0;

    private AIStateInfo _stateInfo;

    public Animator Animator => Boss?.Animator;
    public Rigidbody2D Rigid => Boss?.Rigid;
    public Collider2D Collider => Boss?.Collider2D;

    public Transform Player;
    public Boss Boss;
    //private AgentMovement _agentMovement;

    //private Dictionary<Skill>

    private void Awake() {
        _stateInfo = transform.Find("AI").GetComponent<AIStateInfo>();
        Player = GameObject.Find("Player").transform;
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
        if(Boss.IsPhaseCutScene || Boss.IsDie) return;
        
        _currentState.UpdateState();
    }
}
