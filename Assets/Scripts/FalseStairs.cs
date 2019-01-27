using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseStairs : MonoBehaviour
{
    private Collider2D _collider;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    public void Fall()
    {
        _collider.enabled = false;
        Destroy(gameObject, 2.5f);
    }
}
