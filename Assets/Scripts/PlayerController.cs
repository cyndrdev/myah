using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveForce = 100;

    [Header("Arms")]
    public Transform leftSocket;
    public Transform rightSocket;
    public Transform interactionArm;
    public Transform floppyArm;

    private Rigidbody2D _rb;
    private bool _facingRight;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.AddForce(GetInput() * moveForce);
    }

    private void LateUpdate()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // swap arms to face the right direction
        if (mousePos.x > transform.position.x && !_facingRight) // mouse to the right of player
        {
            SetArmSocket(interactionArm, leftSocket);
            SetArmSocket(floppyArm, rightSocket);
            _facingRight = true;
        }
        else if (mousePos.x <= transform.position.x && _facingRight)
        {
            SetArmSocket(interactionArm, rightSocket);
            SetArmSocket(floppyArm, leftSocket);
            _facingRight = false;
        }
   }

    private void SetArmSocket(Transform arm, Transform socket)
    {
        arm.SetParent(socket);
        arm.localPosition = Vector3.zero;
    }

    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxis(GameConstants.Horizontal), 0);
    }
}
