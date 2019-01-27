using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public string title;

    private string[] _titles = new[] 
    {
        "Waiting for Unity",
        "The Great Hatsby",
        "Pride and Predicates",
        "The Captcha in the Rye",
        "Of Mice and Keyboards",
        "The Lord of the Pings",
        "Thirteen Thirty-Seven",
        "Moby Click",
        "The Lord of the Files",
        "'Code That Compiles On The First Try' and other fairytales"
    };

    void Start()
    {
        title = _titles[Random.Range(0, _titles.Length)];
        GetComponent<Interactable>().OnInteract.AddListener(ReadTitle);
    }

    private void ReadTitle()
    {
        Game.Instance.UI.DialogueView.ShowText("picked up " + title);
    }
}
