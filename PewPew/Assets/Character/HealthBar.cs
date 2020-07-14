using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public Image bar;
    private float fill;

    private void Start() {
        fill = 1f;
    }

    public void ChangeBar(float val) {
        if (val < 0)
            val = 0;
        bar.fillAmount = val;
    }
}
