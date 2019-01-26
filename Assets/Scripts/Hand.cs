using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public float interactionDistance;
    public KeyCode interact;

    private Rigidbody2D _rb;
    private HandInteraction _interaction;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _interaction = GetComponentInChildren<HandInteraction>();
    }

    void Update()
    {
        if (Input.GetKeyDown(interact))
        {
            _interaction.Interact();

        }
    }
}
