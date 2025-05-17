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
    public AudioClip _selaClip;
    private bool isSela = false;

    private CharacterMovement _characterMovement;
    //public AudioClip _walkClip;
    private void Start()
    {
        _characterMovement = FindObjectOfType<CharacterMovement>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        OnlySela();
    }

    private void OnlySela()
    {
        if (_characterMovement.isGeberdi)
        {
            if (!isSela)
            {
                _audioSource.PlayOneShot(_selaClip);
                isSela = true;
            }
        }
    }
    public void PlayOneShotClip(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }
}
