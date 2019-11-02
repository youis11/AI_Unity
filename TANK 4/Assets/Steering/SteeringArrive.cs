using UnityEngine;
using System.Collections;

public class SteeringArrive : Steering
{

    public float min_distance = 0.1f;
    public float slow_distance = 5.0f;
    public float time_to_accel = 1.0f;

    Move move;

    // Use this for initialization
    void Start()
    {
        move = GetComponent<Move>();
    }

    // Update is called once per frame
    void Update()
    {
        Steer(move.target.transform.position);
    }

    public void Steer(Vector3 target)
    {
        if (!move)
            move = GetComponent<Move>();

        Vector3 diff = target - transform.position;
        float diff_magnitude = diff.magnitude;

        diff.Normalize();
        diff *= move.max_mov_acceleration;

        Vector3 desired_acc = diff;

        if (diff_magnitude < slow_distance)
        {
            Vector3 desired_vel = diff.normalized * diff_magnitude * time_to_accel;

            if (diff_magnitude < min_distance)
                desired_vel = Vector3.zero;

            desired_acc = desired_vel - move.current_velocity;
        }

        move.AccelerateMovement(desired_acc, priority);
    }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, min_distance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, slow_distance);
    }
}