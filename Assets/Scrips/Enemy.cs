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
    private List<GameObject> dropList = new List<GameObject>();
    public float dropRate;
    public float collisionOffset = 0.02f;
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
        if (drop1 != null) {
            dropList.Add(drop1);
        }
        if (drop2 != null) {
            dropList.Add(drop2);
        }
        if (drop3 != null) {
            dropList.Add(drop3);
        }
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
            Attack();
            return false;
        };
    }

    void OnMove(Vector2 movementValue) {
        movementInput = movementValue;
    }

    public void TakeDamage(float damageValue) {
        HealthPoints -= damageValue;
        // Here we can calculate other stats like defense and shield etc
        
    }

    public void Defeated() {
        animator.SetTrigger("Defeated");
        isDying = true;
        if (hitBox != null) {
            hitBox.enabled = false;
        }
    }

    public void RemoveEnemy() {
        if (dropRate > Random.Range(0f, 1f)) {
            if (dropList.Count > 0)
            {
                int randomIndex = Random.Range(0, dropList.Count); // Get a random index
                GameObject selectedObject = dropList[randomIndex]; // Select the GameObject
                Instantiate(selectedObject, transform.position, Quaternion.identity);
            }
        }
        Destroy(gameObject);
    }

    public GameObject attack;
    //private bool notAttacked = true;
    private float timer = 0f;
    private float attackCooldown = 1f;
    // Generate attack collider for 1 update and cooldown 0.5 sec
    public void Attack() {

        timer += Time.deltaTime;
        if (timer >= attackCooldown) {
            Instantiate(attack, transform.position, Quaternion.identity);
            timer = 0f;
            // if(notAttacked) {
            //         attack.GetComponent<Collider2D>().enabled = true;
            //         print("attack");
            //         notAttacked = false;
            // }
            // attack.GetComponent<Collider2D>().enabled = false;
        }
    }
}
