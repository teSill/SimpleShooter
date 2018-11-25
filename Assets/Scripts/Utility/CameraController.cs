using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform player;
    public Transform background;

    private float _playerX;

    private void Start() {
        _playerX = player.transform.position.x;
    }


    void Update () {
        FollowPlayer();
    }

    private void FollowPlayer() {
        Vector3 position = transform.position;
        position.x = player.transform.position.x - _playerX;
        transform.position = position;
    }

    private void MoveBackground() {
        Vector3 position = background.transform.position;
        position.x = transform.position.x;
        background.transform.position = transform.position;
    }
}
