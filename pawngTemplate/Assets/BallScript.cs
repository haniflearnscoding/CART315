using System;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;

public class BallScript : MonoBehaviour
{
    public float ballSpeed = 2;
    private int[] directionOptions = { -1, 1 };
    private int hDir, yDir;

    public int score1, score2;
    public AudioSource blip;
    
    private Rigidbody2D rb;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(routine: Launch());
    }

    private void OnCollisionEnter2D(Collision2D wall)
    {
        if (wall.gameObject.name == "leftWall")
        {
            //give points to player 2
            score2 += 1;
            Reset();
        }
        
        if (wall.gameObject.name == "rightWall")
        {
            //give points to player 2
            score1 += 1;
            Reset();
        }

        if (wall.gameObject.name == "topWall" || wall.gameObject.name == "bottomWall")
        {
            blip.pitch = 0.75f;
            blip.Play();
        }
        if (wall.gameObject.name == "paddleRight" || wall.gameObject.name == "paddleLeft")
        {
            blip.pitch = 1f;
            blip.Play();
        }
    }

    private IEnumerator Launch()
    {
        // choose random x dir
        hDir = Random.Range(0, directionOptions.Length);
        hDir = directionOptions[Random.Range(0, directionOptions.Length)];
        // choose random y dir
        yDir = Random.Range(0, directionOptions.Length);
        yDir = directionOptions[Random.Range(0, directionOptions.Length)];
        Random.Range(0, directionOptions.Length);
        // wait for x secs
        yield return new WaitForSeconds(1);
        // apply force
        rb.AddForce(transform.right * ballSpeed * hDir);
        rb.AddForce(transform.up * ballSpeed * yDir);
    }

    private void Reset()
    {
        rb.linearVelocity = Vector2.zero;
        // reset 0/0
        this.transform.localPosition = new Vector3(0, 0, 0);    
        // Launch
        StartCoroutine(routine:Launch());
    }
}
