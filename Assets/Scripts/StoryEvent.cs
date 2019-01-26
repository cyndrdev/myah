using UnityEngine;
using UnityEngine.Events;

public class StoryEvent : MonoBehaviour
{
    public int triggerAt;
    public string message;

    public UnityEvent Triggered = new UnityEvent();

    void Start()
    {
        Game.Instance.Narrative.StoryProgressed.AddListener(CheckForTrigger);
    }

    private void CheckForTrigger(int position)
    {
        if (position == triggerAt)
        {
            Triggered.Invoke();
            print("event " + triggerAt + ": " + message);
        }
    }
}
