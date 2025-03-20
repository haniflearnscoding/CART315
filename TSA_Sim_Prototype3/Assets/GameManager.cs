using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string nextSceneName; // Set this in the Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SafeItem") || other.CompareTag("DangerousItem")) // Ensure your suitcase GameObjects are tagged as "Suitcase"
        {
            Debug.Log("Suitcase entered trigger, changing scene...");
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
