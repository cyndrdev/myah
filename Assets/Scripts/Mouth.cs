using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : MonoBehaviour
{
    public Sprite smile;
    public Sprite surprise;

    private SpriteRenderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();

        //StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        int i = 0;
        while (true)
        {
            if (i == 3) i = 0;

            switch (i)
            {
                case 0:
                    Smile();
                    break;
                case 1:
                    Frown();
                    break;
                case 2:
                    Surprise();
                    break;
                default:
                    throw new System.Exception();
            }

            yield return new WaitForSeconds(1);
            i++;
        }
    }

    public void Smile()
    {
        _renderer.sprite = smile;
        _renderer.flipY = false;    
    }

    public void Frown()
    {
        _renderer.sprite = smile;
        _renderer.flipY = true;    
    }

    public void Surprise()
    {
        _renderer.sprite = surprise;
    }
}
