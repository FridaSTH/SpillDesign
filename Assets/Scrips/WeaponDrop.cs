using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDrop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            // Gets picked up
            PlayerController player = other.GetComponent<PlayerController>();
            if (player.weaponController != null) {
                foreach (GameObject weapon in player.weaponController.weaponList) {
                    string weaponName = weapon.GetComponent<SpriteRenderer>().sprite.name;
                    if (weaponName == gameObject.GetComponent<SpriteRenderer>().sprite.name) {
                        player.weaponController.SetCurrentWeapon(weapon);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
