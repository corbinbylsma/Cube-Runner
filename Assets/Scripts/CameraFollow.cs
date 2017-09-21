using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public Vector3 offset;

    void LateUpdate()
    {
        offset = new Vector3(0, 0, PlayerMovement.speed / 20 - 1.5f);
        transform.position = target.position - offset;
    }
}
