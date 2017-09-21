using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    static public float jumpVel = .4f; // Jump's Power
    static public float speed = 20;     // Speed down the track
    static public float iFrameTimer = 0;// A short invincibility timer for when you hit something.
    private float zpos = 0;             // zpos is the constant forward movement down the track.
    private float xpos = 0;             // xpos is the lane position.
    private float xvel = 0;             // xvel is the short movement between lanes.
    private float xlane = 0;            // xlane (-1, 0, or 1) determines left, middle, or right lane
    private float ypos = 0;             // ypos is the current height of the jump
    private float yvel = 0;             // yvel is the velocity of the jump
    static public float yacc = -.03f;   // yacc is the gravity lowering velocity at a constant rate. It slightly increases with Jump Power.

    void Update()
    {
        GetInput();
        DoPhysics();

        iFrameTimer -= Time.deltaTime;
        yacc = jumpVel / -13 - .003f;
    }
    void GetInput()
    {
        if (Settings.lives > 0) {
            // Move the cube on input
            if (Input.GetButtonDown("Right"))
            {
                xlane++;
                xvel = 1;
            }
            else if (Input.GetButtonDown("Left"))
            {
                xlane--;
                xvel = -1;
            }

            // Jump the cube on input
            if (Input.GetButtonDown("Jump"))
            {
                if (transform.position.y == 0)
                {
                    yvel = jumpVel;
                }
            }
        }
    }

    void DoPhysics()
    {
        // Don't let cube go outside of lanes
        if (xlane < -1) xlane = -1;
        if (xlane > 1) xlane = 1;

        // Continuously move player forward
        zpos += speed * Time.deltaTime;
        if (Settings.lives <= 0)
        {
            speed = 0;
        }

        // Calculate air position/velocity/acceleration y-axis
        ypos += yvel;
        yvel += yacc;
        // This if statement snaps the player to the ground (There's no AABB for the ground track, this is much simpler)
        if (ypos <= 0)
        {
            yvel = 0;
            ypos = 0;
        }

        // Calculate air position/velocity/acceleration x-axis
        xpos += xvel;
        // This if statement snaps the player to the three lanes
        if (xvel >= 1 && xpos >= xlane * 2.4f || xvel <= -1 && xpos <= xlane * 2.4f)
        {
            xvel = 0;
            xpos = xlane * 2.4f;
        }

        // Update position
        transform.position = new Vector3(xpos, ypos, zpos);
    }
}
