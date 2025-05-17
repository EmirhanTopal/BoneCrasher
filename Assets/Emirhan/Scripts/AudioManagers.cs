using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagers : MonoBehaviour
{
    private AudioSource _audioSource;
    public AudioClip _punchClip;
    public AudioClip _kickClip;
    public AudioClip _jumpClip;
    //public AudioClip _walkClip;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayOneShotClip(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }
}
