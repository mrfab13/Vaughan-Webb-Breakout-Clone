using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class Ball : NetworkBehaviour
{

    public float Speed = 30;
    public Rigidbody2D RigidbodyRef;
    public bool CollideWithTeammate = true;

    public enum BallState
    { 
        NONE,
        IDLE,
        DEAD,
        MOVING,
    };
    public BallState CurrentState = BallState.IDLE;


    void OnCollisionEnter2D(Collision2D Collider)
    {
        //if the ball hits the player
        if (Collider.transform.GetComponent<Player>() && CurrentState == BallState.MOVING)
        {
            //calculates angle to reflect the ball
            float X = (transform.position.x - Collider.transform.position.x) / Collider.collider.bounds.size.x;

            //vertical component
            float Y = 1;
            if (CollideWithTeammate)
            {
                Y = Collider.relativeVelocity.y > 0 ? 1 : -1;
            }

            //Calculate direction
            Vector2 Dir = new Vector2(X, Y).normalized;

            // Set Velocity with dir * speed
            RigidbodyRef.velocity = Dir * Speed;
        }
    }
}
