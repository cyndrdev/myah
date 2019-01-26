using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class MakeTea : MonoBehaviour
{
    private enum Stage
    {
        Inactive,
        GatherIngredients,
        MakeTea,
        GiveTea,
        Completed
    }
    private Stage _stage = Stage.Inactive;

    public string title;
    public int start;

    public GameObject[] ingredients;
    public float proximity;
    public float noodleProximity;
    public GameObject hammer;
    public GameObject tea;

    public UnityEvent Completed = new UnityEvent();

    private QuestView _questView;
    private Narrative _narrative;
    private GameObject _tea;

    void Start()
    {
        _questView = Game.Instance.UI.QuestView;

        _narrative = Game.Instance.Narrative;
        _narrative.StoryProgressed.AddListener(OnProgress);
    }

    public void InteractFalseKettle()
    {
        print("oh, no! we can't use *that* kettle. it's out of date!");
    }

    private void OnProgress(int index)
    {
        if (index == start)
        {
            _stage = Stage.GatherIngredients;
            Game.Instance.UI.ShowQuestView();
            _questView.SetTitle(title);

            ShowIngredients();
        }
    }

    private void ShowIngredients()
    {
        var questItems = new[]
                    {
                "Get kettle",
                "Get milk",
                "Get teabags",
                "Get mugs"
            };
        _questView.SetQuestItems(questItems);
    }

    private void ShowHammer()
    {
        var questItems = new[]
        {
            "Get the right tool for the job"
        };
        _questView.SetQuestItems(questItems);
    }

    private void ShowFinish()
    {
        var questItems = new[] { "Take the tea to Noodle" };
        _questView.SetQuestItems(questItems);
    }

    void LateUpdate()
    {
        UpdateProgress();
    }

    private void UpdateProgress()
    {
        switch (_stage)
        {
            case Stage.Inactive:
            case Stage.Completed:
                return;

            case Stage.GatherIngredients:
                CheckReadyToMakeTea();
                break;
            case Stage.MakeTea:
                TryMakeTea();
                break;
            case Stage.GiveTea:
                TryGiveTea();
                break;
            default:
                throw new System.Exception();
        }
    }

    private void TryMakeTea()
    {
        if (!IngredientsTogether())
        {
            _stage = Stage.GatherIngredients;
            //print("no longer ready :(");
            ShowIngredients();
            return;
        }

        var avgPos = GetAverageIngredientsPosition();
        if (Vector3.Distance(hammer.transform.position, avgPos) < proximity)
        {
            foreach (var ingredient in ingredients)
            {
                Destroy(ingredient);
            }

            _tea = Instantiate(tea, avgPos, Quaternion.identity, null);
            _stage = Stage.GiveTea;
            ShowFinish();
        }
    }

    private bool IngredientsTogether()
    {
        var avgPos = GetAverageIngredientsPosition();
        return ingredients.All(i =>
        {
            return Vector3.Distance(i.transform.position, avgPos) < proximity;
        });
    }

    private void CheckReadyToMakeTea()
    {
        if (IngredientsTogether())
        {
            _stage = Stage.MakeTea;
            //print("ready!");
            ShowHammer();
        }
    }

    private void TryGiveTea()
    {
        if (_tea == null)
            throw new System.Exception();

        var noodle = Game.Instance.noodle;

        if (Vector3.Distance(_tea.transform.position, noodle.transform.position) < proximity)
        {
            var hand = noodle.GetComponent<Noodle>().hand;
            _tea.transform.SetParent(hand);
            _tea.transform.localPosition = Vector3.zero;
            _tea.GetComponent<Rigidbody2D>().simulated = false;

            _stage = Stage.Completed;
            Game.Instance.UI.HideQuestView();
        }
    }

    private Vector3 GetAverageIngredientsPosition()
    {
        Vector3 avgPos = Vector3.zero;
        for (int i = 0; i < ingredients.Length; i++)
        {
            avgPos += ingredients[i].transform.position;
        }
        return avgPos / ingredients.Length;
    }
}
