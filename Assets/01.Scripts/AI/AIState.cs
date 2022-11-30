using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
    public List<AIAction> actions = null;
    public List<AITransition> transitions = null;

    private AIBrain _brain;

    private void Awake() {
        _brain = transform.parent.parent.GetComponent<AIBrain>();
    }

    public void UpdateState(){
        foreach(AIAction a in actions){
            a.TakeAction();
        }

        foreach(AITransition t in transitions){
            bool result = false;

            foreach(AIDecision d in t.decisions){
                result = d.MakeADecision();
                if(!result) break;
            }

            if(result){
                if(t.positiveResult != null){
                    _brain.ChangeToState(t.positiveResult);
                }
            }
            else{
                if(t.negativeResult != null){
                    _brain.ChangeToState(t.negativeResult);
                }
            }
        }
    }
}
