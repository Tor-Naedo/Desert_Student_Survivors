using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -25.772f);
    }

    public IEnumerator Shake (float duration, float magnitude)
    {
        Vector3 originalPros = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPros.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPros;
    }
}
