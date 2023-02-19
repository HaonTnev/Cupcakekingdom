using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpointer : MonoBehaviour
{
    public List<GameObject> spawnpoints;
    private Transform currentSpawn;

    public bool active;

    void Awake()
    {
        if (active)
        {
            currentSpawn = spawnpoints[Random.Range(0, spawnpoints.Count)].GetComponent<Transform>();
            transform.position = currentSpawn.position;
        }
    }
}
