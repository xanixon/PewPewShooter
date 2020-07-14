using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class TitanAudio : MonoBehaviour
{
    public AudioClip stepSound;
    public AudioClip preventRoar;
    public AudioClip lastRoar;
    private AudioSource audioSource;

    private void Start() {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnStep() {
        audioSource.PlayOneShot(stepSound);
    }

    private void PreventRoar() {
        audioSource.PlayOneShot(preventRoar);
    }

    private void LastRoar() {
        audioSource.PlayOneShot(lastRoar);
    }
}
