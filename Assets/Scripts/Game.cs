using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    public GameObject soup;
    public GameObject noodle;
    public GameObject interactableObjectsParent;
    public GameObject story;
    public GameObject ui;

    public FloppyArm Storage { get; private set; }
    public Narrative Narrative { get; private set; }
    public NarrativeUI UI { get; private set; }

    void Awake()
    {
        if (Instance != null)
            throw new System.Exception();

        Instance = this;

        Storage = soup.GetComponentInChildren<FloppyArm>();
        Narrative = story.GetComponent<Narrative>();
        UI = ui.GetComponent<NarrativeUI>();
    }
}
