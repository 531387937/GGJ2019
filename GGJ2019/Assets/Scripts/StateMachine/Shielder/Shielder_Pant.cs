﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shielder_Pant : StateMachineBehaviour
{
    public Shielder_ScriptableObject shielder_data;
    Transform transform;
    float time = 0;
    private void OnEnable()
    {
        transform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time = 0;
        if (transform.position.x - animator.transform.position.x < 0)
        {
            animator.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        else
        {
            animator.transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CheckPlayer(animator);
        time += Time.deltaTime;
        if(time>1)
        {
            animator.SetTrigger("after1s");
        }
    }

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

    private void CheckPlayer(Animator animator)
    {
        if (Vector3.Distance(animator.transform.position, transform.position) < shielder_data.ViewRange)
            animator.SetBool("InView", true);
        else
            animator.SetBool("InView", false);
    }
  
}
