using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    //Макс. скорость базовая скорость
    public float WalkSpeed=2;     

    public float overallSpeed;

    public float Acceleration = 10; //ускорение
    public float StrafeSpeedMultiplier = 0.8f; //множитель скорости для ходьбы влево-впрово (клавиши A, D)
    public float ADSSpeedMultiplier = 0.1f;
    public float SprintSpeedMultiplier = 2;
    public float footstepInterval = 1;

    public float playerJumpHeight = 1; //высота прыжка игрока в метрах

    public bool isADS = false;
    public bool isSprint = false;

    //временная херня! по хорошему нужно поместить это 
    //в отедльный объект и выбирать нужный лист с звуками шагов исходя их поверхности под ногами
    [SerializeField] private List<AudioClip> footstepSounds = new List<AudioClip>(); 

    private float footstepAcc = 0; 
    private float CurrentForwardSpeed = 0;
    private float CurrentStrafeSpeed = 0;
    private float jumpInitialSpeed; //рассчетная скорость вначале прыжка
    private Rigidbody myRb;
    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody>();
        jumpInitialSpeed = Mathf.Sqrt(2*playerJumpHeight * Physics.gravity.magnitude);

    }

    // Update is called once per frame
    void Update()
    {
        MovementCtrl();

      
        JumpCtrl();
    }
    void MovementCtrl()
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) //если пользователь что-то нажал из WASD
        {
            //управление звуками шагов
            footstepAcc += Time.deltaTime;
            if(isSprint)
            {
                if (footstepAcc > footstepInterval/SprintSpeedMultiplier)
                {
                    if (footstepSounds.Count > 0)
                        AudioSource.PlayClipAtPoint(footstepSounds[Random.Range(0, footstepSounds.Count)], transform.position - transform.up * 0.3f);

                    footstepAcc = 0;

                }
            }
            else
            {
                if (footstepAcc > footstepInterval)
                {
                    if (footstepSounds.Count > 0)
                        AudioSource.PlayClipAtPoint(footstepSounds[Random.Range(0, footstepSounds.Count)], transform.position - transform.up * 0.3f);

                    footstepAcc = 0;

                }
            }
            

            Vector3 forwardSpeed = transform.forward * CurrentForwardSpeed * Input.GetAxis("Vertical");

            if (Input.GetAxis("Vertical") != 0)
                CurrentForwardSpeed += Acceleration * Time.deltaTime; //увеличиваем соответсвующую скорость
            else //если юзер не трогал WS - тормозим его по forward
            {
                CurrentForwardSpeed -= Acceleration * Time.deltaTime;
                CurrentForwardSpeed = Mathf.Clamp(CurrentForwardSpeed, 0, WalkSpeed * SprintSpeedMultiplier);
            }

            if(!isADS) //в режиме прицеливания свои ограничения скорости
                 if (Input.GetKey(KeyCode.LeftShift))
                 {
                     CurrentForwardSpeed = Mathf.Clamp(CurrentForwardSpeed, 0, WalkSpeed* SprintSpeedMultiplier);
                 }
                 else
                 {
                     CurrentForwardSpeed = Mathf.Clamp(CurrentForwardSpeed, 0, WalkSpeed);
                 }
            else
            {
                CurrentForwardSpeed = Mathf.Clamp(CurrentForwardSpeed, 0, WalkSpeed*ADSSpeedMultiplier);
            }
    

            
            Vector3 strafeSpeed = transform.right * CurrentStrafeSpeed * Input.GetAxis("Horizontal");

            if(Input.GetAxis("Horizontal") != 0)
                CurrentStrafeSpeed += Acceleration * Time.deltaTime; //увеличиваем соответсвующую скорость
            else//если юзер не трогал AD - тормозим его по strafe
            {

                CurrentStrafeSpeed -= Acceleration * Time.deltaTime;
                CurrentStrafeSpeed = Mathf.Clamp(CurrentStrafeSpeed, 0, WalkSpeed * SprintSpeedMultiplier * StrafeSpeedMultiplier);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                isSprint = true;
                CurrentStrafeSpeed = Mathf.Clamp(CurrentStrafeSpeed, 0, WalkSpeed * StrafeSpeedMultiplier * SprintSpeedMultiplier);
            }
            else
            {
                isSprint = false;
                CurrentStrafeSpeed = Mathf.Clamp(CurrentStrafeSpeed, 0, WalkSpeed * StrafeSpeedMultiplier);
            }

            Vector3 verticalSpeed = new Vector3(0, myRb.velocity.y, 0); //скорость прыжка/падения
                myRb.velocity = forwardSpeed + strafeSpeed + verticalSpeed; 
        }
        else //если юзер не трогает WASD - Останавливаем его (убирать этот кусок нельзя!)
        {
            CurrentStrafeSpeed -= Acceleration * Time.deltaTime;
            CurrentStrafeSpeed = Mathf.Clamp(CurrentStrafeSpeed, 0, WalkSpeed * SprintSpeedMultiplier);

            CurrentForwardSpeed -= Acceleration * Time.deltaTime;
            CurrentForwardSpeed = Mathf.Clamp(CurrentForwardSpeed, 0, WalkSpeed * SprintSpeedMultiplier);

        }
        overallSpeed = myRb.velocity.magnitude;
 
    }

    void JumpCtrl()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(Mathf.Abs(myRb.velocity.y) <= 0.001f)
            {
                myRb.velocity += transform.up * jumpInitialSpeed;
            }
          
        }
    }

}




