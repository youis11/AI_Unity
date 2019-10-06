using UnityEngine;
using System.Collections;

public class SteeringObstacleAvoidance : MonoBehaviour {

	public LayerMask mask;
	public float avoid_distance = 5.0f;


	Move move;
	SteeringSeek seek;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>(); 
		seek = GetComponent<SteeringSeek>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// TODO 2: Agents must avoid any collider in their way
		// 1- Create your own (serializable) class for rays and make a public array with it
		// 2- Calculate a quaternion with rotation based on movement vector
		// 3- Cast all rays. If one hit, get away from that surface using the hitpoint and normal info
		// 4- Make sure there is debug draw for all rays (below in OnDrawGizmosSelected)
	}

	void OnDrawGizmosSelected() 
	{
		if(move && this.isActiveAndEnabled)
		{
			Gizmos.color = Color.red;
			float angle = Mathf.Atan2(move.movement_vel.x, move.movement_vel.z);
			Quaternion q = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

			// TODO 2: Debug draw thoise rays (Look at Gizmos.DrawLine)

		}
	}
}
