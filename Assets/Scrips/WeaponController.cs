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
    public List<GameObject> weaponList = new List<GameObject>();

    //public bool leftClickDown = false;
    // Start is called before the first frame update
    void Start()
    {
        if (weapon2 != null) {
            weaponList.Add(weapon2);
        }
        if (weapon3 != null) {
            weaponList.Add(weapon3);
        }
        if (weapon1 != null) {
            weaponList.Add(weapon1);
            SetCurrentWeapon(weapon1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if left mousebutton is holding down, should be on trigger, but whatever for now
        if (Input.GetMouseButton(0)) {
            ContinuingFire();
        }
        
    }

    public void SetCurrentWeapon(GameObject weapon) {
        currentWeapon = weapon;
        foreach (GameObject weaponItem in weaponList) {
            //weaponItem.GetComponent<SpriteRenderer>().enabled = false;
            SpriteRenderer renderer = weaponItem.GetComponent<SpriteRenderer>();
            if (weaponItem == currentWeapon) {
                renderer.enabled = true;
                //if (renderer.sprite.name == "22") {} machinegun
                //if (renderer.sprite.name == "44") {} shotgun
            } else {
                renderer.enabled = false;
            }
        }
    }

    public void SingleFire() {
        if (currentWeapon.GetComponent<SpriteRenderer>().sprite.name == "44") {
            FireThreeBullets();
        } else if (currentWeapon.GetComponent<SpriteRenderer>().sprite.name == "22") {
            FireSingleBullet();
        } else {
            FireSingleBullet();
        }
    }

    private float timer = 0f;
    private float interval = 0.1f;
    public void ContinuingFire() {
        if (currentWeapon.GetComponent<SpriteRenderer>().sprite.name == "22") {

            
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                FireSingleBullet();
                timer = 0f;
            }
        }
    }

    public void FireSingleBullet() {
        bulletGeneration.GetComponent<BulletGeneration>().FireBullet(5f);
    }

    public void FireThreeBullets() {
        bulletGeneration.GetComponent<BulletGeneration>().FireThreeBullets(5f);
    }
}
