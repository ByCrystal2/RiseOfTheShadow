using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSoundManager : MonoBehaviour
{
    [SerializeField] AudioSource m_audioSource;
    [SerializeField] AudioClip menuClip;
    void Start()
    {
        m_audioSource.enabled = true;
        m_audioSource.clip = menuClip;
        m_audioSource.Play();
    }

    
    void Update()
    {
        
    }

    public AudioSource GetAudioSource()
    {
        return m_audioSource;
    }

    public AudioClip GetMenuClip()
    {
        return menuClip;
    }
}
