using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GiveBook : MonoBehaviour
{
    private enum Stage
    {
        Inactive,
        FindBook,
        GiveBook,
        Completed
    }
    private Stage _stage = Stage.Inactive;

    public string title;
    public int start;

    public GameObject[] books;
    public float proximity;

    private QuestView _questView;
    private DialogueView _dialogueView;
    private Narrative _narrative;

    private FloppyArm _storage;
    private GameObject _book;

    void Start()
    {
        if (!books.Any())
            throw new System.Exception();

        _questView = Game.Instance.UI.QuestView;
        _dialogueView = Game.Instance.UI.DialogueView;

        _narrative = Game.Instance.Narrative;
        _narrative.StoryProgressed.AddListener(OnProgress);

        _storage = Game.Instance.Storage;
    }

    private void LateUpdate()
    {
        if (books.Contains(_storage.HeldObject))
        {
            _book = _storage.HeldObject;
        }
        else
        {
            _book = null;
        }

        if (_stage == Stage.GiveBook)
        {
            TryGiveBook();
        }
    }

    private void OnProgress(int index)
    {
        if (index == start)
        {
            _stage = Stage.FindBook;

            _narrative.ProgressStory();

            Game.Instance.UI.ShowQuestView();
            _questView.SetTitle(title);
            _questView.SetQuestItems(new[] { "Find book" });
        }
    }

    public void GetBook()
    {
        _stage = Stage.GiveBook;
        _questView.SetQuestItems(new[] { "Give Noodle the book" });
    }

    private void TryGiveBook()
    {
        var closestBook = books
            .OrderBy(b =>
                Vector3.Distance(
                    b.transform.position,
                    Game.Instance.noodle.transform.position))
            .First();

        if (Vector3.Distance(
            closestBook.transform.position, 
            Game.Instance.noodle.transform.position) < proximity)
        {
            print("gave noodle the book!");

            Game.Instance.UI.HideQuestView();
            _stage = Stage.Completed;
            _narrative.ProgressStory();

            Destroy(closestBook);
        }
    }
}
