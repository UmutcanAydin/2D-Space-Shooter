using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject path;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    public GameObject getEnemy()
    {
        return enemy;
    }

    public List<Transform> getWaypoints()
    {
        var waypoints = new List<Transform>();
        foreach (Transform waypoint in path.transform)
        {

            waypoints.Add(waypoint);
        }
        return waypoints;
    }
    
    public float getTimeBetweenSpawns()
    {
        return timeBetweenSpawns;
    }

    public float getSpawnRandomFactor()
    {
        return spawnRandomFactor;
    }

    public int getNumOfEnemies()
    {
        return numOfEnemies;
    }

    public float getMoveSpeed()
    {
        return moveSpeed;
    }
}
