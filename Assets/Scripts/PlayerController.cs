using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.Translate(GetInput() * speed * Time.deltaTime);
    }

    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxis(GameConstants.Horizontal), 0);
    }
}
