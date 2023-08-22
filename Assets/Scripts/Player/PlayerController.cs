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

    private float continuousForce = 2f;
    private bool isTapPressed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new PlayerControls();
        controls.Enable();
    }

    private void OnEnable()
    {
        controls.Tap.GoUp.performed += OnHold;
        controls.Tap.GoUp.canceled += OnHoldEnd;
        //controls.Swipe.VehicleTeleport.started += OnSwipeStart;
        //controls.Swipe.VehicleTeleport.canceled += OnSwipeCanceled;
    }

    private void OnDisable()
    {
        controls.Tap.GoUp.performed -= OnHold;
        controls.Tap.GoUp.canceled -= OnHoldEnd;
        //controls.Swipe.VehicleTeleport.started -= OnSwipeStart;
        //controls.Swipe.VehicleTeleport.canceled -= OnSwipeCanceled;
    }

    private void OnHoldEnd(InputAction.CallbackContext context)
    {
        Debug.Log("Tap ended!");
        isTapPressed = false;
    }

    private void OnHold(InputAction.CallbackContext context)
    {
        Debug.Log("Tap detected!");
        // Handle tap input
        AccelerateUp();
    }

    
    private void AccelerateUp()
    {
        isTapPressed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTapPressed == true)
        {
            rb.AddForce(Vector2.up * continuousForce, ForceMode2D.Force);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Adds to score/ currency
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
        }
        //Ends Run
        if (other.gameObject.CompareTag("Wall"))
        {
            controls.Disable();
            Destroy(gameObject);
        }
        if(other.gameObject.CompareTag("Glass"))
        {
            //Glass Explosion function here
        }
    }
}
