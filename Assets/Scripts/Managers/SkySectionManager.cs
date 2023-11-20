using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkySectionManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void ClearSkySections()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("SkySectionEndPoint"))
        {
            Destroy(this.gameObject);
            FindObjectOfType<GameManager>().SpawnSkySection();
        }
    }
}
