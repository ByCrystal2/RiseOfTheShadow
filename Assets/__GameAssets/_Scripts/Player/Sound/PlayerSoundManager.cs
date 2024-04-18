using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip runClip;
    [SerializeField] AudioClip jumpClip;

    [SerializeField] AudioClip shotClip;
    [SerializeField] AudioClip damageClip;
    private void Awake()
    {
        audioSource = gameObject.transform.GetChild(2).GetComponent<AudioSource>();
    }
   

    public AudioSource GetPlayerAudio()
    {
        return audioSource;
    }

    public AudioClip GetPlayerRunClip()
    {
        return runClip;
    }

    public AudioClip GetPlayerJumpClip()
    {
        return jumpClip;
    }

    public AudioClip GetPlayerShotClip()
    {
        return shotClip;
    }

    public AudioClip GetPlayerDamageClip()
    {
        return damageClip;
    }



}
