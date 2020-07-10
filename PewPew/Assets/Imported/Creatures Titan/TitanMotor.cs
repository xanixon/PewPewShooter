using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TitanMotor : MonoBehaviour {

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private float distance;
    public Transform target;
    [HideInInspector]public int delayAttack = 1;

    private void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        StartCoroutine(TestExample());
    }

    private IEnumerator TestExample() {
        while (true) {
            distance = Vector3.Distance(target.position, transform.position);
            if (distance > 10)
                yield return StartCoroutine(Idle());

            else if (distance < 10 && distance > 3f) {
                if (animator.GetBool("isCalm") && !animator.GetBool("isWalk"))
                   yield return StartCoroutine(MakeEngry());
                yield return StartCoroutine(Walk());
            }

            else if (distance <= 3f)
                yield return StartCoroutine(Attack()); 
            yield return null;
        }
    }

    private IEnumerator Idle() {
        navMeshAgent.enabled = false;
        animator.SetBool("isWalk", false);
        animator.Play("Idle");
        animator.SetBool("isCalm", true);
        animator.SetInteger("attack", 0);
        yield break;
    }

    private IEnumerator MakeEngry() {
        animator.SetBool("isCalm", false);
        yield return new WaitForSeconds(0.1f);
    }

    private IEnumerator Walk() {
        navMeshAgent.enabled = true;
        navMeshAgent.SetDestination(target.position);
        animator.SetBool("isWalk", true);
        animator.SetFloat("attack", 0);
        yield break;
    }

    private IEnumerator Attack() {
        int i = Random.Range(1, 4);
        navMeshAgent.enabled = false;
        animator.SetBool("isWalk", false);
        animator.SetInteger("attack", i);
        yield return new WaitForSeconds(i * delayAttack);
    }
}
