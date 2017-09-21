using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageCollision : MonoBehaviour
{

    ColliderAABB player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<ColliderAABB>();
    }

    void LateUpdate()
    {
        CollisionDetectBlock();
        CollisionDetectEnemy();
        CollisionDetectWall();
        CollisionDetectPOne();
        CollisionDetectPTwo();
        CollisionDetectPThree();
    }

    void CollisionDetectEnemy()
    {
        foreach (ColliderAABB Enemy in SceneControl.enemiesAABB)
        {
            bool resultEnemy = player.CheckOverlap(Enemy);

            if (resultEnemy)
            {
                // Player loses a life
                if (PlayerMovement.iFrameTimer <= 0 && PlayerMovement.speed > 0)
                {
                    Settings.lives--;
                    PlayerMovement.iFrameTimer = .5f / (PlayerMovement.speed / 20);
                }
            }
        }
    }

    void CollisionDetectWall()
    {
        foreach (ColliderAABB Wall in SceneControl.wallsAABB)
        {
            bool resultWall = player.CheckOverlap(Wall);

            if (resultWall)
            {
                // Player loses a life
                if (PlayerMovement.iFrameTimer <= 0 && PlayerMovement.speed > 0)
                {
                    Settings.lives--;
                    PlayerMovement.iFrameTimer = .5f;
                }
            }
        }
    }

    void CollisionDetectBlock()
    {
        foreach (ColliderAABB Block in SceneControl.blocksAABB)
        {
            bool resultBlock = player.CheckOverlap(Block);

            if (resultBlock)
            {
                // Player loses a life
                if (PlayerMovement.iFrameTimer <= 0 && PlayerMovement.speed > 0)
                {
                    Settings.lives--;
                    PlayerMovement.iFrameTimer = .5f;
                }
            }
        }
    }
    void CollisionDetectPOne()
    {
        foreach (ColliderAABB PowerOne in SceneControl.onesAABB)
        {
            bool resultPOne = player.CheckOverlap(PowerOne);

            if (resultPOne)
            {
                // Player gains a higher jump
                if (PlayerMovement.iFrameTimer <= 0 && PlayerMovement.speed > 0)
                {
                    PlayerMovement.iFrameTimer = .3f;
                    PlayerMovement.jumpVel += .05f;

                    Destroy(SceneControl.ones[0]);
                    SceneControl.ones.RemoveAt(0);
                    Destroy(SceneControl.onesAABB[0]);
                    SceneControl.onesAABB.RemoveAt(0);
                }
            }
        }
    }
    void CollisionDetectPTwo()
    {
        foreach (ColliderAABB PowerTwo in SceneControl.twosAABB)
        {
            bool resultPTwo = player.CheckOverlap(PowerTwo);

            if (resultPTwo)
            {
                // Player gains a higher speed and score multiplier
                if (PlayerMovement.iFrameTimer <= 0 && PlayerMovement.speed > 0)
                {
                    PlayerMovement.iFrameTimer = .3f;
                    PlayerMovement.speed += 3;
                    Settings.bonus += 5;

                    Destroy(SceneControl.twos[0]);
                    SceneControl.twos.RemoveAt(0);
                    Destroy(SceneControl.twosAABB[0]);
                    SceneControl.twosAABB.RemoveAt(0);
                }
            }
        }
    }
    void CollisionDetectPThree()
    {
        foreach (ColliderAABB PowerThree in SceneControl.threesAABB)
        {
            bool resultPThree = player.CheckOverlap(PowerThree);

            if (resultPThree)
            {
                // Player gains an extra life
                if (PlayerMovement.iFrameTimer <= 0 && PlayerMovement.speed > 0)
                {
                    PlayerMovement.iFrameTimer = .3f;
                    Settings.lives++;

                    Destroy(SceneControl.threes[0]);
                    SceneControl.threes.RemoveAt(0);
                    Destroy(SceneControl.threesAABB[0]);
                    SceneControl.threesAABB.RemoveAt(0);
                }
            }
        }
    }
}
