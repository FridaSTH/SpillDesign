using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float healthPoints = 5f;
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public Collider2D hitBox;                       // For getting hit
    public WeaponController weaponController;       // For changing weapon

    Vector2 movementInput;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

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

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;
    public GameObject heart5;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        if(movementInput != Vector2.zero){
            bool success = TryMove(movementInput);
            // If the movement is block, try moving sideways
            if(!success) {
                success = TryMove(new Vector2(movementInput.x, 0));
                if(!success) {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }
            animator.SetBool("isMovingDown", success);   // Need to add more moving directions animations
        } else {
            animator.SetBool("isMovingDown", false);
        }   
    }

    // Check if there is an object in the moving direction and if not move toward that direction
    private bool TryMove(Vector2 direction) {
        int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            moveSpeed * Time.fixedDeltaTime + collisionOffset
        );
        if(count == 0){
            rb.MovePosition(rb.position + direction * 0.8f * Time.fixedDeltaTime);
            return true;
        } else {
            return false;
        };
    }

    // Get movement value from the player
    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }

    // Runs once when left mouse button pressed to fire bullet using weapon controller
    void OnFire() {
        weaponController.GetComponent<WeaponController>().SingleFire();
    }

    public void Defeated(){
        print("Died");
        SceneManager.LoadSceneAsync(0);
    }

    public void TakeDamage(float damageValue) {
        HealthPoints -= damageValue;
        // Here we can calculate other stats like defense and shield ect
        ResetHealthUI();
    }

    public void ResetHealthUI() {
        if (healthPoints == 1f) {
            heart1.SetActive(true);
            heart2.SetActive(false);
            heart3.SetActive(false);
            heart4.SetActive(false);
            heart5.SetActive(false);
        } else if (healthPoints == 2f) {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(false);
            heart4.SetActive(false);
            heart5.SetActive(false);
        } else if (healthPoints == 3f) {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
            heart4.SetActive(false);
            heart5.SetActive(false);
        } else if (healthPoints == 4f) {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
            heart4.SetActive(true);
            heart5.SetActive(false);
        } else if (healthPoints == 5f) {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
            heart4.SetActive(true);
            heart5.SetActive(true);
        }
    }
    
}
