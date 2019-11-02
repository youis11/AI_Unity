using UnityEngine;
using System.Collections;

public class SteeringAlign : Steering {

    public float min_angle = 0.01f;
    public float slow_angle = 0.1f;
    public float time_to_target = 0.1f;

    Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
	}

	// Update is called once per frame
	void Update () 
	{
        // TODO 7: Very similar to arrive, but using angular velocities
        // Find the desired rotation and accelerate to it
        // Use Vector3.SignedAngle() to find the angle between two directions

        Vector3 targetDir = move.target.transform.position - transform.position;
        Vector3 forward = transform.forward;
        float angle = Vector3.SignedAngle(forward, targetDir, Vector3.up);

        float angularRotation = angle;

        if (Mathf.Abs(angle) < slow_angle)
        {
            float idealAngle = angle / time_to_target;
            angularRotation = idealAngle;

            if (Mathf.Abs(angle) < min_angle)
            {
                angularRotation = 0.0f;
                move.SetRotationVelocity(angularRotation,priority);
            }
        }

        move.AccelerateRotation(angularRotation,priority);
    }
}
