using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnim : MonoBehaviour {

    protected Animator animator;
    protected NavMeshAgent navMeshAgent;

    float speedPercent;

    protected virtual void Start() {
        animator = gameObject.GetComponent<Animator>();
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        animator.SetInteger("numPreventionRoar", 3);
    }

    protected virtual void Update() {
        speedPercent = ((navMeshAgent.velocity.sqrMagnitude / (navMeshAgent.speed * navMeshAgent.speed)));
        animator.SetFloat("SpeedPercent", speedPercent, 0.1f, Time.deltaTime);
    }

    protected virtual void Attack(int num) {
        animator.SetInteger("attack", Random.Range(1, num + 1));
    }

    protected void StopAttack() {
        animator.SetInteger("attack", 0);
    }
}
