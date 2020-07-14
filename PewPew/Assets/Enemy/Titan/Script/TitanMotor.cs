using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TitanMotor: MonoBehaviour
{
    private float noticedRadius = 40f;
    private float agrRadius = 20f;
    private float attackRadius = 3f;
    private float turnSpeed = 2f;
    private int numPrevention = 3;
    public bool isCalm = true;

    private float distance;


    public Transform target;
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    void Start(){
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        navMeshAgent.stoppingDistance = attackRadius;
        animator = gameObject.GetComponent<Animator>();
    }

    // Я так понял это не стоит делат в Апдейте, но не придумал ничего лучше.
    void Update() {
        distance = Vector3.Distance(target.position, transform.position);

        if (distance <= noticedRadius) {
            if (isCalm)
                Anxiety();

            if (distance <= agrRadius || (numPrevention <= 0 && !isCalm)) {
                navMeshAgent.SetDestination(target.position);

                if (distance <= navMeshAgent.stoppingDistance) {
                    Turnover();
                }
            }
        }

        if (distance >= noticedRadius && !isCalm) {
            isCalm = true;
            animator.SetBool("isCalm", isCalm);
            animator.SetTrigger("LastRoar");
            navMeshAgent.SetDestination(gameObject.transform.position);
        }
    }

    private void Anxiety() {
        StartCoroutine(PreventRoar());
        isCalm = false;
        animator.SetBool("isCalm", isCalm);
    }

    private IEnumerator PreventRoar() {
        while (numPrevention > 0 && distance <= noticedRadius) {
            animator.SetInteger("numPreventionRoar", numPrevention - 1);
            yield return new WaitForSeconds(3f);
            numPrevention--;
        }
        yield break;
    }

    private void Turnover() {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, agrRadius);
    }
}
