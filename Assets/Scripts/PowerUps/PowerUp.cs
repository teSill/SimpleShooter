using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    private float _movementSpeedBuff = 1.5f;

    private PlayerController playerController;

    public bool movementBoost = false, projectileBoost = false;

    private void Start() {
        playerController = GetComponent<PlayerController>();
    }


    public void ActivatePowerUp() {
        int randomNumber = Random.Range(0, 4);
        string boostType = "";
        switch(randomNumber) {
            case 0:
                StartCoroutine(MovementPowerUp());
                boostType = "Boost: Movement";
                break;
            case 1:
                 StartCoroutine(ProjectilePowerUp());
                boostType = "Boost: Projectiles";
                break;
            case 2:
                DestructionPowerUp();
                boostType = "Boost: Enemy wipe";
                break;
            case 3:
                RestoreHealthPowerUp();
                boostType = "Boost: Health restore";
                break; 
        }
        StartCoroutine(UIController.Instance.DisplayPowerUpText(boostType));
    }

    private IEnumerator MovementPowerUp() {
        movementBoost = true;
        yield return new WaitForSeconds(5f);
        movementBoost = false;
    }

    
    private IEnumerator ProjectilePowerUp() {
        projectileBoost = true;
        yield return new WaitForSeconds(5f);
        projectileBoost = false;
    }

    private void DestructionPowerUp() {
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
            Destroy(enemy);
            Player.Instance.KillCount++;
        }
        Player.Instance.CheckLevelAdvancement();
        UIController.Instance.UpdateKillCountText();
    }

    private void RestoreHealthPowerUp() {
        Player.Instance.Health = 5;
        UIController.Instance.UpdateHealthText();
    }
	
}
