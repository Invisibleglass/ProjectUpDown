using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> sections;

    public Transform sectionSpawnpoint;

    private float score = 0f;
    public float continuousForce = 1f;

    // Start is called before the first frame update
    void Start()
    {
        NewSectionSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        score++;
        //Create Lasers
        /*if (score >= 1000)
        {
            //warning symbol spawn

            //laser spawn

        }
        */
    }

    //applies force to the section increasing based on score
    private void NewSectionForce()
    {
        FindObjectOfType<SectionManager>().GetComponent<Rigidbody2D>().AddForce(Vector2.left * (continuousForce + (score/1000)) , ForceMode2D.Force);
    }

    //spawns new section
    public void NewSectionSpawn()
    {
        //spawns a random section from the list of sections
        int randomIndex = Random.Range(0, sections.Count);
        GameObject section = Instantiate(sections[randomIndex], sectionSpawnpoint);
        NewSectionForce();
    }
}
