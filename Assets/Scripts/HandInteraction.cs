using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandInteraction : MonoBehaviour
{
    private List<Interactable> _interactableObjects = new List<Interactable>();
    private FloppyArm _storage;

    private void Start()
    {
        _storage = Game.Instance.Storage;
    }

    public void Interact()
    {
        if (_storage.HeldObject != null)
        {
            var o = _storage.HeldObject;
            var pickup = _storage.HeldObject.GetComponent<Pickupable>();
            pickup.Drop();

            o.transform.position = transform.position;
            return;
        }

        if (!_interactableObjects.Any())
            return;

        _interactableObjects.First().Interact();
   }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var interactable = other.GetComponent<Interactable>();
        if (interactable == null)
            return;

        if (_interactableObjects.Contains(interactable))
            return;

        _interactableObjects.Add(interactable);
        //print("added " + interactable.ToString());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var interactable = other.GetComponent<Interactable>();
        if (interactable == null)
            return;

        if (!_interactableObjects.Contains(interactable))
            return;

        _interactableObjects.Remove(interactable);
        //print("removed " + interactable.ToString());
    }
}
