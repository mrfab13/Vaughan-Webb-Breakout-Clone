using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Ball : NetworkBehaviour
{
    public float Speed = 30;
    public Rigidbody2D RigidbodyRef;
    public bool IsAssigned = false;
    [SerializeField] private Vector2 PaddleAngleMinMax = new Vector2(-0.45f, 0.45f);

    public enum BallState
    {
        NONE,
        IDLE,
        MOVING,
    };

    [SyncVar]
    public BallState CurrentState = BallState.IDLE;

    //Makes sure the speed doesnt excede max 
    void FixedUpdate()
    {
        RigidbodyRef.velocity = Vector2.ClampMagnitude(RigidbodyRef.velocity, Speed);
    }

    //Ball collision handler
    void OnCollisionEnter2D(Collision2D Collider)
    {
        //Reflects the ball at calculated angle based upon where the collision happens on the paddle
        if (Collider.gameObject.CompareTag("Player") && CurrentState == BallState.MOVING)
        {
            float OffsetFromPaddle = Collider.transform.position.x - transform.position.x;
            float AngularStrength = OffsetFromPaddle / Collider.collider.bounds.size.x;
            float BounceAngle = Mathf.Clamp(-45.0f * AngularStrength * Mathf.Deg2Rad, PaddleAngleMinMax.x, PaddleAngleMinMax.y);
            RigidbodyRef.velocity = Vector2.ClampMagnitude(new Vector2(Mathf.Sin(BounceAngle), Mathf.Cos(BounceAngle)), 1.0f) * Speed;
        }
        else if (Collider.gameObject.CompareTag("Brick")) //Deals 1 damage to brick upon collisions
        {
            Collider.transform.GetComponent<Brick>().HP -= 1;
        }
        else if (Collider.gameObject.CompareTag("KillWall")) //Resets the ball upon leavign the map
        {
            RigidbodyRef.simulated = false;
            CurrentState = BallState.IDLE;
        }
    }
}