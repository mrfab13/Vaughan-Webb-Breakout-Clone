using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class Player : NetworkBehaviour
{
    public float Speed = 30;
    public Rigidbody2D RigidbodyRef;
    public Ball BallRef;

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
            BallRef.transform.position = this.gameObject.transform.position + new Vector3(0.0f, 3.0f, 0.0f);
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
                        BallRef.RigidbodyRef.velocity = Vector2.up * BallRef.Speed;

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
}
