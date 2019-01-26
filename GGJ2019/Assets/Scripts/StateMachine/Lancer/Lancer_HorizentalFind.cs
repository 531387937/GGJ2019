using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lancer_HorizentalFind : StateMachineBehaviour
{
    public Lancer_ScriptableObject lancer_data;
    Transform transform;

    private void OnEnable()
    {
        transform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Check(animator);
        Approach(animator);
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

    private void Check(Animator animator)
    {
        if((transform.position.y > animator.transform.position.y) && (Mathf.Abs(transform.position.x - animator.transform.position.x) < lancer_data.CompareRange))
        {
            animator.SetBool("Upward", true);
        }
        else
        {
            animator.SetBool("Upward", false);
        }

        if(Mathf.Abs(transform.position.x - animator.transform.position.x) < lancer_data.AttackRange)
        {
            animator.SetBool("Forward",true);
        }
        else
        {
            animator.SetBool("Forward", false);
        }
    }

    private void Approach(Animator animator)
    {

    }
}
