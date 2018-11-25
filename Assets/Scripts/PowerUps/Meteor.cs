using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour {

    private int _health = 6;

    public GameObject powerUp;

    public void SpawnMeteor() {
        Instantiate(gameObject, Constants.GetEnemySpawnPosition(), Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag.Equals("PlayerLaser")) {
            ProcessHit();
        }
        if (other.tag.Equals("Player")) {
            Player.Instance.HandleDeath();
        }
    }

    private void ProcessHit() {
        _health--;
        if (_health <= 0) {
            HandleDeath();
        }
    }

    private void HandleDeath() {
        Vector2 position = transform.position;
        Destroy(this.gameObject);
        GameObject pu = Instantiate(powerUp, position, Quaternion.identity);
        Destroy(pu, 3f);
    }
}
