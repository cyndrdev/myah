using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    private GameObject player;
    public GameObject interactableObjectsParent;

    public FloppyArm Storage { get; private set; }

    void Awake()
    {
        if (Instance != null)
            throw new System.Exception();

        Instance = this;
        player = GameObject.FindGameObjectWithTag(GameConstants.Player);

        Storage = player.GetComponentInChildren<FloppyArm>();
    }
}
