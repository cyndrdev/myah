using System.Linq;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    class DialogueLine
    {
        public int index;
        public string speaker;
        public string text;
    }

    public TextAsset script;
    public KeyCode advanceDialogue;

    private DialogueLine[] _lines;
    private Narrative _narrative;
    private int _lastIndex;
    private DialogueView _dialogueView;

    void Start()
    {
        _dialogueView = Game.Instance.UI.DialogueView;
        _narrative = Game.Instance.Narrative;

        var lines = script.text.Split('\n');
        print("parsed " + lines.Length + " lines of dialogue");
        _lines = new DialogueLine[lines.Length];

        for (int i = 0; i < _lines.Length; i++)
        {
            var parts = lines[i].Split('\t');
            if (parts.Length != 3)
                throw new System.Exception("can't parse line: " + lines[i]);

            _lines[i] = new DialogueLine()
            {
                index = int.Parse(parts[0]),
                speaker = parts[1],
                text = parts[2]
            };
        }
        _lastIndex = _lines.Select(l => l.index).Max();

        _narrative.StoryProgressed.AddListener(ShowLine);
    }

    private int _dialogueIndex;
    private void Update()
    {
        // don't continue if the next line doesn't exist
        if (_narrative.position != 0 && 
            GetLine(_narrative.position) == null)
            return;

        if (Input.GetKeyDown(advanceDialogue))
        {
            _narrative.ProgressStory();
        }
    }

    private string GetLine(int index)
    {
        return _lines.SingleOrDefault(l => l.index == index)?.text;
    }

    private void ShowLine(int index)
    {
        if (index > _lastIndex)
        {
            _dialogueView.Clear();
            print("no more dialogue :(");
            return;
        }

        var line = _lines.SingleOrDefault(l => l.index == index);
        if (line == null)
            // no dialogue for this index
            return;

        string message = string.Format("{0}: {1}", line.speaker, line.text);
        _dialogueView.ShowText(message);
    }
}
