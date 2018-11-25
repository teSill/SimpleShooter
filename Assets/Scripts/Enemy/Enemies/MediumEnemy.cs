using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumEnemy : Enemy {

    public GameObject laserPrefab;

    protected override void Start() {
        base.Start();
        StartCoroutine(ShootingSequence());
        shootingInterval = 1f;
        movementSpeed = 6.5f;
        health = 3;
    }

    protected override void Shoot() {
        base.Shoot();
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 270)));
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed, 0);
        Destroy(laser, Constants.projectileLifetime);
    }

    private IEnumerator ShootingSequence() {
        while(true) {
            if (inCombat)
                Shoot();
            yield return new WaitForSeconds(shootingInterval);
        }
    }
}
