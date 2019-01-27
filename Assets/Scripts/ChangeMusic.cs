using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    private enum Stage
    {
        Inactive,
        FindRecord,
        PlayRecord,
        Completed
    }
    private Stage _stage = Stage.Inactive;

    public string title;
    public int start;

    public GameObject record;
    public GameObject recordPlayer;
    public float proximity;

    private QuestView _questView;
    private DialogueView _dialogueView;
    private Narrative _narrative;

    void Start()
    {
        _questView = Game.Instance.UI.QuestView;
        _dialogueView = Game.Instance.UI.DialogueView;

        _narrative = Game.Instance.Narrative;
        _narrative.StoryProgressed.AddListener(OnProgress);
    }

    private void LateUpdate()
    {
        switch (_stage)
        {
            case Stage.Inactive:
            case Stage.Completed:
            case Stage.FindRecord:
                return;

            case Stage.PlayRecord:
                TryPlayRecord();
                break;

            default:
                throw new System.Exception();
        }
    }

    private void OnProgress(int index)
    {
        if (index == start)
        {
            _stage = Stage.FindRecord;

            Game.Instance.UI.ShowQuestView();
            _questView.SetTitle(title);
            _questView.SetQuestItems(new[] { "Find record" });
        }
    }

    // run when the player gets the record from the frame   
    public void GetRecord()
    {
        _stage = Stage.PlayRecord;
        _questView.SetQuestItems(new[] { "Play record" });
    }

    private void TryPlayRecord()
    {
        if (Vector3.Distance(
            record.transform.position,
            recordPlayer.transform.position) < proximity)
        {
            print("playing record!");

            Game.Instance.UI.HideQuestView();
            
            _stage = Stage.Completed;
            _narrative.ProgressStory();

            Destroy(record);
        }
    }
}
