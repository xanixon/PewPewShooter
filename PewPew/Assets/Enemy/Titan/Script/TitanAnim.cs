using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TitanAnim : MonoBehaviour {

    private Animator animator;
    private NavMeshAgent navMeshAgent;

    private void Start() {
        animator = gameObject.GetComponent<Animator>();
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        animator.SetInteger("numPreventionRoar", 3);
    }

    private void Update() {
        animator.SetFloat("SpeedPercent", navMeshAgent.velocity.magnitude/navMeshAgent.speed, 0.1f, Time.deltaTime);
    }

    private void Attack() {
        animator.SetInteger("attack", Random.Range(1, 4));
    }
}
