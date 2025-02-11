using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BreakoutBall_working : MonoBehaviour
{
    private Rigidbody2D rb;
    public float ballSpeed;
    public float maxSpeed = 10f;
    public float minSpeed = 2f;
    public AudioSource scoreSound, blip;

    private int[] dirOptions = { -1, 1 };
    private int hDir;

    private bool gameRunning;
    public static int ballCount = 3; // Start with 3 balls

    public GameObject gameOverScreen; // Assign in Inspector

    void Start()
    {
        gameOverScreen.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        Reset();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameRunning)
        {
            StartCoroutine(Launch());
        }
    }

    private IEnumerator Launch()
    {
        gameRunning = true;
        hDir = dirOptions[Random.Range(0, dirOptions.Length)];
        rb.linearVelocity = new Vector2(ballSpeed * hDir, -ballSpeed);
        yield return null;
    }

    public void Reset()
    {
        ballCount--; // Decrement lives
        Debug.Log("Balls left: " + ballCount);

        if (ballCount <= 0)
        {
            GameOver();
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            transform.position = new Vector2(0, 0);
            gameRunning = false;
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true); // Show Game Over UI
        }
    }
    
    // if the ball goes out of bounds
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Reset")
        {
            Reset(); // Ball lost
        }
        
        // did we hit a wall?
        if (other.gameObject.tag == "Wall")
        {
            // make pitch lower
            blip.pitch = 0.75f;
            blip.Play();
            SpeedCheck();
        }
        if (other.gameObject.tag == "Brick")
        {
            // make pitch lower
            blip.pitch = 1.25f;
            blip.Play();
            Destroy(other.gameObject);
            SpeedCheck();
            
        }
        
        // did we hit a paddle?
        if (other.gameObject.tag == "Paddle")
        {
            // make pitch higher
            blip.pitch = 1f;
            blip.Play();
            SpeedCheck();
        }
        
        // did we hit the Bottom
        if (other.gameObject.tag == "Reset")
        {
            Reset();
        }

    }

    private void SpeedCheck() {
        
        // Prevent ball from going too fast
        if (Mathf.Abs(rb.linearVelocity.x) > maxSpeed) rb.linearVelocity = new Vector2(rb.linearVelocity.x * 0.99f, rb.linearVelocity.y);
        if (Mathf.Abs(rb.linearVelocity.y) > maxSpeed) rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.99f);

        if (Mathf.Abs(rb.linearVelocity.x) < minSpeed)
        {
            Debug.Log("too slow?");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x * 1.1f, rb.linearVelocity.y);
        }

        if (Mathf.Abs(rb.linearVelocity.y) < minSpeed)
        {
            Debug.Log("too slow?");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 1.1f);
        }


        // Prevent too shallow of an angle
        if (Mathf.Abs(rb.linearVelocity.x) < minSpeed) {
            // shorthand to check for existing direction
            rb.linearVelocity = new Vector2((rb.linearVelocity.x < 0) ? -minSpeed : minSpeed, rb.linearVelocity.y);
        }

        if (Mathf.Abs(rb.linearVelocity.y) < minSpeed) {
            // shorthand to check for existing direction
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, (rb.linearVelocity.y < 0) ? -minSpeed : minSpeed);
        }
        
        Debug.Log(rb.linearVelocity);

    }


}
