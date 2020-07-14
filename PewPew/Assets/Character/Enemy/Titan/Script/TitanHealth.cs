using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanHealth : Health
{
    protected override void Die() {
        gameObject.GetComponent<Animator>().SetTrigger("Death");
    }
}
