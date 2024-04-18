using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;

    [SerializeField] private AudioClip level1Clip;
    [SerializeField] private AudioClip level2Clip;
    [SerializeField] private AudioClip level3Clip;
    [SerializeField] private AudioClip level4Clip;
    [SerializeField] private AudioClip level5Clip;
    private void Awake()
    {
        m_AudioSource = gameObject.transform.GetChild(0).GetComponent<AudioSource>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AudioSource GetLevelAudioSource()
    {
        return m_AudioSource;
    }

    public void PlayCurrentLevelClip(int currentLevel)
    {
        switch (currentLevel)
        {
            case 1:
                m_AudioSource.clip = level1Clip;
                break;
            case 2:
                m_AudioSource.clip = level2Clip;
                break;
            case 3:
                m_AudioSource.clip = level3Clip;
                break;
            case 4:
                m_AudioSource.clip = level4Clip;
                break;
            case 5:
                m_AudioSource.clip = level5Clip;
                break;
            default:
                break;
        }
        m_AudioSource.Play();
    }
}
