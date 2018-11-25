using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour {

    public Transform mainCamera;

    [SerializeField]
    private float _speed = 0.5f;
	
	// Update is called once per frame
	void Update () {
		Vector2 offset = new Vector2(_speed * Time.time, 0);
        GetComponent<Renderer>().material.mainTextureOffset = offset;

        // Move along with the player and camera
        transform.position = new Vector2(mainCamera.transform.position.x, mainCamera.transform.position.y);
	}
}
