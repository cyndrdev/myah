using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooglyEyes : MonoBehaviour
{
    [SerializeField]
    private GameObject _leftEye;

    [SerializeField]
    private GameObject _rightEye;

    private Material _leftMaterial;
    private Material _rightMaterial;

    private float _leftRot;
    private float _rightRot;

    private Vector2 _leftPos;
    private Vector2 _rightPos;

    private Vector2 _starePos;

    void Start()
    {
        if (_leftEye == null || _rightEye == null)
            throw new System.Exception();

        _leftMaterial = _leftEye.GetComponent<Renderer>().material;
        _rightMaterial = _rightEye.GetComponent<Renderer>().material;
    }

    void Update()
    {
        _starePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        _leftPos = _leftEye.transform.position;
        _rightPos = _rightEye.transform.position;

        _leftRot = Vector2.Angle(Vector2.up, _starePos - _leftPos);
        _rightRot = Vector2.Angle(Vector2.up, _starePos - _rightPos);

        if (_leftPos.x > _starePos.x) _leftRot = (Mathf.PI * 2) - _leftRot;
        if (_rightPos.x > _starePos.x) _rightRot = (Mathf.PI * 2) - _rightRot;

        _leftMaterial.SetFloat("_SubRot", Mathf.Deg2Rad * _leftRot);
        _rightMaterial.SetFloat("_SubRot", Mathf.Deg2Rad * _rightRot);
    }
}
