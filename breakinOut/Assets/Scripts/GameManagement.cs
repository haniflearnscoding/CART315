using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour {
    public int lives;
    public int points;
    public int lives = 3;
    public Image heartImage;

    public static GameManagement S;
    
    // Start is called before the first frame update
    void Awake() {
        S = this;
    }
    
    public void LoseLife() {
        lives -= 1;
        Debug.Log(lives);
        
        if(lives <=0) GameOver();
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    
  
}
