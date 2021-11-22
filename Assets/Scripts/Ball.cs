using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class Ball : NetworkBehaviour
{

    public float Speed = 30;
    public Rigidbody2D RigidbodyRef;
    public bool CollideWithTeammate = true;
    public bool IsAssigned = false;




    public enum BallState
    {
        NONE,
        IDLE,
        DEAD,
        MOVING,
    };
    public BallState CurrentState = BallState.IDLE;


    private void FixedUpdate()
    {
        RigidbodyRef.velocity = Vector2.ClampMagnitude(RigidbodyRef.velocity, 30.0f);
    }


    void OnCollisionEnter2D(Collision2D Collider)
    {
        //if the ball hits the player
        if (Collider.transform.GetComponent<Player>() && CurrentState == BallState.MOVING)
        {
            float offsetFromPaddle = Collider.transform.position.x - transform.position.x;
            float angularStrength = offsetFromPaddle / Collider.collider.bounds.size.x;
            float bounceAngle = Mathf.Clamp(-45.0f * angularStrength * Mathf.Deg2Rad, -0.45f, 0.45f) ;
            RigidbodyRef.velocity = Vector2.ClampMagnitude(new Vector2(Mathf.Sin(bounceAngle), Mathf.Cos(bounceAngle)), 1.0f) * Speed;
        }
    }
}