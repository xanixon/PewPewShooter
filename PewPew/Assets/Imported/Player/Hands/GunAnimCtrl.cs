using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimCtrl : MonoBehaviour
{
    private PlayerMotor motor;
    private Animator anim;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        motor = GetComponentInParent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = motor.overallSpeed / motor.WalkSpeed;
        anim.SetFloat("walkSpeed", speed);
    }
}
