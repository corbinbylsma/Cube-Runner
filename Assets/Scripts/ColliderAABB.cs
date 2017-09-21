using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderAABB : MonoBehaviour {

    static public ColliderAABB Player;

    static public ColliderAABB Enemy;

    static public ColliderAABB Wall;

    static public ColliderAABB Block;

    public Vector3 halfSize;

    Vector3 min = Vector3.zero;
    Vector3 max = Vector3.zero;

    void Update()
    {
        CalcEdges();
    }

    void CalcEdges()
    {
        min = transform.position - halfSize;
        max = transform.position + halfSize;
    }

    public bool CheckOverlap(ColliderAABB other)
    {
        if (other != null)
        {
            if (min.x > other.max.x) return false;
            if (max.x < other.min.x) return false;

            if (min.y > other.max.y) return false;
            if (max.y < other.min.y) return false;

            if (min.z > other.max.z) return false;
            if (max.z < other.min.z) return false;

            return true;
        }
        else
        {
            return false;
        }
    }
}