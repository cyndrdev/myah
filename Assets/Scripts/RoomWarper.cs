using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomWarper : MonoBehaviour
{
    [SerializeField]
    float _xPosition;
    [SerializeField]
    int _floor;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // only continue if we've triggered the player
        var player = collider.gameObject.GetComponent<PlayerController>();
        if (player == null) return;

        float relY = collider.transform.position.y % 100;
        collider.transform.position = new Vector2(_xPosition, (_floor * 100.0f) + relY);

        Camera.main.gameObject.GetComponent<CinematicCamera>().Snap();
    }
}
