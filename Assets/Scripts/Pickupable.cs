using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Pickupable : MonoBehaviour
{
    private FloppyArm _storage;

    void Start()
    {
        _storage = Game.Instance.Storage;
    }

    public void Pickup()
    {
        _storage.StoreObject(gameObject);
    }

    public void Drop()
    {
        _storage.RemoveObject();
        transform.SetParent(Game.Instance.interactableObjectsParent.transform);
    }
}
