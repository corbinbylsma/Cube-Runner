using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lanes : MonoBehaviour {

    private int xpos = 0;               // xpos is the lane movement.
    private const float JUMP_VEL = .6f;
    public float speed = 10;
    private float zpos = 0;             // zpos is the constant forward movement down the track.
    private float ypos = 0;             // ypos is the height manipulated by jumping.
    private float yvel = 0;
    private float yacc = -.03f;

	void Update () {
    
        // Move the cube on input
        if (Input.GetButtonDown("Right"))
        {
            xpos++;
        }
        else if (Input.GetButtonDown("Left"))
        {
            xpos--;
        }
        
        // Jump Cube on input
        if (Input.GetButtonDown("Jump"))
        {
            if (transform.position.y == 0)
            {
                yvel = JUMP_VEL;
            }
        }

        // Don't let cube go outside of lanes
        if (xpos < -1) xpos = -1;
        if (xpos > 1) xpos = 1;

        // Continuously move player forward
        zpos += speed * Time.deltaTime;

        // Calculate air position/velocity/acceleration
        ypos += yvel;
        yvel += yacc;
        if (ypos <= 0)
        {
            yvel = 0;
            ypos = 0;
        }

        // Update position
        transform.position = new Vector3(2.4f * xpos, ypos, zpos);

    }
}
