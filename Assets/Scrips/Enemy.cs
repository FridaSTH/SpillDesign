using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float healthPoints = 2;
    Animator animator;

    public float moveSpeed = 1f;
    public GameObject drop1;
    public GameObject drop2;
    public GameObject drop3;
    public float dropRate;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    GameObject player;
    private bool isDying = false;
    public Collider2D hitBox;

    // Start is called before the first frame update
    void Start()
    {
        //print("dsfds");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
    }
    public float HealthPoints {
        set {
            healthPoints = value;
            if(healthPoints <= 0) {
                Defeated();
            }
        }
        get {
            return healthPoints;
        }
    }

    // Update is called once per frame
    private void FixedUpdate() {
        // Find player and move towards the player
        if (isDying != false) {
            animator.SetBool("isMoving", false);
        } else {
            movementInput = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            movementInput.Normalize();
            if (movementInput != Vector2.zero){
                bool success = TryMove(movementInput);
                if(!success) {
                    success = TryMove(new Vector2(movementInput.x, 0));
                    if(!success) {
                        success = TryMove(new Vector2(0, movementInput.y));
                    }
                }
                animator.SetBool("isMoving", success);   // Need to add more moving directions
            } else {
                animator.SetBool("isMoving", false);
            }
        }
    }

    private bool TryMove(Vector2 direction) {
        int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            moveSpeed * Time.fixedDeltaTime + collisionOffset
        );
        if(count == 0){
            rb.MovePosition(rb.position + direction * 0.3f * Time.fixedDeltaTime);
            
            return true;
        } else {
            return false;
        };
    }

    void OnMove(Vector2 movementValue) {
        movementInput = movementValue;
    }

    public void TakeDamage(float damageValue) {
        HealthPoints -= damageValue;
        // Here we can calculate other stats like defense and shield ect
    }

    public void Defeated() {
        animator.SetTrigger("Defeated");
        isDying = true;
        if (hitBox != null) {
            hitBox.enabled = false;
        }
    }

    public void RemoveEnemy() {
        Destroy(gameObject);
    }
}
