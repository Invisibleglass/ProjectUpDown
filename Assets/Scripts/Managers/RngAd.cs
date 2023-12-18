using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RngAd : MonoBehaviour
{
    [SerializeField] private Button AdTime; 

    // Start is called before the first frame update
    void Start()
    {
        if (AdTime)
            AdTime.onClick.AddListener(PlayAd);
        if (Random.Range(0,5) == 4)
        {
            AdTime.gameObject.SetActive(true);
        }
    }

    private void PlayAd()
    {
        //Play ad and if ad completed double coins
        
    }
}
