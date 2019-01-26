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

    [Header("Legs")]
    public Transform leftLeg;
    public Transform rightLeg;

    public float stepSize;
    public float stepsPerSecond;

    private float _leftFullPos;
    private float _rightFullPos;

    private Rigidbody2D _rb;
    private bool _facingRight;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _leftFullPos = leftLeg.localPosition.y;
        _rightFullPos = rightLeg.localPosition.y;
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

        // make legs walk
        float stepSizeMod = Mathf.SmoothStep(0f, stepSize, Mathf.Abs(_rb.velocity.x) / 5f);
        float legAmt = (1f + Mathf.Sin(Time.time * 2 * Mathf.PI * stepsPerSecond)) / 2f;
        float leftLegHeight = Mathf.Lerp(_leftFullPos, _leftFullPos + stepSizeMod, legAmt);
        float rightLegHeight = Mathf.Lerp(_rightFullPos, _rightFullPos + stepSizeMod, 1f - legAmt);

        leftLeg.localPosition = new Vector3(leftLeg.localPosition.x, leftLegHeight);
        rightLeg.localPosition = new Vector3(rightLeg.localPosition.x, rightLegHeight);
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
