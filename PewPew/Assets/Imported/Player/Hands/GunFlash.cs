using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFlash : MonoBehaviour
{
    [SerializeField] private List<GameObject> Flashes = new List<GameObject>();

    private Animation anim;

    private void Start()
    {
        anim = GetComponent<Animation>();
    }

    public void StartFlash(float stepTime)
    {
        anim.Play();
    }


   
}
