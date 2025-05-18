using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManagers : MonoBehaviour
{
    private AudioSource _audioSource;
    public AudioClip _punchClip;
    public AudioClip _kickClip;
    public AudioClip _jumpClip;
    public AudioClip _selaClip;
    private bool isSela = false;

    [SerializeField] private CharacterMovement _p1;
    [SerializeField] private CharacterMovement _p2;
    //public AudioClip _walkClip;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        OnlySela();
    }

    private void OnlySela()
    {
        if (_p1.isGeberdi || _p2.isGeberdi)
        {
            if (!isSela)
            {
                _audioSource.PlayOneShot(_selaClip);
                //KO image
                isSela = true;
                StartCoroutine(WaitNextScene());
            }
        }
    }

    private IEnumerator WaitNextScene()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("CreditScene");
    }
    public void PlayOneShotClip(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }
}
