using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MonoBehaviour
{
    private Animator animator;

    private void Start() {
        animator = gameObject.transform.root.GetComponent<Animator>();   
    }

    private void OnTriggerEnter(Collider other) {
        string clipName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        if (clipName == "Attack(1)" || clipName == "Attack(2)" || clipName == "Attack(3)" )
            other.gameObject.GetComponent<Health>().TakeDamage(5f);
    }
}
