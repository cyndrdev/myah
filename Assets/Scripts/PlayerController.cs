using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float force;

    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.AddForce(GetInput() * force);
    }

    private Vector2 GetInput()
    {
        return new Vector2(
            Input.GetAxis(GameConstants.Horizontal), 
            Input.GetAxis(GameConstants.Vertical));
    }
}
