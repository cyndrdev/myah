using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeFromBlack : MonoBehaviour
{
    [SerializeField]
    private float _fadeTime;
    private float _fadeAmt;
    private Image _image;

    private int _endPoint;
    private Narrative _narrative;
    private bool _fadingOut = false;

    void Start()
    {
        _image = gameObject.GetComponent<Image>();
        _narrative = Game.Instance.Narrative;
        _endPoint = _narrative.end;
        Debug.Log(_endPoint);
        _fadeAmt = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_fadeTime <= 0f) return;

        if (_narrative.position == _endPoint)
        {
            _fadingOut = true;
            _fadeAmt = 0f;
        }

        if (_fadeAmt >= 1f) return;

        Color _color = new Color(0f, 0f, 0f, _fadingOut ? _fadeAmt : 1f - _fadeAmt);
        _image.color = _color;

        if (_fadeAmt == 1f)
            gameObject.SetActive(false);

        _fadeAmt = Mathf.Clamp(_fadeAmt + (Time.deltaTime / _fadeTime), 0f, 1f);
    }
}
