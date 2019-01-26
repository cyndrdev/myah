using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MakeTea : MonoBehaviour
{
    public GameObject[] ingredients;
    public float proximity;
    public GameObject hammer;
    public GameObject tea;

    private bool _canComplete;
    private bool _completed;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        if (_completed)
            return;

        CheckReady();

        if (!_canComplete)
            return;

        var avgPos = GetAveragePosition();
        if (Vector3.Distance(hammer.transform.position, avgPos) < proximity)
        {
            foreach (var ingredient in ingredients)
            {
                Destroy(ingredient);
            }

            Instantiate(tea, avgPos, Quaternion.identity, null);
            _completed = true;
        }
    }

    private void CheckReady()
    {
        var avgPos = GetAveragePosition();

        bool ready = ingredients.All(i =>
        {
            return Vector3.Distance(i.transform.position, avgPos) < proximity;
        });

        if (ready && !_canComplete)
        {
            _canComplete = true;
            print("ready!");
        }
        else if (!ready && _canComplete)
        {
            _canComplete = false;
            print("no longer ready :(");
        }
    }

    private Vector3 GetAveragePosition()
    {
        Vector3 avgPos = Vector3.zero;
        for (int i = 0; i < ingredients.Length; i++)
        {
            avgPos += ingredients[i].transform.position;
        }
        return avgPos / ingredients.Length;
    }
}
