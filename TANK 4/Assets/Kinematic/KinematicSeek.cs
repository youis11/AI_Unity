using UnityEngine;
using System.Collections;

public class KinematicSeek : MonoBehaviour {

    Move move;
    public Vector3 dir;

    // Use this for initialization
    void Start () {
		move = GetComponent<Move>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        // TODO 1: Set movement velocity to max speed in the direction of the target
        dir = move.target.transform.position - transform.position;
        move.SetMovementVelocity(move.max_mov_velocity * dir.normalized);

        // Remember to enable this component in the inspector
    }
}
