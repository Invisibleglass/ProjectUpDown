using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasePlayerController : MonoBehaviour
{
    /*protected CharacterSwitchHandler characterSwitcher;

    public BasePlayerController(CharacterSwitchHandler characterSwitcher)
    {
        this.characterSwitcher = characterSwitcher;
    }*/

    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected PlayerControls controls;

    protected float continuousForce = 25f;
    protected bool isTapPressed;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new PlayerControls();
    }

    public void OnEnable()
    {
        controls.Enable();
        controls.Tap.GoUp.performed += OnHold;
        controls.Tap.GoUp.canceled += OnHoldEnd;
        //controls.Swipe.VehicleTeleport.started += OnSwipeStart;
        //controls.Swipe.VehicleTeleport.canceled += OnSwipeCanceled;
    }

    public void OnDisable()
    {
        controls.Tap.GoUp.performed -= OnHold;
        controls.Tap.GoUp.canceled -= OnHoldEnd;
        controls.Disable();
        //controls.Swipe.VehicleTeleport.started -= OnSwipeStart;
        //controls.Swipe.VehicleTeleport.canceled -= OnSwipeCanceled;
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        if (isTapPressed == true)
        {
            //USE continuousForce * Time.deltaTime in order to solve different frame rates
            rb.AddForce(Vector2.up * continuousForce * Time.deltaTime, ForceMode2D.Impulse);
        }
    }

    protected virtual void OnHoldEnd(InputAction.CallbackContext context)
    {
        FindObjectOfType<UIManager>().TapTextDeactive();
        isTapPressed = false;
        ///ChangeSpriteWalking();
    }

    protected virtual void OnHold(InputAction.CallbackContext context)
    {
        FindObjectOfType<UIManager>().TapTextActive();
        AccelerateUp();
        ///ChangeSpriteFlying();
    }

    protected virtual void AccelerateUp()
    {
        isTapPressed = true;
    }


}
