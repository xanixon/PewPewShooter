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

    private Health health;      //избавится от этого

    public Transform target;

    protected virtual void Start() {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();
        health = gameObject.GetComponent<Health>();
    }

    protected void Update() {
        isShotDown = health.tempCruth;
        if (!isShotDown)
            BehaviorChoices();
        else
            InjuredBehavior();
    }

    protected abstract void BehaviorChoices();

    protected abstract void InjuredBehavior();

    protected abstract void Turnover();

    protected abstract void Attack();

    public void DestroyMe() {
        Destroy(gameObject);
    }
}
