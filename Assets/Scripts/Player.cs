using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    public float Speed = 30;
    public Rigidbody2D RigidbodyRef;
    public Ball BallRef;
    [SerializeField] private Vector3 BallFollowSpace = new Vector3(0.0f, 3.0f, 1.0f);
    [SerializeField] private Vector2 LaunchAngleMinMax = new Vector2(-45.0f, 45.0f);

    [SerializeField] private GameObject MenuCam;

    void Start()
    {
        //Only assign to yourself
        if (!isLocalPlayer)
        {
            return;
        }

        //Disable menu graphic upon spawn
        MenuCam = GameObject.Find("Menu Camera");
        MenuCam.SetActive(false);

        //Assign created ball to yourself
        GameObject[] Balls;
        Balls = GameObject.FindGameObjectsWithTag("Ball");
        for (int i = 0; i < Balls.Length; i++)
        {
            if (Balls[i].GetComponent<Ball>().IsAssigned == false)
            {
                BallRef = Balls[i].GetComponent<Ball>();
                Balls[i].GetComponent<Ball>().IsAssigned = true;
            }
        }
    }

    //Enable the main menu graphic upon leaving
    void OnDestroy()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        MenuCam.SetActive(true);
    }

    void Update()
    {
        //You can only Launch your own ball
        if (!isLocalPlayer)
        {
            return;
        }
        
        //Launch the ball when player presses space if ball is idle
        if ((Input.GetKeyDown(KeyCode.Space)) && (BallRef.CurrentState == Ball.BallState.IDLE))
        {
            BallRef.RigidbodyRef.simulated = true;
            BallRef.CurrentState = Ball.BallState.MOVING;
            float BounceAngle = Random.Range(LaunchAngleMinMax.x, LaunchAngleMinMax.y) * Mathf.Deg2Rad;
            BallRef.RigidbodyRef.AddForce(new Vector2(Mathf.Sin(BounceAngle), Mathf.Cos(BounceAngle)) * BallRef.Speed, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        //You can only control your own player
        if (!isLocalPlayer)
        {
            return;
        }

        //Handles movement
        RigidbodyRef.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), 0) * Speed * Time.fixedDeltaTime;

        //Ball follows the paddle if in idle state
        if (BallRef.CurrentState == Ball.BallState.IDLE)
        {
            BallRef.transform.position = gameObject.transform.position + BallFollowSpace;
        }
    }
}
