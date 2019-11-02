using UnityEngine;
using System.Collections;

public class SteeringWander : Steering
{
    public Vector3 offset = Vector3.zero;
    public float radius = 5.0f;

    private float timer = 2.5f;

    public float update_time = 0.0f;
    public float update_time_min = 2.0f;
    public float update_time_max = 5.0f;

    SteeringSeek seek;
    Vector3 random_point;

    // Use this for initialization
    void Start()
    {
        seek = GetComponent<SteeringSeek>();
        NewDirection();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= update_time)
        {
            NewDirection();
            timer = 0.0f;
        }
    }

    // Update is called once per frame
    void NewDirection()
    {
        update_time = Random.Range(update_time_min, update_time_max);

        random_point = Random.insideUnitSphere;
        random_point *= radius;
        random_point += transform.position + offset;
        random_point.y = transform.position.y;

        seek.Steer(random_point, priority);
    }

    void OnDrawGizmosSelected()
    {
        if (isActiveAndEnabled)
        {
            // Display the explosion radius when selected
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.TransformPoint(offset), radius);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(random_point, 0.2f);
        }
    }
}