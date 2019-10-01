using UnityEngine;
using System.Collections;

public class SteeringArrive : MonoBehaviour {

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

        // TODO 3: Find the acceleration to achieve the desired velocity
        // If we are close enough to the target just stop moving and do nothing else
        // Calculate the desired acceleration using the velocity we want to achieve and the one we already have
        // Use time_to_target as the time to transition from the current velocity to the desired velocity
        // Clamp the desired acceleration and call move.AccelerateMovement()

        Vector3 diff = move.target.transform.position - transform.position;

        if (min_distance > diff.magnitude)
            move.SetMovementVelocity(Vector3.zero);
        else
        {
            Vector3 diff_norm = diff.normalized * move.max_mov_speed;

            if (slow_distance > diff.magnitude)
                diff_norm = diff_norm * (diff.magnitude/ slow_distance);

            Vector3 desired_acc = diff_norm - move.movement;
            desired_acc /= time_to_accel;
            if (desired_acc.magnitude > move.max_mov_acceleration)
                desired_acc = desired_acc.normalized * move.max_mov_acceleration;

             move.AccelerateMovement(desired_acc);

        }


        //TODO 4: Add a slow factor to reach the target
        // Start slowing down when we get closer to the target
        // Calculate a slow factor (0 to 1 multiplier to desired velocity)
        // Once inside the slow radius, the further we are from it, the slower we go



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
