using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCam: MonoBehaviour
{
    public GameObject RotateAbout;
    public GameObject Target;
    public Camera Cam;
    private float Theta = 0.0f;
    public float Speed = 1.0f;
    public float Height = 2.0f;
    public float CircleSize = 1.0f;
    public float Center = 0.0f;
    public Vector3 TargetOffset;

    [Header("Rotate on these axis")]
    public bool X;
    public bool Y;
    public bool Z;


    void Update()
    {
        Theta += (Time.deltaTime * Speed);
        Vector3 newpos = Vector3.zero;

        //Rotate on the X axis 
        if (X == true)
        {
            newpos += new Vector3(Mathf.Sin(Theta) * CircleSize, 0, 0);
        }
        else
        {
            newpos += new Vector3(Height, 0, 0);
        }

        //Rotate on the Y axis
        if (Y == true)
        {
            if (X == true)
            {
                newpos += new Vector3(0, Mathf.Cos(Theta) * CircleSize, 0);
            }
            else
            {
                newpos += new Vector3(0, Mathf.Sin(Theta) * CircleSize, 0);
            }
        }
        else
        {
            newpos += new Vector3(0, Height, 0);
        }

        //Rotate on the Z axis
        if (Z == true)
        {
            newpos += new Vector3(0, 0, Mathf.Cos(Theta) * CircleSize);
        }
        else
        {
            newpos += new Vector3(0, 0, Height);
        }

        //Apply calculations
        Cam.GetComponent<Transform>().localPosition = newpos + RotateAbout.transform.localPosition;
        Cam.transform.LookAt(new Vector3(Target.transform.position.x + TargetOffset.x, Target.transform.position.y + TargetOffset.y, Target.transform.position.z + TargetOffset.z), Vector3.up);
        Cam.transform.Rotate(0.0f, Center, 0.0f);
    }

}
