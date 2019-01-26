﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueGhost_Fly : StateMachineBehaviour
{
    public Blue_ScriptableObject blue_data;
    Transform transform;
    float time;
    float sign;

    private void OnEnable()
    {
        transform = GameObject.FindGameObjectWithTag("Player").transform;
        time = 0;
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        sign = Mathf.Sign((transform.position - animator.transform.position).x);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time += Time.deltaTime;
        animator.transform.position += blue_data.Speed * Time.deltaTime * Vector3.right * sign;
        animator.transform.position += blue_data.Amplitude * Mathf.Sin(time * blue_data.Cycle) * Vector3.up;
        CheckDir(animator);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time = 0;
    }

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

    private void CheckDir(Animator animator)
    {
        if (Vector3.Distance(animator.transform.position, transform.position) >= blue_data.ViewRange)
            sign = -sign;
    }
}
