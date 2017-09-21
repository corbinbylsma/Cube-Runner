using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour {

    public GameObject prefabTrackChunk; // The track that the player moves along
    public GameObject Enemy;            // A long obstacle that blocks a lane
    public GameObject Wall;             // A wide obstacle that the player has to jump over
    public GameObject Block;            // A small obstacle that swarms all the lanes and in the air.
    public GameObject PowerOne;         // This powerup increases the player's jump
    public GameObject PowerTwo;         // This powerup increases the player's speed and score
    public GameObject PowerThree;       // This powerup give the player an additional life
    public Transform player;            // Referencing the player object

    // A collection of each type of object
    List<GameObject> chunks = new List<GameObject>();
    static public List<GameObject> enemies = new List<GameObject>();
    static public List<GameObject> walls = new List<GameObject>();
    static public List<GameObject> blocks = new List<GameObject>();
    static public List<GameObject> ones = new List<GameObject>();
    static public List<GameObject> twos = new List<GameObject>();
    static public List<GameObject> threes = new List<GameObject>();

    // A collection of each type of object's AABB
    static public List<ColliderAABB> enemiesAABB = new List<ColliderAABB>();
    static public List<ColliderAABB> wallsAABB = new List<ColliderAABB>();
    static public List<ColliderAABB> blocksAABB = new List<ColliderAABB>();
    static public List<ColliderAABB> onesAABB = new List<ColliderAABB>();
    static public List<ColliderAABB> twosAABB = new List<ColliderAABB>();
    static public List<ColliderAABB> threesAABB = new List<ColliderAABB>();

    // Timers that spawn obstacles/powerups when they reach zero
    private float enemyTimer = 1;
    private float wallTimer = 2;
    private float blockTimer = 1;
    private float powerOneTimer = 5;
    private float powerTwoTimer = 8;
    private float powerThreeTimer = 12;

    void Update() {
        if (Settings.lives > 0) // Halts object spawns when all lives are lost
        {
            spawnChunks();
            
            enemyTimer -= Time.deltaTime;
            wallTimer -= Time.deltaTime;
            blockTimer -= Time.deltaTime;
            powerOneTimer -= Time.deltaTime;
            powerTwoTimer -= Time.deltaTime;
            powerThreeTimer -= Time.deltaTime;

            float flow = PlayerMovement.speed / 20; // This variable makes obstacle timers shorter as the player gains speed

            if (enemyTimer <= 0)
            {
                spawnEnemies();
                enemyTimer = Random.Range(1.2f / flow, 2.4f / flow); 
            }
            if (wallTimer <= 0)
            {
                spawnWalls();
                wallTimer = Random.Range(.8f / flow, 1.6f / flow);
            }
            if (blockTimer <= 0)
            {
                spawnBlocks();
                blockTimer = Random.Range(.4f / flow, .8f / flow);
            }
            if (powerOneTimer <= 0)
            {
                spawnPowerOne();
                powerOneTimer = Random.Range(6, 15);
            }
            if (powerTwoTimer <= 0)
            {
                spawnPowerTwo();
                powerTwoTimer = Random.Range(3, 9);
            }
            if (powerThreeTimer <= 0)
            {
                spawnPowerThree();
                powerThreeTimer = Random.Range(8, 20);
            }
        }
    }
    void spawnChunks()
    {
        if (chunks.Count > 0)
        {
            if (player.position.z - chunks[0].transform.position.z > 25)
            {
                Destroy(chunks[0]);
                chunks.RemoveAt(0);
            }
        }

       
        while (chunks.Count < 5)
        {
            Vector3 position = Vector3.zero;

            if (chunks.Count > 0)
            {
                position = chunks[chunks.Count - 1].transform.Find("Connector").position;
            }

            GameObject trackobj = Instantiate(prefabTrackChunk, position, Quaternion.identity);

            chunks.Add(trackobj);
        }
    }
    void spawnEnemies()
    {
        if (enemies.Count > 0)
        {
            if (player.position.z - enemies[0].transform.position.z > 25)
            {
                Destroy(enemies[0]);
                enemies.RemoveAt(0);
                Destroy(enemiesAABB[0]);
                enemiesAABB.RemoveAt(0);
            }
        }
        if (chunks.Count >= 3)
        {
            int rng = Random.Range(1, 10);
            string conversion = rng.ToString();

            Vector3 enemyPos = Vector3.zero;
                
            enemyPos = chunks[chunks.Count - 1].transform.Find("SpawnPoint" + conversion).position;
            GameObject enemyobj = Instantiate(Enemy, enemyPos, Quaternion.identity);
            enemies.Add(enemyobj);

            ColliderAABB enemyAABB = enemyobj.GetComponent<ColliderAABB>();
            enemiesAABB.Add(enemyAABB);
        }
    }
    void spawnWalls()
    {
        if (walls.Count > 0)
        {
            if (player.position.z - walls[0].transform.position.z > 25)
            {
                Destroy(walls[0]);
                walls.RemoveAt(0);
                Destroy(wallsAABB[0]);
                wallsAABB.RemoveAt(0);
            }
        }
        if (chunks.Count >= 3)
        {
            int rng = Random.Range(1, 4) * 3 - 1;
            string conversion = rng.ToString();

            Vector3 wallPos = Vector3.zero;

            wallPos = chunks[chunks.Count - 1].transform.Find("SpawnPoint" + conversion).position;
            GameObject wallobj = Instantiate(Wall, wallPos, Quaternion.identity);
            walls.Add(wallobj);

            ColliderAABB wallAABB = wallobj.GetComponent<ColliderAABB>();
            wallsAABB.Add(wallAABB);
        }
    }
    void spawnBlocks()
    {
        if (blocks.Count > 0)
        {
            if (player.position.z - blocks[0].transform.position.z > 25)
            {
                Destroy(blocks[0]);
                blocks.RemoveAt(0);
                Destroy(blocksAABB[0]);
                blocksAABB.RemoveAt(0);
            }
        }
        if (chunks.Count >= 3)
        {
            int rng = Random.Range(1, 10);
            string conversion = rng.ToString();

            Vector3 blockPos = Vector3.zero;

            blockPos = chunks[chunks.Count - 1].transform.Find("SpawnPoint" + conversion).position;

            int moveUp = Random.Range(0, 2);
            Vector3 hover = new Vector3(0, moveUp * 2, 0);
            GameObject blockobj = Instantiate(Block, blockPos + hover, Quaternion.identity);
            blocks.Add(blockobj);

            ColliderAABB blockAABB = blockobj.GetComponent<ColliderAABB>();
            blocksAABB.Add(blockAABB);
        }
    }
    void spawnPowerOne()
    {
        if (ones.Count > 0)
        {
            if (player.position.z - twos[0].transform.position.z > 25)
            {
                Destroy(ones[0]);
                ones.RemoveAt(0);
                Destroy(onesAABB[0]);
                onesAABB.RemoveAt(0);
            }
        }
        if (chunks.Count >= 3)
        {
            int rng = Random.Range(1, 10);
            string conversion = rng.ToString();

            Vector3 onePos = Vector3.zero;
            onePos = chunks[chunks.Count - 1].transform.Find("SpawnPoint" + conversion).position;

            float moveUp = Random.Range(0, 2);
            Vector3 hover = new Vector3(0, moveUp * 2, 0);
            GameObject oneobj = Instantiate(PowerOne, onePos + hover, Quaternion.identity);
            ones.Add(oneobj);

            ColliderAABB oneAABB = oneobj.GetComponent<ColliderAABB>();
            onesAABB.Add(oneAABB);
            
        }
    }
    void spawnPowerTwo()
    {
        if (twos.Count > 0)
        {
            if (player.position.z - twos[0].transform.position.z > 25)
            {
                Destroy(twos[0]);
                twos.RemoveAt(0);
                Destroy(twosAABB[0]);
                twosAABB.RemoveAt(0);
            }
        }
        if (chunks.Count >= 3)
        {
            int rng = Random.Range(1, 10);
            string conversion = rng.ToString();

            Vector3 twoPos = Vector3.zero;
            twoPos = chunks[chunks.Count - 1].transform.Find("SpawnPoint" + conversion).position;

            float moveUp = Random.Range(0, 2);
            Vector3 hover = new Vector3(0, moveUp * 2, 0);
            GameObject twoobj = Instantiate(PowerTwo, twoPos + hover, Quaternion.identity);
            twos.Add(twoobj);

            ColliderAABB twoAABB = twoobj.GetComponent<ColliderAABB>();
            twosAABB.Add(twoAABB);
            
        }
    }
    void spawnPowerThree()
    {
        if (threes.Count > 0)
        {
            if (player.position.z - threes[0].transform.position.z > 25)
            {
                Destroy(threes[0]);
                threes.RemoveAt(0);
                Destroy(threesAABB[0]);
                threesAABB.RemoveAt(0);
            }
        }
        if (chunks.Count >= 3)
        {
            int rng = Random.Range(1, 10);
            string conversion = rng.ToString();

            Vector3 threePos = Vector3.zero;
            threePos = chunks[chunks.Count - 1].transform.Find("SpawnPoint" + conversion).position;

            float moveUp = Random.Range(0, 2);
            Vector3 hover = new Vector3(0, moveUp * 2, 0);
            GameObject threeobj = Instantiate(PowerThree, threePos + hover, Quaternion.identity);
            threes.Add(threeobj);

            ColliderAABB threeAABB = threeobj.GetComponent<ColliderAABB>();
            threesAABB.Add(threeAABB);
            
        }
    }
}
