using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    public GameObject bulletGeneration;
    private GameObject currentWeapon;
    private List<GameObject> weaponList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        if (weapon2 != null) {
            weaponList.Add(weapon2);
            print("added weapon 2");
        }
        if (weapon3 != null) {
            weaponList.Add(weapon3);
            print("added weapon 3");
        }
        if (weapon1 != null) {
            weaponList.Add(weapon1);
            print("added weapon 1");
            SetCurrentWeapon(weapon1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrentWeapon(GameObject weapon) {
        currentWeapon = weapon;
        foreach (GameObject weaponItem in weaponList) {
            weaponItem.GetComponent<SpriteRenderer>().enabled = false;
            if (weaponItem == currentWeapon) {
                weaponItem.GetComponent<SpriteRenderer>().enabled = true;
            } else {
                weaponItem.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
}
