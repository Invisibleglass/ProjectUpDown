using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Section Prefabs")]
    public List<GameObject> sections;
    public Transform sectionSpawnpoint;

    [Header("Laser Prefabs")]
    public GameObject dangerIcon;
    public GameObject laser1;
    public GameObject laser2;
    public GameObject laser3;

    [HideInInspector] public float score = 0f;

    //Laser location
    private float randomLaserY;

    //Timer for score
    private float waitTime = 1f;
    private float timer;
    private float totalTimer = 0f;

    [HideInInspector] public float continuousForce = 1f;

    // Start is called before the first frame update
    void Start()
    {
        NewSectionSpawn();
        StartCoroutine(LaserSpawnTimer());
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        // Check if a second has gone by and subtracting that second so it is more accurate
        if (timer > waitTime)
        {
            timer = timer - waitTime;
            score++;
            totalTimer++;
        }
    }

    //applies force to the section increasing based on score
    private void NewSectionForce()
    {
        FindObjectOfType<SectionManager>().GetComponent<Rigidbody2D>().AddForce(Vector2.left * (continuousForce + (score / 10)), ForceMode2D.Force);
    }

    //spawns new section
    public void NewSectionSpawn()
    {
        //spawns a random section from the list of sections
        int randomIndex = Random.Range(0, sections.Count);
        GameObject section = Instantiate(sections[randomIndex], sectionSpawnpoint);
        NewSectionForce();
    }

    //used to add 5 points to the score (Player/Coin collision)
    public void AddScore()
    {
        score = score + 5;
    }

    IEnumerator LaserSpawnTimer()
    {
        //print the time when LaserSpawnTimer is called
        Debug.Log("Started LaserSpawnTimer at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 15 seconds.
        yield return new WaitForSeconds(15f);

        //After we have waited 5 seconds print the time again and start lasers :)
        Debug.Log("Finished LaserSpawnTimer at timestamp : " + Time.time);
        StartCoroutine(LaserIconSpawn());
    }

    IEnumerator LaserIconSpawn()
    {
        Debug.Log("Started LaserIconSpawn at timestamp : " + Time.time);

        //random icon height
        randomLaserY = Random.Range(-2.5f, 2.5f);
        Vector3 dangerIconTransform = new Vector3(8.5f, randomLaserY, 0f);

        //warning symbol spawn * 3
        GameObject warning = Instantiate(dangerIcon, dangerIconTransform, gameObject.transform.rotation);

        yield return new WaitForSeconds(0.2f);
        Destroy(warning);

        yield return new WaitForSeconds(0.1f);
        GameObject warning2 = Instantiate(dangerIcon, dangerIconTransform, gameObject.transform.rotation);

        yield return new WaitForSeconds(0.2f);
        Destroy(warning2);

        yield return new WaitForSeconds(0.1f);
        GameObject warning3 = Instantiate(dangerIcon, dangerIconTransform, gameObject.transform.rotation);

        yield return new WaitForSeconds(0.2f);
        Destroy(warning3);
        Debug.Log("Finished LaserIconSpawn at timestamp : " + Time.time);
        ///FIX LATER IF PLAYER DIES TO STOP THE LOOP
        StartCoroutine(LaserSpawn());
    }

    IEnumerator LaserSpawn()
    {
        Debug.Log("Started LaserSpawn at timestamp : " + Time.time);

        yield return new WaitForSeconds(0.2f);

        Vector3 LaserTransform = new Vector3(0f, randomLaserY, 0f);
        GameObject Laser1 = Instantiate(laser1, LaserTransform, gameObject.transform.rotation);

        yield return new WaitForSeconds(0.1f);
        GameObject Laser2 = Instantiate(laser2, LaserTransform, gameObject.transform.rotation);
        Destroy(Laser1);

        yield return new WaitForSeconds(0.1f);
        GameObject Laser3 = Instantiate(laser3, LaserTransform, gameObject.transform.rotation);
        Destroy(Laser2);

        yield return new WaitForSeconds(0.8f);
        Destroy(Laser3);

        Debug.Log("Finished LaserSpawn at timestamp : " + Time.time);
        yield return new WaitForSeconds(Random.Range(5f,10f));

        ///FIX LATER IF PLAYER DIES TO STOP THE LOOP
        StartCoroutine(LaserIconSpawn());
    }
}
