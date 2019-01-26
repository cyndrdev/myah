using UnityEngine;

public class RoomWarper : MonoBehaviour
{
    [SerializeField]
    float _xPosition;
    [SerializeField]
    int _floor;
    public bool locked;
    public GameObject block;

    private Renderer _graphics;

    private void Start()
    {
        _graphics = GetComponentInChildren<Renderer>();

        if (locked)
        {
            _graphics.enabled = false;
        }
        else
        {
            block.SetActive(false);
        }
    }

    public void Unlock()
    {
        _graphics.enabled = true;
        locked = false;
        block.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (locked)
            return;

        // only continue if we've triggered the player
        var player = collider.gameObject.GetComponent<PlayerController>();
        if (player == null) return;

        float relY = collider.transform.position.y % 100;
        collider.transform.position = new Vector2(_xPosition, (_floor * 100.0f) + relY);

        Camera.main.gameObject.GetComponent<CinematicCamera>().Snap();
    }
}
