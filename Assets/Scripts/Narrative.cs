using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Narrative : MonoBehaviour
{
    public int position;
    public int end;

    public class StoryProgressionEvent : UnityEvent<int> { }
    public UnityEvent<int> StoryProgressed = new StoryProgressionEvent();

    public void ProgressStory()
    {
        position++;
        StoryProgressed.Invoke(position);
        //print("story progressed to position " + Position);

        if (position == end)
        {
            Game.Instance.UI.GetComponentInChildren<FadeToBlack>().Fade();
        }
    }
}
