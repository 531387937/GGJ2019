using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shielder_Attack : StateMachineBehaviour
{
    public Shielder_ScriptableObject shielder_data;
    Transform transform;
    Ray2D ray;
    RaycastHit2D raycastHit;
    float deltaTime;

    private void OnEnable()
    {
        transform = GameObject.FindGameObjectWithTag("Player").transform;
        ray = new Ray2D();
        ray.direction = new Vector2(1, -1);
        deltaTime = 0;
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("ThroughPlayer", false);
        deltaTime = 0;
        animator.gameObject.GetComponentInChildren<ShieldHitBox>().enabled = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CheckThroughPlayer(animator);
        deltaTime += Time.deltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponentInChildren<ShieldHitBox>().enabled = false;
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

    private void CheckThroughPlayer(Animator animator)
    {
        if(Vector3.Distance(animator.transform.position, transform.position) >= shielder_data.SlowDownDistance)
        {
            animator.SetBool("ThroughPlayer", true);
        }
    }

    private void CheckEdge(Animator animator)
    {
        ray.origin = animator.transform.position;
        raycastHit = Physics2D.Raycast(ray.origin, ray.direction,50);
        if(raycastHit.collider == null)
        {
            animator.SetBool("CloseToEdge", true);
        }
        else
        {
            animator.SetBool("CloseToEdge", false);
        }
    }

    private void CheckTime(Animator animator)
    {
        if(deltaTime > shielder_data.DashTime)
        {
            animator.SetBool("RunOutTime", true);
        }
        else
        {
            animator.SetBool("RunOutTime", false);
        }
    }
}
