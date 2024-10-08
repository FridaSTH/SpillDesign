using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGeneration : MonoBehaviour
{
    public Bullet bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void FireBullet(float speed) {
        //print("fired at " + speed + " speed"); //new Vector3(0 * 2.0f, 0, 0)
        Bullet bullet1 = Instantiate(bullet, transform.position, Quaternion.identity);
        bullet1.direction = BulletDirection(0f);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.playerFire, this.transform.position);
    }

    public void FireThreeBullets(float speed) {
        //print("fired at " + speed + " speed"); //new Vector3(0 * 2.0f, 0, 0)
        Bullet bullet1 = Instantiate(bullet, transform.position, Quaternion.identity);
        bullet1.direction = BulletDirection(-0.1f);
        Bullet bullet2 = Instantiate(bullet, transform.position, Quaternion.identity);
        bullet2.direction = BulletDirection(0f);
        Bullet bullet3 = Instantiate(bullet, transform.position, Quaternion.identity);
        bullet3.direction = BulletDirection(0.1f);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.playerFire, this.transform.position);
    }

    public Vector2 BulletDirection(float radians) {
        Vector2 mousePos = Input.mousePosition;
        Camera cam = Camera.main;
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
        //Vector3 screenPos = gameObject.transform.parent.GetComponentInParent<WeaponPosition>().screenPos;
        //Vector3 screenPos = new Vector3(0.1f, 0.1f, 0f);
        Vector2 direction = new Vector2(mousePos.x - screenPos.x, mousePos.y - screenPos.y);
        direction.Normalize();

        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);

        // Rotate the vector using the 2D rotation formula
        float newX = direction.x * cos - direction.y * sin;
        float newY = direction.x * sin + direction.y * cos;

        direction = new Vector2(newX, newY);
        return direction;
    }
}
