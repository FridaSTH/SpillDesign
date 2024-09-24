using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public Collider2D hitBox;                       // For getting hit
    public WeaponController weaponController;       // For changing weapon

    Vector2 movementInput;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    

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
}
