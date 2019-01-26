using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Narrative : MonoBehaviour
{
    public int Position { get; private set; } = 0;

    public class StoryProgressionEvent : UnityEvent<int> { }
    public UnityEvent<int> StoryProgressed = new StoryProgressionEvent();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ProgressStory();
        }
    }

    public void ProgressStory()
    {
        Position++;
        StoryProgressed.Invoke(Position);
        //print("story progressed to position " + Position);
    }
}
