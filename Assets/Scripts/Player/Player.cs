using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public EnemySpawning enemyController;

    private int _health;
    public int Health { get { return _health; } set { _health = value; } }

	private int _killCount;
    public int KillCount { get { return _killCount; } set { _killCount = value; } }

    private static Player _instance;

    public static Player Instance { get { return _instance; } }

    private void Awake() {
        if (_instance != null) {
            Destroy(this);
        } else {
            _instance = this;
        }

        _health = 5;
    }


    public void CheckLevelAdvancement() {
        switch(_killCount) {
            case 5:
                enemyController.SetGameStage(EnemySpawning.GameStage.Medium);
                return;
            case 15:
                enemyController.SetGameStage(EnemySpawning.GameStage.Hard);
                return;
            case 30:
                enemyController.SetGameStage(EnemySpawning.GameStage.Extreme);
                return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        print(other.tag);
        if (!other.tag.Equals("EnemyLaser")) {
            ProcessHit();
            Destroy(other.gameObject);
        }
        if (other.tag.Equals("PowerUp")) {
            GetComponent<PowerUp>().ActivatePowerUp();
            Destroy(other.gameObject);
        }
        if (other.tag.Equals("Meteor")) {
            HandleDeath();
        }
    }

    private void ProcessHit() {
        Player.Instance.Health--;
        UIController.Instance.UpdateHealthText();
        if(_health <= 0) {
            HandleDeath();
        }
    }

    public void HandleDeath() {
        Time.timeScale = 0;
        UIController.Instance.DisplayDeathScreen();
    }

}
