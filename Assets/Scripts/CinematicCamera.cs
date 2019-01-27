using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject _target;

    [SerializeField]
    private float _positionSpeed = 0.1f;
    [SerializeField]
    private float _wobbleRefresh = 7.5f;
    private float _wobbleProgress = 0.0f;
    [SerializeField]
    private float _wobbleAmount = 0.25f;

    [SerializeField]
    private Vector3 _pan;

    [SerializeField]
    private float _leftClamp;
    [SerializeField]
    private float _rightClamp;

    private float _zAmount;

    private Vector3 _targetPosition;
    private Vector3 _currentPosition;

    private Vector3 _oldWobble;
    private Vector3 _targetWobble;

    void UpdateWobble()
    {
        _wobbleProgress = 0.0f;
        _oldWobble = _targetWobble;
        _targetWobble = new Vector3(Random.Range(0, _wobbleAmount), Random.Range(0, _wobbleAmount));
    }

    void Start()
    {
        if (_target == null)
            throw new System.Exception();

        _zAmount = transform.position.z;
        UpdateWobble();
        Snap();
    }

    void LateUpdate()
    {
        _targetPosition = _target.transform.position;
        _targetPosition.z = _zAmount;
        _targetPosition.x = Mathf.Clamp(_targetPosition.x, _leftClamp, _rightClamp);

        _wobbleProgress += Time.deltaTime;
        if (_wobbleProgress > _wobbleRefresh)
        {
            UpdateWobble();
        }

        // lerp all the things!
        _currentPosition = Vector3.Lerp(_currentPosition, _targetPosition, _positionSpeed);
        Vector3 _currentWobble = Vector3.Lerp(_oldWobble, _targetWobble, Generics.EaseInOutQuad(_wobbleProgress / _wobbleRefresh));

        transform.position = _currentPosition + _currentWobble + _pan;
    }

    public void Snap()
    {
        _targetPosition = _currentPosition = _target.transform.position;
    }
}
