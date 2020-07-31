using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastWeaponCtrl : MonoBehaviour {
    private float damage = 1f;
    private float range = 100f;
    private WeaponAmmunition ammo;  //боезапас

    [SerializeField] private Transform cam = null;
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private AudioClip shootSound = null;
    private PlayerAnim anim;

    private RaycastHit hit;

    private void Start() {
        anim = gameObject.transform.root.GetComponent<PlayerAnim>();
        ammo = new WeaponAmmunition(12, 5);
        StartCoroutine(WeaponUpdate());
    }

    private IEnumerator WeaponUpdate() {
        while (true) {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                if (!ammo.isEmpty()) {
                    Shoot();
                }

            if (Input.GetKeyDown(KeyCode.R)) {
                Reload();
                yield return new WaitForSeconds(3f);
            }
            yield return null;
        }
    }

    protected virtual void Shoot() {
        audioSource.PlayOneShot(shootSound);
        anim.Shoot();
        ammo.SpendAmmo();
        if (Physics.Raycast(cam.position, cam.forward, out hit, range)) {
            if (!hit.collider.isTrigger) {
                hit.collider.gameObject.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }

    private void Reload() {
        anim.Reload();
        ammo.Reload();
    }

    public void TakeAmmo(int totalAmmo) {
        ammo.TakeAmmo(totalAmmo);
    }
}
