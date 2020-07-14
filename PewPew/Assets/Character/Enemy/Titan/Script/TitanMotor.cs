using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TitanMotor: CharacterAnim
{
    private float noticedRadius = 40f;      //
    private float agrRadius = 20f;          //
    private float attackRadius = 3f;        //
    private float turnSpeed = 2f;           //
    private int numPrevention = 3;          //
    private int numberAttacs = 3;           //
    private bool isCalm = true;
    private bool isDead = false;

    private float distance;

    public Transform target;

    protected override void Start(){
        base.Start();
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        navMeshAgent.stoppingDistance = attackRadius;
        animator = gameObject.GetComponent<Animator>();
        PowDistance();  //Возводим растояния в квадрат, так будет проще потом сравинивать
    }

    private void PowDistance() {
        noticedRadius *= noticedRadius;
        agrRadius *= agrRadius;
    }

    // Я так понял это не стоит делат в Апдейте, но не придумал ничего лучше, да и вообще, наверно стоит разделить управление анимацией и движением на 2 скрипта.
    protected override void Update() {
        distance = (target.position - transform.position).sqrMagnitude;

        if (distance <= noticedRadius) {
            if (isCalm)
                Anxiety();

            if (distance <= agrRadius || (numPrevention <= 0 && !isCalm)) {
                navMeshAgent.SetDestination(target.position);
                base.Update();
            }

            if (distance <= navMeshAgent.stoppingDistance * navMeshAgent.stoppingDistance) {
                Turnover();
                Attack(numberAttacs);
            }
        }

        else if (distance >= noticedRadius && !isCalm) {
            isCalm = true;
            animator.SetBool("isCalm", isCalm);
            animator.SetTrigger("LastRoar");
            navMeshAgent.SetDestination(gameObject.transform.position);
            StopAttack();
        }
    }

    private void Anxiety() {
        isCalm = false;
        animator.SetBool("isCalm", isCalm);
        StartCoroutine(PreventRoar());
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

    public void Destr() {
        Destroy(gameObject);
    }
}
