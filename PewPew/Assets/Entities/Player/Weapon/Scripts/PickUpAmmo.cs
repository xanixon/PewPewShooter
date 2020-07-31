using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAmmo : MonoBehaviour{
    int contains = 12 * 2;
    string type; //для разных оружий на будующее

    private void OnTriggerEnter(Collider other) {
        other.gameObject.GetComponent<PlayerMotor>().TakeAmmo(contains);
        Destroy(gameObject);
    }
}
