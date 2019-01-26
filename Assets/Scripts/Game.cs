using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    public GameObject player;
    public GameObject interactableObjectsParent;
    public GameObject story;

    public FloppyArm Storage { get; private set; }
    public Narrative Narrative { get; private set; }

    void Awake()
    {
        if (Instance != null)
            throw new System.Exception();

        Instance = this;

        Storage = player.GetComponentInChildren<FloppyArm>();
        Narrative = story.GetComponent<Narrative>();
    }
}
