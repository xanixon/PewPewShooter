using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        other.gameObject.GetComponent<Health>().TakeDamage(5f);
    }
}
