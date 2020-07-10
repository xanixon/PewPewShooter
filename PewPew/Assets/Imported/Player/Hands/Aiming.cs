using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    public bool ADS = false;
    private PlayerMotor motor;
    private Animator anim;
    private Camera mainCamera;
    [SerializeField] private float FOVMultiplier = 1.5f; //изменяет угол обзора камеры при прицеливании
    [Range(0,1)]
    [SerializeField] private float FOVSpeed = 0.3f;

    private float oldCameraAngle;
    private bool isFOVChaning = false; //запущены ли сейчас корутины меняющие FOV
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        motor = GetComponentInParent<PlayerMotor>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        oldCameraAngle = mainCamera.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            ADS = true;

            motor.isADS = ADS;
            anim.SetBool("aiming", ADS);
            StartCoroutine(SetCameraToADS(FOVSpeed));
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            ADS= false;

            motor.isADS = ADS;
            anim.SetBool("aiming", ADS);
            StartCoroutine(SetCameraToNormal(FOVSpeed));
        }
    }

    IEnumerator SetCameraToADS(float duration)
    {
        if (isFOVChaning) yield break;
        isFOVChaning = true;

        float elapsed = 0;
        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, oldCameraAngle / FOVMultiplier, elapsed/duration);
            yield return null;
        }
        isFOVChaning = false;
    }

    IEnumerator SetCameraToNormal(float duration)
    {
        while (isFOVChaning)
        {
            yield return null;
        }
        isFOVChaning = true;
        
        float elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, oldCameraAngle , elapsed / duration);
            yield return null;
        }
        isFOVChaning = false;
    }
}
