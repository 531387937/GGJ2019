﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Alertness : StateMachineBehaviour
{
    public Slime_ScriptableObject slime_data;
    Transform transform;

    private void OnEnable()
    {
        transform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CheckAttackRange(animator);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

    private void CheckAttackRange(Animator animator)
    {
        if(Vector3.Distance(transform.position,animator.transform.position) < slime_data.AttackRange)
        {
            animator.SetBool("InAttackRange", true);
        }
        else
        {
            animator.SetBool("InAttackRange", false);
        }
    }
}
