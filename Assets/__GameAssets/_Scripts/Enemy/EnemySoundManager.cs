using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundManager : MonoBehaviour
{
    AudioSource audioSource; 

    [SerializeField] AudioClip deathClip;

    [SerializeField] AudioClip shotGunClip;
    [SerializeField] AudioClip magicClip;
    [SerializeField] AudioClip arrowClip;
    [SerializeField] AudioClip smallShotClip;
    private void Awake()
    {
        audioSource = gameObject.transform.GetChild(2).GetComponent<AudioSource>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AudioSource GetEnemyAudio()
    {
        return audioSource;
    }

    public AudioClip GetEnemyShotgunClip()
    {
        return shotGunClip;
    }

    public AudioClip GetEnemySmallShotClip()
    {
        return smallShotClip;
    }

    public AudioClip GetEnemyMagicClip()
    {
        return magicClip;
    }


    public AudioClip GetEnemyArrowClip()
    {
        return arrowClip;
    }


    public AudioClip GetEnemyDeathClip()
    {
        return deathClip;
    }
}
