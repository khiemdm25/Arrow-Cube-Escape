using UnityEngine;
using System.Collections;

public class LineShake : MonoBehaviour
{
    [SerializeField] private float duration = 0.15f;
    [SerializeField] private float strength = 0.05f;

    private Vector3 _originalPos;

    public void PlayShake()
    {
        StopAllCoroutines();
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        _originalPos = transform.localPosition;

        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;

            Vector3 offset = Random.insideUnitSphere * strength;
            transform.localPosition = _originalPos + offset;

            yield return null;
        }

        transform.localPosition = _originalPos;
    }
}