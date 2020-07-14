using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileCtrl : MonoBehaviour
{
    public float BulletSpeed = 200;
    public float Damage = 1;
    public float Lifetime = 10;
    public ParticleSystem PS;
    public Transform particleTransform;

    private Rigidbody myRb;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody>();
        myRb.velocity = transform.forward * BulletSpeed;
        Destroy(gameObject, Lifetime);
    }



    private void OnCollisionEnter(Collision collision)
    {
        Health h = collision.gameObject.GetComponent<Health>();
        if (h != null)
        {
            h.TakeDamage(Damage);
           
        }
        if(particleTransform != null)
        {
            particleTransform.parent = null;
            if (PS != null)
                PS.Play();
            Destroy(particleTransform.gameObject, 2);
        }
        Destroy(gameObject);
    }


}
