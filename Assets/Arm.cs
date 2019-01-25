using UnityEngine;

public class Arm : MonoBehaviour
{
    public GameObject shoulder;
    public GameObject hand;

    private LineRenderer _line;

    void Start()
    {
        _line = GetComponent<LineRenderer>();
        _line.positionCount = 2;
    }

    void Update()
    {
        _line.SetPositions(new[]
        {
            shoulder.transform.position,
            hand.transform.position
        });
    }
}
