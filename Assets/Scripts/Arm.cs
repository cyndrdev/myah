using UnityEngine;

public class Arm : MonoBehaviour
{
    public GameObject shoulder;
    public GameObject hand;
    public float length;
    [Range(0, 1)]
    public float moveSpeed;

    private LineRenderer _line;
    private Rigidbody2D _handRb;

    void Start()
    {
        _line = GetComponent<LineRenderer>();
        _line.positionCount = 2;

        _handRb = hand.GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        _line.SetPositions(new[]
        {
            shoulder.transform.position,
            hand.transform.position
        });
    }

    private void FixedUpdate()
    {
        Vector3 targetPos;

        var mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var originPos = (Vector2)shoulder.transform.position;
        var toMouse = mousePos - originPos;
        var dir = toMouse.normalized;

        if (toMouse.magnitude < length)
        {
            targetPos = mousePos;
        }
        else
        {
            targetPos = originPos + dir * length;
        }

        hand.transform.position = Vector3.Lerp(
            hand.transform.position, 
            targetPos, 
            moveSpeed);
    }
}
