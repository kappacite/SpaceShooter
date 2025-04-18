using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float currentAcceleration;
    public float accelerationPower = 0.3f;
    public float horizontalMovement;
    public float verticalMovement;
    public bool buttonPressed;
    Rigidbody2D rb;
    float speed = 2f;
    public Animator animator;

    private Vector3 _mousePosition;
    
    public LayerMask wallLayer;
   
    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _mousePosition = Input.mousePosition;
    }

    public void Move(InputAction.CallbackContext context) {
        buttonPressed = context.performed;
        if (buttonPressed) {
            horizontalMovement = Keyboard.current.dKey.isPressed ? 1 : (Keyboard.current.aKey.isPressed ? -1 : 0);
            verticalMovement = Keyboard.current.wKey.isPressed ? 1 : (Keyboard.current.sKey.isPressed ? -1 : 0);
            animator.SetTrigger("IsPowering");
        } else {
            animator.ResetTrigger("IsPowering");
        }
    }

    void FaceMouse() {
        _mousePosition = Input.mousePosition;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * horizontalMovement, Mathf.Infinity, wallLayer );
        if (hit.collider == null || hit.distance > transform.localScale.x / 2){
            UpdateAcceleration();
            rb.velocity = new Vector2(horizontalMovement, verticalMovement).normalized * speed * currentAcceleration;
        }
        if (Input.mousePosition != _mousePosition)
        {
            FaceMouse();
        }
        
        
    }

    private void UpdateAcceleration() {

        if (!buttonPressed && currentAcceleration > 0) {

            currentAcceleration -= currentAcceleration * Time.fixedDeltaTime * 3f;
        }
        else if (currentAcceleration < 1) {
            currentAcceleration += accelerationPower * Time.fixedDeltaTime;
        }

    }
}
