using System.Linq;
using UnityEngine;

public class MakeTea : MonoBehaviour
{
    public string title;
    public int start;

    public GameObject[] ingredients;
    public string[] questItems;
    public float proximity;
    public GameObject hammer;
    public GameObject tea;

    private bool _started;
    private bool _canComplete;
    private bool _completed;
    private QuestView _questView;
    private Narrative _narrative;

    void Start()
    {
        _questView = Game.Instance.UI.QuestView;

        _narrative = Game.Instance.Narrative;
        _narrative.StoryProgressed.AddListener(OnProgress);
    }

    private void OnProgress(int index)
    {
        if (index == start)
        {
            _started = true;
            Game.Instance.UI.ShowQuestView();
            _questView.SetTitle(title);
            _questView.SetQuestItems(questItems);
        }
    }

    void LateUpdate()
    {
        if (!_started)
            return;

        UpdateProgress();
    }

    private void UpdateProgress()
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
            Game.Instance.UI.HideQuestView();
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
