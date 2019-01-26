using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpZone : MonoBehaviour
{
    [SerializeField]
    private string _targetScene;
    [SerializeField]
    private Vector2 _targetPos;

    void OnTriggerEnter2D(Collider2D collider)
    {
        // check our collider is the player
        var player = collider.GetComponent<PlayerController>();
        if (player == null)
            return;

        DoSceneLoad();
    }

    void DoSceneLoad()
    {
        // load our scene
        SceneManager.LoadScene(_targetScene, LoadSceneMode.Single);
        var _player = GameObject.FindGameObjectWithTag("Player");
        _player.transform.position = _targetPos;
    }
}
