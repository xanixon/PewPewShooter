using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilCtrl : MonoBehaviour
{

    public static RecoilCtrl instance;
    private Transform tr;

    private PlayerMouseY mouseY; //отдача влияет на положение камеры, но беда в том, что mouseLook этому предпятсвует. Поэтому RecoilCtrl должен сообщать
    private PlayerMouseLookX mouseX; //mouseLookX, mouseLookY что надо сдвинуть камеру

    private int recoilCount = 0; //количество активных эффектов отдачи (используется для усиления отдачи со временем стрельбы)
    // Start is called before the first frame update
    void Start()
    {
        tr = transform;
        if(instance != null)
        {
            Debug.LogError("There more than one RecoilCtrl!");
           
        }
        instance = this;
        mouseY = GetComponent<PlayerMouseY>();
        mouseX = GetComponentInParent<PlayerMouseLookX>();
    }



    public void StartRecoil(float duration, float verticalPower, float horizontalRecoil, float serialBonusPower)
    {
        StartCoroutine(startRecoil(duration, verticalPower, horizontalRecoil, serialBonusPower));
    }

    IEnumerator startRecoil(float duration, float verticalPower, float horizontalRecoil, float serialBonusPower)
    {
        float elapsed = 0;

        float horizontalPower = horizontalRecoil * Random.Range(-1,2); //горизонтальная отдача имееть случайную направленность 
        recoilCount++;
        while (elapsed < duration )
        {
            elapsed += Time.deltaTime;
            
            mouseY.AddToCurrentRecoil((verticalPower + serialBonusPower * recoilCount) * Random.Range(0.1f,1f)); //каждый следующий выстрел вызывает все большую отдачу
            mouseX.AddToCurrentRecoil((horizontalPower));


            yield return null;
        }

        elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
        }

        
        recoilCount--;
       

        

    }
}
