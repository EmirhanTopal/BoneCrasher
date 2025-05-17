using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShake : MonoBehaviour
{
    private Vector3 originalPos;

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
