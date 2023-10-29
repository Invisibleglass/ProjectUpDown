using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AlejandroPlayerController : BasePlayerController
{
    protected override void Awake()
    {
        base.Awake();
    }

    public Sprite flyingSprite;
    public Sprite walkingSprite;

    public AudioClip coinSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Adds to score/currency
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            FindObjectOfType<AudioManager>().PlaySound(coinSound);
            FindObjectOfType<GameManager>().AddScore();
        }
        if (other.gameObject.CompareTag("Glass"))
        {
            //Glass Explosion function here
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            controls.Disable();
            FindObjectOfType<UIManager>().GameOver();
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Laser"))
        {
            controls.Disable();
            FindObjectOfType<UIManager>().GameOver();
            Destroy(gameObject);
        }

    }

    protected override void OnHoldEnd(InputAction.CallbackContext context)
    {
        base.OnHoldEnd(context);

        ChangeSpriteWalking();
    }

    protected override void OnHold(InputAction.CallbackContext context)
    {
        base.OnHold(context);

        ChangeSpriteFlying();
    }

    private void ChangeSpriteFlying()
    {
        GetComponent<SpriteRenderer>().sprite = flyingSprite;
    }

    private void ChangeSpriteWalking()
    {
        GetComponent<SpriteRenderer>().sprite = walkingSprite;
    }

}
