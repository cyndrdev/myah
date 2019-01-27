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

    void Start()
    {
        _image = gameObject.GetComponent<Image>();
        _fadeAmt = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_fadeTime <= 0f) return; 

        Color _color = new Color(0f, 0f, 0f, 1f - _fadeAmt);
        _image.color = _color;

        if (_fadeAmt == 1f)
            gameObject.SetActive(false);

        _fadeAmt = Mathf.Clamp(_fadeAmt + (Time.deltaTime / _fadeTime), 0f, 1f);
    }
}
