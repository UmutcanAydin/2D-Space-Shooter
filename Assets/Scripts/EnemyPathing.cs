using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] List<Transform> waypoints;
    [SerializeField] float moveSpeed = 2f;
    int waypointIndex = 1;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].position, moveSpeed * Time.deltaTime);
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
