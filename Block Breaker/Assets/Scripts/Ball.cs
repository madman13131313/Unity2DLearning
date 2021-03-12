﻿using UnityEngine;

public class Ball : MonoBehaviour
{
    // config params
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = .2f;

    // state
    bool hasStarted = false;
    Vector2 paddleToBallVector;

    // Cached component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidbody2D;
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector.y = transform.position.y - paddle1.transform.position.y;
        gameSession = FindObjectOfType<GameSession>();
        myAudioSource = GetComponent<AudioSource>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (! hasStarted) { 
            LockBallToPaddle();
            LaunchOnMouseClick();
        }  
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0)) {
            myRigidbody2D.velocity = new Vector2(xPush, yPush);
            hasStarted = true;
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2
             (Random.Range(0, randomFactor),
              Random.Range(0, randomFactor));

        if (hasStarted)
        {
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidbody2D.velocity += velocityTweak;
        }
    }
}
