using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject destructionPrefab;

    public void RestartLevel() {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void QuitGame() {
        Application.Quit();
    }
}
