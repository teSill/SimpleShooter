using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public TMP_Text levelUpText;

    public TMP_Text healthText;
    public TMP_Text levelText;
    public TMP_Text killCountText;

    public GameObject deathInterface; 
    public GameObject powerUpText;
    public Text boostTypeText;

	private static UIController _instance;

    public static UIController Instance { get { return _instance; } }

    private void Awake() {
        if (_instance != null) {
            Destroy(this);
        } else {
            _instance = this;
        }
    }

    public void UpdateHealthText() {
        healthText.text = "Health: " + Player.Instance.Health + "/5";
    }

    public void UpdateLevelText() {
        levelText.text = "Level: " + EnemySpawning.currentStage;
    }

    public void UpdateKillCountText() {
        killCountText.text = "Kill count: " + Player.Instance.KillCount;
    }

    public void DisplayLevelAdvanceTextCall() {
        StartCoroutine(DisplayLevelAdvanceText());
    }

    public IEnumerator DisplayPowerUpText(string boostType) {
        boostTypeText.text = boostType;
        powerUpText.SetActive(true);
        yield return new WaitForSeconds(2f);
        powerUpText.SetActive(false);
    }

    private IEnumerator DisplayLevelAdvanceText() {
        levelUpText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        levelUpText.gameObject.SetActive(false);
    }

    public void DisplayDeathScreen() {
        deathInterface.SetActive(true);
    }
}
