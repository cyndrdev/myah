using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public float armLength;
    [Range(0, 1)]
    public float moveSpeed;

    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 targetPos;

        var mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var originPos = (Vector2)transform.parent.position;
        var toMouse = mousePos - originPos;
        var dir = toMouse.normalized;

        if (toMouse.magnitude < armLength)
        {
            targetPos = mousePos;
        }
        else
        {
            targetPos = originPos + dir * armLength;
        }

        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed);
    }
}
