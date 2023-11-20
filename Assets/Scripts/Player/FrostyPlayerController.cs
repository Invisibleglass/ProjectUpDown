using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostyPlayerController : BasePlayerController
{
    private GameManager gameManager;

    [SerializeField] private GameObject body;
    [SerializeField] private GameObject body2;
    [SerializeField] private GameObject body3;
    [SerializeField] private GameObject body4;
    [SerializeField] private BoxCollider2D roof;
    [SerializeField] private BoxCollider2D skyFloor;

    private float flightVelocity = 10f;
    private float delay = 0.1f;
    private float flightHeightSky = 9f;
    private float flightHeightCave = 3f;

    private bool isGoingUp;

    public AudioClip coinSound;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    IEnumerator FollowHead()
    {
        float currentPosition = this.gameObject.transform.position.y;
        float bodyPosition = body.transform.position.y;
        float body2Position = body.transform.position.y;
        float body3Position = body.transform.position.y;

        yield return new WaitForSeconds(delay);
        body.transform.position = new Vector3(body.transform.position.x, currentPosition, body.transform.position.z);
        body2.transform.position = new Vector3(body2.transform.position.x, bodyPosition, body2.transform.position.z);
        yield return new WaitForSeconds(delay);
        body3.transform.position = new Vector3(body3.transform.position.x, body2Position, body3.transform.position.z);
        yield return new WaitForSeconds(delay);
        body4.transform.position = new Vector3(body4.transform.position.x, body3Position, body4.transform.position.z);
    }

    protected override void Update()
    {
        base.Update();

        StartCoroutine(FollowHead());
    }

    public void SetWormPositions(Vector3 currentPosition)
    {
        gameObject.transform.position = currentPosition;
        body.transform.position = new Vector3(body.transform.position.x, currentPosition.y, body.transform.position.z);
        body2.transform.position = new Vector3(body2.transform.position.x, currentPosition.y, body2.transform.position.z);
        body3.transform.position = new Vector3(body3.transform.position.x, currentPosition.y, body3.transform.position.z);
        body4.transform.position = new Vector3(body4.transform.position.x, currentPosition.y, body4.transform.position.z);
    }

    //Takes the player to the top world
    public void TakeFlight()
    {
        StartCoroutine(FrostyGoesUp());
    }

    public void SinkDown()
    {
        StartCoroutine(FrostyGoesDown());
    }

    IEnumerator FrostyGoesUp()
    {
        isGoingUp = true;
        while (gameObject.transform.position.y < flightHeightSky)
        {
            //Frosty continues up
            rb.velocity = new Vector2(0f, flightVelocity);
            //roof collider disabled
            roof.enabled = false;
            //camera follows frosty
            FindObjectOfType<CameraManager>().FollowFrosty(isGoingUp);
            yield return null;
        }
        OnEnable();
        skyFloor.enabled = true;
        gameManager.SpawnSkySection();
    }

    IEnumerator FrostyGoesDown()
    {
        isGoingUp = false;
        OnDisable();
        while (gameObject.transform.position.y > flightHeightCave)
        {
            //Frosty continues down
            rb.velocity = new Vector2(0f, -flightVelocity);
            //skyFloor collider disabled
            skyFloor.enabled = false;
            //camera follows frosty
            FindObjectOfType<CameraManager>().FollowFrosty(isGoingUp);
            yield return null;
        }
        roof.enabled = true;
        gameManager.AddSectionForce(FindFirstObjectByType<SectionManager>().gameObject);
        FindObjectOfType<SkySectionManager>().ClearSkySections();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Adds to score/currency
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            FindObjectOfType<AudioManager>().PlaySound(coinSound);
            FindObjectOfType<GameManager>().AddScore();
        }
    }
}
