using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Вешается на родительский объект с коллайдером, позволяет поворачиваться только влево-вправо
/// </summary>
public class PlayerMouseLookX : MonoBehaviour
{
    [SerializeField] private float XSensivity = 1;
    [SerializeField] private bool InvertX = false;

    private float xDirection, yDirection;
    private Rigidbody myRB;
    private Transform tr;
    private float currentRecoil = 0;
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();

        tr = transform;

        if (InvertX)
            xDirection = -1;
        else
            xDirection = 1;
    }

    // Update is called once per frame
    void Update()
    {
        currentRecoil = Mathf.Lerp(currentRecoil, 0, 0.1f);
        myRB.MoveRotation(Quaternion.Euler(tr.eulerAngles.x, tr.eulerAngles.y + Input.GetAxis("Mouse X") * XSensivity * xDirection + currentRecoil, tr.eulerAngles.z));


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
}
