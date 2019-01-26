using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FloppyArm : MonoBehaviour
{
    public Transform[] joints;
    public Transform hand;

    private LineRenderer _line;

    public GameObject HeldObject { get; private set; }

    void Start()
    {
        _line = GetComponent<LineRenderer>();
        _line.positionCount = joints.Length;
    }

    void LateUpdate()
    {
        DrawArm();

        if (HeldObject == null)
            return;

        HeldObject.transform.localPosition = Vector3.zero;
        HeldObject.transform.rotation = hand.rotation;
    }

    private void DrawArm()
    {
        Vector3[] positions = new Vector3[joints.Length];
        for (int i = 0; i < joints.Length; i++)
        {
            positions[i] = joints[i].position;
        }

        _line.SetPositions(positions);
    }

    public void StoreObject(GameObject obj)
    {
        if (HeldObject != null)
            throw new System.Exception();

        HeldObject = obj;
        obj.transform.SetParent(hand);
        obj.transform.localPosition = Vector3.zero;

        var rb = obj.GetComponent<Rigidbody2D>();
        rb.simulated = false;
    }

    public GameObject RemoveObject()
    {
        if (HeldObject == null)
            throw new System.Exception();

        HeldObject.transform.SetParent(null);
        var o = HeldObject;
        HeldObject = null;

        var rb = o.GetComponent<Rigidbody2D>();
        rb.simulated = true;

        return o;
    }
}
