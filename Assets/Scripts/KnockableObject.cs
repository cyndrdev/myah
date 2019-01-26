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

    private SoundEngine _soundEngine;

    private bool _isKnockable = true;

    private void Start()
    {
        _soundEngine = GameObject.FindGameObjectWithTag(GameConstants.Persistent).GetComponentInChildren<SoundEngine>();
        if (_soundEngine == null)
            throw new System.Exception();
    }

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
        _isKnockable = false;
        switch (Random.Range(0, 2))
        {
            case 0:
                yield return Nudge(direction);
                break;

            case 1:
                yield return Wobble(direction);
                break;

            default:
                throw new System.Exception();
        }
        _isKnockable = true;
    }

    private IEnumerator Nudge(Vector2 direction)
    {
        _soundEngine.PlaySFX("bounce", true);
        var startPos = transform.position;
        float progress = 0;
        float start = Time.time;

        while (progress < Mathf.PI)
        {
            float time = Time.time - start;
            progress = time * nudgeSpeed;

            float x = (progress / Mathf.PI) * nudgeDistance;
            float y = Mathf.Sin(progress) * nudgeBounceHeight;

            transform.position = startPos + new Vector3(x * direction.x, y);
            yield return new WaitForEndOfFrame();
        }

        transform.position = new Vector3(transform.position.x, startPos.y);
    }

    private IEnumerator Wobble(Vector2 direction)
    {
        _soundEngine.PlaySFX("bonk", true);
        var startRotation = transform.rotation;

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
    }
}
