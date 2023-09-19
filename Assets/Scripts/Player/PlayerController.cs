using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 swipeStartPos;
    private PlayerControls controls;
    private Rigidbody2D rb;

    private float continuousForce = 25f;
    private bool isTapPressed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.Tap.GoUp.performed += OnHold;
        controls.Tap.GoUp.canceled += OnHoldEnd;
        //controls.Swipe.VehicleTeleport.started += OnSwipeStart;
        //controls.Swipe.VehicleTeleport.canceled += OnSwipeCanceled;
    }

    private void OnDisable()
    {
        controls.Tap.GoUp.performed -= OnHold;
        controls.Tap.GoUp.canceled -= OnHoldEnd;
        controls.Disable();
        //controls.Swipe.VehicleTeleport.started -= OnSwipeStart;
        //controls.Swipe.VehicleTeleport.canceled -= OnSwipeCanceled;
    }
    // Update is called once per frame
    void Update()
    {
        if(isTapPressed == true)
        {
            //USE continuousForce * Time.deltaTime in order to solve different frame rates
            rb.AddForce(Vector2.up * continuousForce * Time.deltaTime, ForceMode2D.Impulse);
        }
    }

    private void OnHoldEnd(InputAction.CallbackContext context)
    {
        FindObjectOfType<UIManager>().TapTextDeactive();
        isTapPressed = false;
    }

    private void OnHold(InputAction.CallbackContext context)
    {
        FindObjectOfType<UIManager>().TapTextActive();
        // Handle tap input
        AccelerateUp();
    }

    private void AccelerateUp()
    {
        isTapPressed = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Adds to score/ currency
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            FindObjectOfType<GameManager>().AddScore();
        }
        if(other.gameObject.CompareTag("Glass"))
        {
            //Glass Explosion function here
        }
        ///ADD GAMEOVER SCREEN AND STOP GAMEPLAY
        if (other.gameObject.CompareTag("Wall"))
        {
            controls.Disable();
            FindObjectOfType<UIManager>().GameOver();
            Destroy(gameObject);
        }
        if(other.gameObject.CompareTag("Laser"))
        {
            controls.Disable();
            Destroy(gameObject);
        }

    }
}
