using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonClickManager : MonoBehaviour
{
    private Vector3 _ogScale;
    public string nextScene = "PassengerCheck";
    
    public Sprite firstChangeSprite; // Assign this in Inspector (first sprite change)
    public Sprite secondChangeSprite; // Assign this in Inspector (second sprite change)
    
    private SpriteRenderer spriteRenderer;
    private bool hasChangedOnce = false; // Ensure first sprite change happens only once

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _ogScale = transform.localScale;
    }

    private void Update()
    {
        if (!hasChangedOnce && CheckItem()) // First sprite change when condition is met
        {
            if (spriteRenderer != null && firstChangeSprite != null)
            {
                spriteRenderer.sprite = firstChangeSprite;
                hasChangedOnce = true; // Prevents it from continuously changing
            }
        }
    }

    public void OnClick()
    {
        if (CheckItem()) 
        {
            if (spriteRenderer != null && secondChangeSprite != null)
            {
                spriteRenderer.sprite = secondChangeSprite; // Second sprite change on click
            }
            SceneManager.LoadScene(nextScene);
        }
    }

    private bool CheckItem()
    {
        GameObject[] correctItems = GameObject.FindGameObjectsWithTag("DangerousItem");
        foreach (GameObject item in correctItems)
        {
            XRayItem xRayItem = item.GetComponent<XRayItem>();
            if (xRayItem != null && !xRayItem.IsMarked)
            {
                return false; 
            }
        }
        
        GameObject[] incorrectItems = GameObject.FindGameObjectsWithTag("SafeItem");
        foreach (GameObject item in incorrectItems)
        {
            XRayItem xRayItem = item.GetComponent<XRayItem>();
            if (xRayItem != null && xRayItem.IsMarked)
            {
                return false;
            }
        }

        return true; 
    }
}
