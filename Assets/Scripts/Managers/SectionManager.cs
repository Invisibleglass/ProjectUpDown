using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SectionManager : MonoBehaviour
{
    public bool pauseSection = false;
    private float pauseLocation = -15f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (pauseSection == true)
        {
            if (transform.position.x <= pauseLocation)
            {
                FindObjectOfType<GameManager>().SectionCounterForce(FindObjectOfType<SectionManager>().gameObject) ;
                pauseSection = false;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("SectionEndPoint"))
        {
            Destroy(this.gameObject);
            FindObjectOfType<GameManager>().NewSectionSpawn();
        }
    }
}
