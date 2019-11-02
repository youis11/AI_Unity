using UnityEngine;
using System.Collections;

public class SteeringArrive : Steering {

    public float min_distance = 0.1f;
    public float slow_distance = 5.0f;
    public float time_to_accel = 0.1f;

    Move move;

	// Use this for initialization
	void Start () { 
		move = GetComponent<Move>();
	}

	// Update is called once per frame
	void Update () 
	{
		Steer(move.target.transform.position);
	}

	public void Steer(Vector3 target)
	{
		if(!move)
			move = GetComponent<Move>();

        // Velocity we are trying to match
        // TODO 3: Find the acceleration to achieve the desired velocity
        // If we are close enough to the target just stop moving and do nothing else
        // Calculate the desired acceleration using the velocity we want to achieve and the one we already have
        // Use time_to_target as the time to transition from the current velocity to the desired velocity
        // Clamp the desired acceleration and call move.AccelerateMovement()

        Vector3 diff = target - transform.position;

        if (min_distance > diff.magnitude)
            move.SetMovementVelocity(Vector3.zero,priority);
        else
        {
            Vector3 diff_norm = diff.normalized * move.max_mov_velocity;

            if (slow_distance > diff.magnitude)
                diff_norm = diff_norm * (diff.magnitude / slow_distance);

            Vector3 desired_acc = diff_norm - move.current_velocity;
            desired_acc /= time_to_accel;
            if (desired_acc.magnitude > move.max_mov_acceleration)
                desired_acc = desired_acc.normalized * move.max_mov_acceleration;

            move.AccelerateMovement(desired_acc,priority);

        }
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
