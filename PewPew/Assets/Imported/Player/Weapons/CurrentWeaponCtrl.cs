using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Управление стрельбой их выбранного оружия.
/// TODO: отдачу нужно переделать
/// </summary>
public class CurrentWeaponCtrl : MonoBehaviour
{
    //временно статы оружия задаются через инспектор
    //отдача
    [SerializeField] private float RateOfFire = 0.5f; //темп стрельбы
    //[SerializeField] private float Damage =1;
    [SerializeField] private float RecoilDuration = 0.08f; //длительность отдачи 
    [SerializeField] private float VerticalRecoil = 0.1f; //сила вертикальной отдачи
    [SerializeField] private float HorizontalRecoil = 0.02f; //сила горизонтальной отдачи
    [SerializeField] private float ADSRecoilMultiplier=0.3f; //множитель силы отдачи в режиме прицеливания
    [Range(0, 5f)]
    [SerializeField] private float SerialBonusRecoil = 0.5f;   //усиления отдачи с каждым следующим выстрелом (аддитивно влияет на отдачу)

    //разброс
    [SerializeField] private float bulletSpread = 1; //разброс пуль в градусах

    //зависимости
    [SerializeField] private AudioClip ShotSound = null;
    //[SerializeField] private AudioClip ReloadSound = null;
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private Vector3 bulletSpawnOffset;
    [SerializeField] private GunFlash gunFlash = null;
    [SerializeField] private PlayerMotor motor = null;
    [SerializeField] private Aiming ADS_Ctrl = null;
    private Transform tr, parentTr;


    private float lastShotTime = 0;
    // Start is called before the first frame update
    void Start()
    {

        tr = transform.GetChild(0);
        parentTr = GetComponentInParent<Transform>();
        gunFlash = GetComponentInChildren<GunFlash>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            bool ableToShoot = false; //мы не можем стрелять, если мы бежим
            if(motor != null)
            {
                ableToShoot = !motor.isSprint || ADS_Ctrl.ADS;
            }
            else
            {
                ableToShoot = true; //если motor == null мы можем стрелять

            }
            if (ableToShoot && Time.time > lastShotTime + RateOfFire)
            {
                lastShotTime = Time.time;

                Quaternion spread = Quaternion.Euler(Random.Range(-bulletSpread, bulletSpread), Random.Range(-bulletSpread, bulletSpread), 0);
                Instantiate(bulletPrefab, tr.position + tr.right * bulletSpawnOffset.x + tr.up * bulletSpawnOffset.y + tr.forward * bulletSpawnOffset.z, parentTr.rotation*spread);
                AudioSource.PlayClipAtPoint(ShotSound, tr.position);

                if (RecoilCtrl.instance != null)
                {
                    if(ADS_Ctrl.ADS)  //в режиме прицеливания величина отдачи умножается на ADSMultiplier
                        RecoilCtrl.instance.StartRecoil(RecoilDuration, VerticalRecoil*ADSRecoilMultiplier,HorizontalRecoil * ADSRecoilMultiplier, SerialBonusRecoil);  
                    else
                        RecoilCtrl.instance.StartRecoil(RecoilDuration, VerticalRecoil, HorizontalRecoil, SerialBonusRecoil);
                }

                if (gunFlash)
                    gunFlash.StartFlash(0.02f);
            }
        }


      
    }


}
