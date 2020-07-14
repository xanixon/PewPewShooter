using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseY : MonoBehaviour
{
    [SerializeField] private float YSensivity = 1;
    [SerializeField] private bool InvertY = false;
    [SerializeField] private float MinAngle=-75 ,MaxAngle=75; //ограничение в повороте головы, чтобы не вертеть головой вверх-вниз на 360 градусов
    [Range(1,30)]
    //[SerializeField] private float Smoothness = 20; //плавность камеры 

    private Quaternion oldQuaternion;
    private float yDirection;
    private float yRotation = 0;
    private Transform tr;
    private float avgValue = 0;
    private Quaternion startRot;
    private float currentRecoil=0; //влияние отдачи на положение камеры. Управляется через RecoilCtrl при помощи методов Add/Set Recoil
    // Start is called before the first frame update
    void Start()
    {

        tr = transform;
        startRot = tr.localRotation;
        if (InvertY)
            yDirection = -1;
        else
            yDirection = 1;

       
    }

    // Update is called once per frame
    void Update()
    {
        avgValue = 0;

        float newY = Input.GetAxis("Mouse Y") * YSensivity * yDirection;
        if (newY > 0 && yRotation < MaxAngle)
            yRotation += newY;
        if(newY < 0 && yRotation > MinAngle)
            yRotation += newY;

        avgValue = yRotation;
        

        avgValue = ClampAngle(avgValue + currentRecoil, MinAngle, MaxAngle);
        currentRecoil = Mathf.Lerp(currentRecoil, 0, 0.1f);
        Quaternion difference = startRot * Quaternion.AngleAxis(avgValue, Vector3.left) * Quaternion.Inverse(oldQuaternion);

        tr.localRotation *= difference;


        oldQuaternion = tr.localRotation;

    }

    /// <summary>
    /// Устанавливает значение CurrentRecoil
    /// </summary>
    /// <param name="angle"></param>
    public void SetCurrentRecoil(float angle)
    {
        currentRecoil = angle;
    }

    /// <summary>
    /// Добавляет к текущей отдаче значение angle
    /// </summary>
    /// <param name="angle"></param>
    public void AddToCurrentRecoil(float angle)
    {
        currentRecoil += angle;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        angle = angle % 360;
        if ((angle >= -360F) && (angle <= 360F))
        {
            if (angle < -360F)
            {
                angle += 360F;
            }
            if (angle > 360F)
            {
                angle -= 360F;
            }
        }
        return Mathf.Clamp(angle, min, max);
    }
}
