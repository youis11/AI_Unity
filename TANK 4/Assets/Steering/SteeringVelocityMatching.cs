using UnityEngine;
using System.Collections;

public class SteeringVelocityMatching : Steering {

	public float time_to_accel = 0.25f;

	Move move;
	Move target_move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
		target_move = move.target.GetComponent<Move>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(target_move)
		{
            // TODO 8: First come up with your ideal velocity
            // then accelerate to it.

            //similar to steeringArrive
            Vector3 ideal_velocity = target_move.current_velocity;

            Vector3 accel = ideal_velocity - move.current_velocity;
            accel = accel / time_to_accel;

            if (accel.magnitude > move.max_mov_acceleration)
                accel = accel.normalized * move.max_mov_acceleration;

            move.AccelerateMovement(accel,priority);
        }
	}
}
