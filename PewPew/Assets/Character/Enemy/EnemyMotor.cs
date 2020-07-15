using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyMotor : MonoBehaviour{
    protected NavMeshAgent navMeshAgent;
    protected Animator animator;
    protected bool isCalm = true;
    protected bool isDead = false;
    protected float distance;

    public Transform target;

    protected virtual void Start() {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();
    }

    protected void Update() {
        BehaviorChoices();
    }

    protected abstract void BehaviorChoices();

    protected abstract void Turnover();

    protected abstract void Attack();

    public void DestroyMe() {
        Destroy(gameObject);
    }
}
