using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    private SpriteRenderer _sprite;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    public void UpdateRenderLayer()
    {
        _sprite.sortingLayerName = GameConstants.Pickups;
        _sprite.sortingOrder = 1;
    }
}
