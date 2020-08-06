using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMedkit : MonoBehaviour{
    float contains = 5f;

    private void OnTriggerEnter(Collider other) {
        other.gameObject.GetComponent<Health>().TakeHeal(contains);
        Destroy(gameObject);
    }
}
