using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = FindObjectOfType<GameManager>().score.ToString() + " Points";
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = FindObjectOfType<GameManager>().score.ToString() + " Points";
    }
}
