using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour {

    public GameObject prefabTrackChunk;

    public GameObject Enemy;

    public Transform player;

    List<GameObject> chunks = new List<GameObject>();

    List<GameObject> enemies = new List<GameObject>();

	void Update () {

        if (chunks.Count > 0)
        {
            if (player.position.z - chunks[0].transform.position.z > 25)
            {
                Destroy(chunks[0]);
                chunks.RemoveAt(0);
            }
        }

        if (enemies.Count > 0)
        {
            if (player.position.z - enemies[0].transform.position.z > 25)
            {
                Destroy(enemies[0]);
                enemies.RemoveAt(0);
            }
        }

        while (chunks.Count < 7)
        {
            Vector3 position = Vector3.zero;

            if (chunks.Count > 0)
            {
                position = chunks[chunks.Count - 1].transform.Find("Connector").position;
            }

            GameObject trackobj = Instantiate(prefabTrackChunk, position, Quaternion.identity);

            chunks.Add(trackobj);

            if (chunks.Count >= 4)
            {
                RangeInt rng = new RangeInt(1, 9);
                string conversion = rng.ToString();

                Vector3 enemyPos = Vector3.zero;

                //print(conversion);

                enemyPos = chunks[chunks.Count - 1].transform.Find("SpawnPoint" + conversion).position;

                GameObject enemyobj = Instantiate(Enemy, enemyPos, Quaternion.identity);

                enemies.Add(enemyobj);
            }

            
        }
    }
}
