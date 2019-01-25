using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockableObject : MonoBehaviour
{
    public float wobbleAmplitude;
    public float wobbles;

    private float _angle;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            print("body knock");
            StartCoroutine(Wobble());
        }

        var hand = collision.GetComponent<Hand>();
        if (hand != null)
        {
            print("hand knock");
            StartCoroutine(Wobble());
        }
    }

    private IEnumerator Wobble()
    {
        print("wobble");
        yield return null;
    }
}
