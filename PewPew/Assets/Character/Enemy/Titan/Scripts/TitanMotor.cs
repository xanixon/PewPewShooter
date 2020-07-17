using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TitanMotor: EnemyMotor {
    private float noticedRadius = 40f;      //
    private float agrRadius = 20f;          //
    private float attackRadius = 3f;        //
    private float turnSpeed = 2f;           //
    private int numPrevention = 3;          //
    private int numberAttacs = 3;           //
    private TitanAnim titanAnim;


    protected override void Start() {
        base.Start();
        navMeshAgent.stoppingDistance = attackRadius;
        titanAnim = gameObject.GetComponent<TitanAnim>();
        PowDistance();  //Возводим растояния в квадрат, так будет проще потом сравнивать
    }

    private void PowDistance() {
        noticedRadius *= noticedRadius;
        agrRadius *= agrRadius;
    }

    // Я так понял это не стоит делат в Апдейте, но не придумал ничего лучше, да и вообще, не уверен, что стоит разделять управление анимацией и действием на 2 скрипта.
    protected override void BehaviorChoices() {
        distance = (target.position - transform.position).sqrMagnitude;

        if (distance <= noticedRadius) {
            if (isCalm)
                Anxiety();

            if (distance <= agrRadius || (numPrevention <= 0 && !isCalm)) {
                navMeshAgent.SetDestination(target.position);
            }

            if (distance <= navMeshAgent.stoppingDistance * navMeshAgent.stoppingDistance) {
                Turnover();
                Attack();
            }
        }

        else if (distance >= noticedRadius && !isCalm) 
            BecomeCalm();
    }

    protected override void InjuredBehavior() {
        distance = (target.position - transform.position).sqrMagnitude;
        navMeshAgent.SetDestination(target.position);

        if (distance <= navMeshAgent.stoppingDistance * navMeshAgent.stoppingDistance) {
            Turnover();
            Attack();
        }
    }

    protected override void Attack() {
        titanAnim.Attack(numberAttacs);
        //+ что-нибудь ещё
    }

    private void BecomeCalm() {
        isCalm = true;
        titanAnim.BecomeCalm();

        navMeshAgent.SetDestination(gameObject.transform.position);
    }

    private void Anxiety() {            //волнение ебаки, когда она нас заметила
        isCalm = false;
        animator.SetBool("isCalm", isCalm);
        StartCoroutine(PreventRoar());
    }

    private IEnumerator PreventRoar() {      //рычит, даёт шанс свалить.
        while (numPrevention > 0 && distance <= noticedRadius) {
            animator.SetInteger("numPreventionRoar", numPrevention - 1);
            yield return new WaitForSeconds(3f);
            numPrevention--;
        }
        yield break;
    }

    protected override void Turnover() {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
}
