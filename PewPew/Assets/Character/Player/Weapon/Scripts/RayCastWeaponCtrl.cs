using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastWeaponCtrl : MonoBehaviour {
    private float damage = 1f;
    private float range = 100f;

    [SerializeField] private Transform cam = null;
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private AudioClip shootSound = null;

    private RaycastHit hit;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            Shoot();
    }

    private void Shoot() {
        audioSource.PlayOneShot(shootSound);
        if (Physics.Raycast(cam.position, cam.forward, out hit, range))
            hit.collider.gameObject.GetComponent<Health>().TakeDamage(damage);
    }
}
