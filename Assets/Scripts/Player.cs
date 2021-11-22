using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class Player : NetworkBehaviour
{
    public float Speed = 30;
    public Rigidbody2D RigidbodyRef;
    public Ball BallRef;

    void Start()
    {

        if (!isLocalPlayer)
        {
            return;
        }

        //BallRef = PrefBall.GetComponent<Ball>();
        GameObject[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("Ball");


        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].GetComponent<Ball>().IsAssigned == false)
            {
                Debug.Log(i);
                BallRef = gameObjects[i].GetComponent<Ball>();
                gameObjects[i].GetComponent<Ball>().IsAssigned = true;
            }
        }
    }

    void Update()
    {
        //you can only control your own player
        if (!isLocalPlayer)
        {
            return;
        }
        
        //player interact button
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (BallRef.CurrentState)
            {
                case Ball.BallState.NONE:
                    {
                        Debug.LogWarning("Ball State Not Set");
                        break;
                    }
                case Ball.BallState.IDLE:
                    {
                        //Launch the ball
                        BallRef.RigidbodyRef.simulated = true;
                        BallRef.CurrentState = Ball.BallState.MOVING;
                        BallRef.RigidbodyRef.AddForce(Vector2.up * BallRef.Speed, ForceMode2D.Impulse);

                        Debug.Log("PEW");
                        break;
                    }
                case Ball.BallState.DEAD:
                    {
                        //respawn the ball
                        break;
                    }
                case Ball.BallState.MOVING:
                    {
                        //speed boost if neer paddle?
                        break;
                    }
                default:
                    {
                        Debug.LogWarning("Ball State Not Set");
                        break;
                    }
            }
        }
    }

    void FixedUpdate()
    {
        //you can only control your own player
        if (!isLocalPlayer)
        {
            return;
        }

        //paddle horizontal movment
        RigidbodyRef.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), 0) * Speed * Time.fixedDeltaTime;

        //follow the paddle

        if (BallRef.CurrentState == Ball.BallState.IDLE)
        {
            BallRef.transform.position = gameObject.transform.position + new Vector3(0.0f, 3.0f, 0.0f);
            Debug.Log(BallRef.transform.position + " " + gameObject.transform.position + new Vector3(0.0f, 3.0f, 0.0f));
        }
    }
}
