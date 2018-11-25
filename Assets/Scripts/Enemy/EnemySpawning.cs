using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour {

    public List<Enemy> enemies;
    private List<Enemy> spawnableEnemies = new List<Enemy>();

    public Player player;
    public Meteor meteor;

    public enum GameStage {
        Easy,
        Medium,
        Hard,
        Extreme
    }

   public static GameStage currentStage;

	// Use this for initialization
	void Start () {
		spawnableEnemies.Add(enemies[0]);
        StartCoroutine(EnemySpawnLoop());
	}

    private IEnumerator EnemySpawnLoop() {
        while(true) {
            SpawnRandomEnemy();
            yield return new WaitForSeconds(GetSpawnInterval());
        }
    }

    private int GetSpawnInterval() {
        switch(currentStage) {
            case GameStage.Easy:
                return 4;
            case GameStage.Medium:
                return 3;
            case GameStage.Hard:
                return 2;
            case GameStage.Extreme:
                return 1;
            default:
                return 0;
        }
    }

    public void SetGameStage(GameStage stage) {
        currentStage = stage;
        if(!spawnableEnemies.Contains(enemies[spawnableEnemies.Count])) {
            spawnableEnemies.Add(enemies[spawnableEnemies.Count]);
        }
        UIController.Instance.UpdateLevelText();
        UIController.Instance.DisplayLevelAdvanceTextCall();
    }

    private void SpawnRandomEnemy() {
        Enemy enemy = spawnableEnemies[Random.Range(0, spawnableEnemies.Count)];
        enemy = Instantiate(enemy, Constants.GetEnemySpawnPosition(), Quaternion.Euler(new Vector3(0, 0, 270)));

        // Meteor spawning
        int randomNumber = Random.Range(0, 2); 
        if (randomNumber == 0) { // 20%
            meteor.SpawnMeteor();
        }
    }
}
