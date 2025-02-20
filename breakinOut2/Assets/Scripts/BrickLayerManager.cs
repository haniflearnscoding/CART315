using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickLayerManager : MonoBehaviour {
    public GameObject brick;
    public int rows = 5, columns = 10;
    public float brickSpacing_h = 1.5f;
    public float brickSpacing_v = 0.75f;

    void Start() {
        CreateBrickLayout();
    }

    // Creates the brick layout and parents each brick under this manager
    public void CreateBrickLayout() {
        // Clear previous bricks if any
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        
        for (int j = 0; j < rows; j++) {
            for (int i = 0; i < columns; i++) {
                float xPos = -columns + (i * brickSpacing_h);
                float yPos = rows - (j * brickSpacing_v);
                // Random rotation for each brick when instantiating
                Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(-15f, 15f));
                GameObject newBrick = Instantiate(brick, new Vector3(xPos, yPos, 0), randomRotation, transform);
                Debug.Log("Created brick " + newBrick.name + " with rotation: " + newBrick.transform.rotation.eulerAngles.z);
            }
        }
    }

    // Resets the rotation of all bricks (assuming they are children of this manager)
    public void ResetBrickRotations() {
        Debug.Log("Resetting brick rotations...");
        foreach (Transform brickTransform in transform) {
            float newAngle = Random.Range(-15f, 15f);
            brickTransform.rotation = Quaternion.Euler(0, 0, newAngle);
            Debug.Log("Brick " + brickTransform.name + " new rotation: " + brickTransform.rotation.eulerAngles.z);
        }
    }
}
