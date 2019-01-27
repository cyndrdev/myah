using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestView : MonoBehaviour
{
    public Text title;
    public float questItemSpacing;

    [Header("Quest Items")]
    public Transform questItemsParent;
    public GameObject questItemTemplate;

    private GameObject[] _questItems;

    private void Awake()
    {
        questItemTemplate.SetActive(false);
        _questItems = new GameObject[0];
    }

    public void SetTitle(string title)
    {
        this.title.text = title;
    }

    public void SetQuestItems(string[] items)
    {
        for (int i = 0; i < _questItems.Length; i++)
        {
            Destroy(_questItems[i]);
        }

        _questItems = new GameObject[items.Length];
        for (int i = 0; i < items.Length; i++)
        {
            _questItems[i] = MakeQuestItem(items[i], questItemSpacing * i);
            _questItems[i].SetActive(true);
        }
    }

    private GameObject MakeQuestItem(string text, float offset)
    {
        var questItem = Instantiate(questItemTemplate, questItemsParent);
        questItem.transform.Translate(Vector2.down * offset);

        var t = questItem.GetComponentInChildren<Text>();
        t.text = text;

        return questItem;
    }
}
