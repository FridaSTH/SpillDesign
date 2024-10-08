using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDrop : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            // Gets picked up
            PlayerController player = other.GetComponent<PlayerController>();
            if (player.weaponController != null) {
                foreach (GameObject weapon in player.weaponController.weaponList) {
                    string weaponName = weapon.GetComponent<SpriteRenderer>().sprite.name;
                    if (weaponName == gameObject.GetComponent<SpriteRenderer>().sprite.name) {
                        player.weaponController.SetCurrentWeapon(weapon);
                        // Play weapon collected sound
                        AudioManager.instance.PlayOneShot(FMODEvents.instance.weaponCollect, this.transform.position);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
