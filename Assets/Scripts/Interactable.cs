using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Interactable : MonoBehaviour
{
    public bool active;
    public UnityEvent OnInteract = new UnityEvent();

    private Collider2D _collider;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();

        _rb.simulated = active;
        _collider.enabled = active;
    }

    public void Interact()
    {
        if (!active)
            return;

        OnInteract.Invoke();
    }

    public void Activate()
    {
        if (!active)
        {
            active = true;
            _rb.simulated = active;
            _collider.enabled = active;
        }
    }
}
