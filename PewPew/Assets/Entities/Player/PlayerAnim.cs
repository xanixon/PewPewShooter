using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour {
    private Animator animator;
    [SerializeField]private GameObject endUI = null;

    private void Start() {
        animator = gameObject.GetComponent<Animator>();
    }

    public void Move(float forvardSpeed, float strafeSpeed) {
        if (forvardSpeed < 0) {
            animator.SetBool("isRuningBack", true);
        }

        else {
            animator.SetBool("isRuningBack", false);
            animator.SetFloat("SpeedX", forvardSpeed);
        }
    }

    public void Shoot() {
        animator.SetTrigger("Fire");
    }

    public void Reload() {
        animator.SetTrigger("Reload");
    }

    private void EndUI() {
        endUI.SetActive(true);
    }
}
