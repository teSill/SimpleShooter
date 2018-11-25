using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour {

    public static float horizontalSpeed = 10f;

    public static float projectileLifetime = 5f;

     // Restrict movement near the edges of the camera by the following amount. Applies to both players and enemies
    public static float paddingMin = 1f;
    public static float paddingMax = 2.5f;

    public static Vector2 GetEnemySpawnPosition() {
        Camera gameCamera = Camera.main;
        float yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + Constants.paddingMin;
        float yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - Constants.paddingMax;

        float xPos = gameCamera.transform.position.x + 20;
        float yPos = Random.Range(yMin, yMax);

        return new Vector2(xPos, yPos);
    }
}
