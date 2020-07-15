using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnim : MonoBehaviour {

    protected Animator animator;
    protected NavMeshAgent navMeshAgent;

    float speedPercent;

    protected virtual void Start() {
        animator = gameObject.GetComponent<Animator>();
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    protected virtual void Update() {
        speedPercent = ((navMeshAgent.velocity.sqrMagnitude / (navMeshAgent.speed * navMeshAgent.speed)));
        animator.SetFloat("SpeedPercent", speedPercent, 0.1f, Time.deltaTime);
    }

    public virtual void Attack() {
        animator.SetTrigger("attack");
    }
}
