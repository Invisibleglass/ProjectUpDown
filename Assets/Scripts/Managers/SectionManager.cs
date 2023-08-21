using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SectionManager : MonoBehaviour
{
    Rigidbody2D rb;

    //put this into GameManager
    public float continuousForce = 100;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(Vector2.left * continuousForce, ForceMode2D.Force);  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
