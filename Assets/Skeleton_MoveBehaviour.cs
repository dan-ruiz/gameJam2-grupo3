using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_MoveBehaviour : StateMachineBehaviour
{
    private SkeletonBossStats skeletonBoss;
    private Rigidbody2D rb2D;

    [SerializeField] private float speed;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        skeletonBoss = animator.GetComponent<SkeletonBossStats>();
        rb2D = skeletonBoss.rb2D;

        skeletonBoss.LookAtPlayer();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //rb2D.velocity = new Vector2(speed, rb2D.velocity.y) * animator.transform.right;

        Vector2 direction = (new Vector2(skeletonBoss.player.position.x, skeletonBoss.player.position.y) - rb2D.position).normalized;
        rb2D.velocity = direction * speed;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //rb2D.velocity = new Vector2(0, rb2D.velocity.y);    
        rb2D.velocity = Vector2.zero;
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
}
