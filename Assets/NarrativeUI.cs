using System;
using System.Linq;
using UnityEngine;

public class NarrativeUI : MonoBehaviour
{
    public GameObject questView;
    public GameObject dialogueView;

    public QuestView QuestView { get; private set; }
    public DialogueView DialogueView { get; private set; }

    private void Awake()
    {
        QuestView = questView.GetComponent<QuestView>();
        DialogueView = dialogueView.GetComponent<DialogueView>();
    }

    private void Start()
    {
        HideQuestView();
    }

    public void ShowQuestView()
    {
        questView.SetActive(true);
    }

    public void HideQuestView()
    {
        questView.SetActive(false);
    }
}
