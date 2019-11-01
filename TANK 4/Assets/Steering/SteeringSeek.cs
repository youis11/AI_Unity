using UnityEngine;
using System.Collections;

public class SteeringSeek : Steering {

	Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>(); 
	}
	
	// Update is called once per frame
	void Update () 
	{
		Steer(move.target.transform.position,priority);
	}

	public void Steer(Vector3 target, int priority)
	{
		if(!move)
			move = GetComponent<Move>();

        // TODO 1: accelerate towards our target at max_acceleration
        // use move.AccelerateMovement()
        Vector3 dir = target - transform.position;

        Vector3 acc = dir.normalized * move.max_mov_acceleration;
        move.AccelerateMovement(acc,priority);
    }
}
