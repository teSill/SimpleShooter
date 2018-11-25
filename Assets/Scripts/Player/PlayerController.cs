using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject laserPrefab;
    
    [SerializeField]
    private float _movementSpeed = 20f;

    [SerializeField]
    private float _horizontalSpeed = Constants.horizontalSpeed;

    public float HorizontalSpeed { get { return _horizontalSpeed; } }

    [SerializeField]
    private float _projectileSpeed = 20f;

    // Used for getting the camera view position to restrict player movement in SetMovementRestriction()
    private float _yMin;
    private float _yMax;

	// Use this for initialization
	void Start () {
        SetMovementRestriction();
    }

    // Update is called once per frame
    void Update () {
		Move();
        Fire();
	}

    private void Move() {
        float movementSpeed = GetComponent<PowerUp>().movementBoost ? (_movementSpeed * 2): _movementSpeed;
        float direction = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        float moveY = Mathf.Clamp(transform.position.y + direction, _yMin, _yMax);
        float moveX = transform.position.x + _horizontalSpeed * Time.deltaTime;

        transform.position = new Vector2(moveX, moveY);
        print(movementSpeed);
    }

    private void Fire() {
        if (Input.GetMouseButtonDown(0)) {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 270)));
            laser.transform.localScale = GetComponent<PowerUp>().projectileBoost ? laserPrefab.transform.localScale * 3 : laserPrefab.transform.localScale;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(_projectileSpeed, 0);
            Destroy(laser, Constants.projectileLifetime);
        }
    }

    private void SetMovementRestriction() {
        Camera gameCamera = Camera.main;
        _yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + Constants.paddingMin;
        _yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - Constants.paddingMax;
    }
}
