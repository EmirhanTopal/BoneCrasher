using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class cameraShake : MonoBehaviour
{
    private Vector3 originalPos;
    private StunHandler _stunHandler;
    private CharacterMovement _characterMovement;
    public float maxBorder;
    public float minBorder;
    public float midSide;
    private Transform _p1Transform;
    private Transform _p2Transform;
    [SerializeField] private int maxDist = 16;
    private void Start()
    {
        _characterMovement = FindObjectOfType<CharacterMovement>();
        _stunHandler = FindObjectOfType<StunHandler>();
        _p1Transform = _stunHandler.player1.gameObject.transform;
        _p2Transform = _stunHandler.player2.gameObject.transform;
    }

    public float cameraHalfWidth = 8f; // kırmızı çerçevenin yarısı

    void Update()
    {
        float p1X = _p1Transform.position.x;
        float p2X = _p2Transform.position.x;

        float midX = (p1X + p2X) / 2f;
        float leftLimit = midX - cameraHalfWidth;
        float rightLimit = midX + cameraHalfWidth;

        var p1 = _p1Transform.GetComponent<CharacterMovement>();
        var p2 = _p2Transform.GetComponent<CharacterMovement>();

// Oyuncuların ekran dışına çıkma durumunu kontrol et
        bool p1OutLeft = p1X < leftLimit + 0.5f;
        bool p1OutRight = p1X > rightLimit - 0.5f;
        bool p2OutLeft = p2X < leftLimit + 0.5f;
        bool p2OutRight = p2X > rightLimit - 0.5f;

// İkisini de serbest bırak
        p1.SetCanMove(true);
        p2.SetCanMove(true);

// P1 sola gidip P2'yi dışarı atıyorsa → P1 dursun
        if (p2OutLeft && p1X < p2X)
            p1.SetCanMove(false, -1f); // -1 sola yürümeyi engeller

// P1 sağa gidip P2'yi dışarı atıyorsa → P1 dursun
        if (p2OutRight && p1X > p2X)
            p1.SetCanMove(false, 1f);

// Tersi senaryo
        if (p1OutLeft && p2X < p1X)
            p2.SetCanMove(false, -1f);

        if (p1OutRight && p2X > p1X)
            p2.SetCanMove(false, 1f);

    }

    private void LateUpdate()
    {
        midSide = (_p1Transform.position.x + _p2Transform.position.x) / 2f;
        float clampedValue = Mathf.Clamp(midSide, minBorder, maxBorder);
        transform.position = new Vector3(clampedValue, transform.position.y, transform.position.z);
    }

    public void ShakeFunc(float duration, float magnitude)
    {
        StartCoroutine(ShakeShake(duration, magnitude));
    }
    
    private IEnumerator ShakeShake(float duration, float magnitude)
    {
        originalPos = transform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;
            
            transform.localPosition = new Vector3(offsetX, offsetY, originalPos.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
        
    }
}
