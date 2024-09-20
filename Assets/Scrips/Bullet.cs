using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 1f;
    public float speed = 1f;
    public float timeToDisappear;
    Vector2 direction;
    Rigidbody2D rb;
    //Quaternion initialQuaternion;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Find direction the bullet should fly toward
        Vector2 mousePos = Input.mousePosition;
        Camera cam = Camera.main;
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
        //Vector3 screenPos = gameObject.transform.parent.GetComponentInParent<WeaponPosition>().screenPos;
        //Vector3 screenPos = new Vector3(0.1f, 0.1f, 0f);
        direction = new Vector2(mousePos.x - screenPos.x, mousePos.y - screenPos.y);
        direction.Normalize();

        Destroy(gameObject, 1.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * 2f * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {
            // Deal damage
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null) {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
