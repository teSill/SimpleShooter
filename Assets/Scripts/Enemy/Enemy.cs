using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    protected float movementSpeed = 5f;
    protected float projectileSpeed = -10f;
    protected float shootingInterval = 2f;
    protected float health = 2f;

    protected Player player;

    protected bool inCombat;

    protected virtual void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();
        StartCoroutine(MoveToBattlePosition());
    }

    protected virtual void Shoot() {}

    protected virtual IEnumerator StartCombat() {
        inCombat = true;
        float randomNumber = Random.Range(0, 2);
        float moveDirection = randomNumber == 1 ? -movementSpeed : movementSpeed;
        while(true) {
            transform.position = new Vector2(transform.position.x + Constants.horizontalSpeed * Time.deltaTime, transform.position.y + moveDirection * Time.deltaTime);
            if (NeedsToTurn()) {
                moveDirection = moveDirection > 0 ? -movementSpeed : movementSpeed;
            }
            yield return null;
        }
    }

    protected bool NeedsToTurn() {
        Camera gameCamera = Camera.main;
        float yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + Constants.paddingMin;
        float yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - Constants.paddingMax;
        int randomNumber = Random.Range(0, 150);
        print(randomNumber);
        if (randomNumber == 0)
            return true;
        if (transform.position.y <= yMin)
            return true;
        if (transform.position.y >= yMax)
            return true;
        return false;
    }

     private IEnumerator MoveToBattlePosition() {
        while (Vector2.Distance(transform.position, player.transform.position) > 17) {
            yield return null;
        }
        StartCoroutine(StartCombat());
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name.Contains("Player") && inCombat) {
            ProcessHit();
            Destroy(other.gameObject);
        }
    }

    private void ProcessHit() {
        health--;
        if (health <= 0) {
            HandleDeath();
        }
    }

    private void HandleDeath() {
        Destroy(this.gameObject);
        Player.Instance.KillCount++;
        Player.Instance.CheckLevelAdvancement();
        UIController.Instance.UpdateKillCountText();
    }
}
