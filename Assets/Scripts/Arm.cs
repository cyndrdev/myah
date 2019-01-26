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

        _handRb = hand.GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        var points = GetBezierPoints();
        _line.positionCount = points.Length;
        _line.SetPositions(points);
    }

    private Vector3[] GetBezierPoints()
    {
        var shoulderPos = shoulder.transform.position;
        var handPos = hand.transform.position;
        var avg = (shoulderPos + handPos) / 2f;
        var l = (handPos - shoulderPos).magnitude;

        float armDroop = 0.7f;

        var minY = avg.y - l * armDroop;
        var min = new Vector3(avg.x, minY);

        var pos1 = handPos.x < shoulderPos.x ? handPos : shoulderPos;
        var pos2 = min;
        var pos3 = handPos.x < shoulderPos.x ? shoulderPos : handPos;

        var seg1 = pos2 - pos1;
        var seg2 = pos3 - pos2;

        int steps = 10;
        Vector3[] results = new Vector3[steps + 1];
        float stepSize = 1f / (steps - 1f);
        for (int i = 0; i <= steps; i++)
        {
            float t = i * stepSize;

            var startPos = Vector3.Lerp(pos1, pos2, t);
            var endPos = Vector3.Lerp(pos2, pos3, t);
            var point = Vector3.Lerp(startPos, endPos, t);

            results[i] = point;
        }

        return results;
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
