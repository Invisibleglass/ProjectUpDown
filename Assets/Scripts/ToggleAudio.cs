using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAudio : MonoBehaviour
{
    [SerializeField] private bool _toggleMusic;
    [SerializeField] private bool _toggleEffects;

    public void Toggle()
    {
        if (_toggleEffects)
        {
            AudioManager.Instance.ToggleEffects();
        }
        if (_toggleMusic)
        {
            AudioManager.Instance.ToggleMusic();
        }
    }

    public void SwapColorMusic()
    {
        if (AudioManager.Instance.isMusicMuted() == true)
        {
            GetComponent<Image>().color = Color.red;
        }
        else
        {
            GetComponent<Image>().color = Color.green;
        }
    }

    public void SwapColorEffects()
    {
        if (AudioManager.Instance.isEffectsMuted() == true)
        {
            GetComponent<Image>().color = Color.red;
        }
        else
        {
            GetComponent<Image>().color = Color.green;
        }
    }
}
