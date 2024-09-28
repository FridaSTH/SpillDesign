using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage = 1;

    void Start()
    {
        
    }

    private bool runOnce = false;
    // Update is called once per frame
    void Update()
    {
        if (runOnce)
        {
            Destroy(gameObject);
        }
        runOnce = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            // Deal damage
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null) {
                player.TakeDamage(damage);
                print("attacked player");
                Destroy(gameObject);
            }
        }
    }
}
