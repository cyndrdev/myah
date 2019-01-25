using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockableObject : MonoBehaviour
{
    [Header("Wobble stuff")]
    public float wobbleSpeed;
    public float wobbleAmplitude;
    [Range(0, 1)]
    public float damping;

    [Header("Nudge stuff")]
    public float nudgeDistance;
    public float nudgeBounceHeight;
    public float nudgeSpeed;

    private bool _isKnockable = true;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!_isKnockable)
            return;

        Vector2 direction;
        if (collider.transform.position.x < transform.position.x)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }

        var player = collider.GetComponent<PlayerController>();
        if (player != null)
        {
            print("body knock");
            StartCoroutine(Knock(direction));
        }

        var hand = collider.GetComponent<Hand>();
        if (hand != null)
        {
            print("hand knock");
            StartCoroutine(Knock(direction));
        }
    }

    private IEnumerator Knock(Vector2 direction)
    {
        switch (UnityEngine.Random.Range(0, 2))
        {
            case 0:
                return Nudge(direction);

            case 1:
                return Wobble(direction);

            default:
                throw new System.Exception();
        }
    }

    private IEnumerator Nudge(Vector2 direction)
    {
        _isKnockable = false;
        var startPos = transform.position;
        float progress = 0;
        float start = Time.time;

        while (progress < Mathf.PI)
        {
            float time = Time.time - start;
            progress = time * nudgeSpeed;

            float x = (progress / Mathf.PI) * nudgeDistance;
            float y = Mathf.Sin(progress) * nudgeBounceHeight;

            transform.position = startPos + new Vector3(x, y) * direction.x;
            yield return new WaitForEndOfFrame();
        }

        transform.position = new Vector3(transform.position.x, startPos.y);
        _isKnockable = true;
    }

    private IEnumerator Wobble(Vector2 direction)
    {
        var startRotation = transform.rotation;
        _isKnockable = false;

        float amplitude = wobbleAmplitude;
        float start = Time.time;
        float angle = 0f;

        float threshold = 0.1f;
        while (amplitude > threshold)
        {
            float time = Time.time - start;

            angle = Mathf.Sin(time * wobbleSpeed) * amplitude * -direction.x;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            amplitude *= (1f - damping);
            yield return new WaitForEndOfFrame();
        }

        transform.rotation = startRotation;
        _isKnockable = true;
    }
}
