using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockableObject : MonoBehaviour
{
    public float wobbleSpeed;
    public float wobbleAmplitude;
    [Range(0, 1)]
    public float damping;

    private bool _isKnockable = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        
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
            StartCoroutine(Wobble(direction));
        }

        var hand = collider.GetComponent<Hand>();
        if (hand != null)
        {
            print("hand knock");
            StartCoroutine(Wobble(direction));
        }
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
