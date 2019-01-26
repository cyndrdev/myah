using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FloppyArm : MonoBehaviour
{
    public Transform[] joints;

    private LineRenderer _line;

    void Start()
    {
        _line = GetComponent<LineRenderer>();
        _line.positionCount = joints.Length;
    }

    void LateUpdate()
    {
        Vector3[] positions = new Vector3[joints.Length];
        for (int i = 0; i < joints.Length; i++)
        {
            positions[i] = joints[i].position;
        }

        _line.SetPositions(positions);
    }
}
