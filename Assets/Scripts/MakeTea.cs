using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MakeTea : MonoBehaviour
{
    public GameObject[] ingredients;
    public float proximity;
    public GameObject hammer;

    private bool _canComplete;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        Vector3 avgPos = Vector3.zero;
        for (int i = 0; i < ingredients.Length; i++)
        {
            avgPos += ingredients[i].transform.position;
        }
        avgPos /= ingredients.Length;

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
}
