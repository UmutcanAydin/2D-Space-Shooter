using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.getWaypoints();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void setWaveConfig(WaveConfig newWaveConfig)
    {
        waveConfig = newWaveConfig;
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].position, waveConfig.getMoveSpeed() * Time.deltaTime);
        if (transform.position == waypoints[waypointIndex].position && waypointIndex < waypoints.Count - 1)
        {
            waypointIndex++;
        }
        else if (transform.position == waypoints[waypoints.Count - 1].position && waypointIndex == waypoints.Count - 1)
        {
            Destroy(gameObject);
        }
    }
}
