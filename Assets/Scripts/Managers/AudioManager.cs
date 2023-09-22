using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Music")]
    public AudioClip PlaySceneMusic;

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _effectsSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        _effectsSource.PlayOneShot(clip);
    }

    public void ToggleEffects()
    {
        _effectsSource.mute = !_effectsSource.mute;
    }

    public void ToggleMusic()
    {
        _musicSource.mute = !_musicSource.mute;
    }

    public bool isMusicMuted()
    {
        if (_musicSource.mute == true)
            return true;
        else
            return false;
    }

    public bool isEffectsMuted()
    {
        if (_effectsSource.mute == true)
            return true;
        else
            return false;
    }

    private void Update()
    {
        
    }
}
