using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public Image bar;

    public void ChangeBar(float val) {
        if (val < 0)
            val = 0;
        bar.fillAmount = val;
    }
}
