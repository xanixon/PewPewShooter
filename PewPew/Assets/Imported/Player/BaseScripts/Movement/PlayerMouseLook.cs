using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMouseLook : MonoBehaviour
{

    [SerializeField] private float XSensivity=1, YSensivity = 1;
    [SerializeField] private bool InvertX = false, InvertY = false;



    private float xDirection, yDirection;
 
    // Start is called before the first frame update
    void Start()
    {
        if (InvertX)
            xDirection = 1;
        else
            xDirection = -1;
        if (InvertY)
            yDirection = -1;
        else
            yDirection = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float x, y;

        x = Input.GetAxis("Mouse X") * yDirection * XSensivity;
        y = Input.GetAxis("Mouse Y") * xDirection * YSensivity;
        // if (x > 75) x = 75;
        // if (x < -75) x = -75;
        Debug.Log(y);
        x = ClampAngle(x, -75, 75);

        y = ClampAngle(y, -360, 360);
        transform.localRotation *= Quaternion.AngleAxis(y, Vector3.left) * Quaternion.AngleAxis(x, Vector3.up);

       

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
