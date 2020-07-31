using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponAmmunition {
    private int capacity;
    public int currCapacity;
    private int count;
    private int type = 1;    //разные перезарядки, 1 - оставшиеся в абойме патроны не теряются. 2 - теряются.
    public int total;
    private Text[] UItexts;

    public WeaponAmmunition(int capacity, int count) {
        this.capacity = capacity;
        this.currCapacity = capacity;
        this.count = count;
        this.total = capacity * count;
        UItexts = GameObject.FindObjectsOfType<Text>();

        UItexts[1].text = currCapacity.ToString();
        UItexts[0].text = total.ToString();
    }

    public void Reload() {
        if (type == 1) {
            if (total > 0)

            total -= capacity - currCapacity;
            if (total <= 0)
                currCapacity = capacity + total;
            else
                currCapacity = capacity;
        }

        else if (type == 2) {
            if (count > 0)

            count--;
            currCapacity = capacity;
            total = capacity * count;
        }

        UItexts[1].text = currCapacity.ToString();
        UItexts[0].text = total.ToString();
    }

    public void SpendAmmo() {
        currCapacity--;
        UItexts[1].text = currCapacity.ToString();
    }

    public void TakeAmmo(int totalAmmo) {
        count += totalAmmo / capacity;
        total = count * capacity;
        UItexts[0].text = total.ToString();
    }

    public bool isEmpty() {
        if (currCapacity <= 0)
            return true;
        else
            return false;
    }
}
