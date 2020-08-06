using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanAnim : EnemyAnim{
    protected override void Start() {
        base.Start();
        animator.SetInteger("numPreventionRoar", 3); //
    }

    public void Attack(int num) {
        animator.SetInteger("attack", Random.Range(1, num + 1));
    }

    public void StopAttack() {
        animator.SetInteger("attack", 0);
    }

    public void BecomeCalm() {
        animator.SetBool("isCalm", true);
        animator.SetTrigger("LastRoar");
        StopAttack();
    }
}
