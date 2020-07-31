using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyMotor : MonoBehaviour{
    protected NavMeshAgent navMeshAgent;
    protected Animator animator;
    protected bool isCalm = true;
    private bool isShotDown = false;
    protected float distance;
    protected Transform target;

    private Health health;      //избавится от этого

    protected virtual void Start() {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();
        health = gameObject.GetComponent<Health>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(EnvironmentCheck());
    }

    protected IEnumerator EnvironmentCheck() {
        while (true) {
            isShotDown = health.tempCruth;
            distance = (target.position - transform.position).sqrMagnitude;

            if (!isShotDown)
                BehaviorChoices();
            else
                InjuredBehavior();
            yield return null;
        }
    }

    protected abstract void BehaviorChoices();

    protected abstract void InjuredBehavior();

    protected abstract void Turnover();

    protected abstract void Attack();

    public void DestroyMe() {
        Destroy(gameObject);
    }
}
