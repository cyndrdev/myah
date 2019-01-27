using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueView : MonoBehaviour
{
    public float timeout = 10;

    public bool IsShowingText => !string.IsNullOrWhiteSpace(text.text);

    public GameObject _background;

    // i heard you like text fam
    public Text text;
    private void Awake() => Clear();
    public void Clear() => Text("");
    private void Text(string text) => this.text.text = text;

    private float _timeout;
    private bool _timingOut;

    public void ShowText(string text)
    {
        _timeout = timeout;
        _background.SetActive(true);

        Text(text);
        StartCoroutine(ClearTimeout());
    }

    private IEnumerator ClearTimeout()
    {
        if (_timingOut)
            yield break;

        _timingOut = true;

        while (_timeout > 0)
        {
            yield return new WaitForEndOfFrame();
            _timeout -= Time.deltaTime;
        }

        Clear();
        _background.SetActive(false);
        _timingOut = false;
    }
}
