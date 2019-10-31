using UnityEngine;
using System.Collections;

public class SteeringFlee : Steering {

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
        if (!move)
            move = GetComponent<Move>();

        // TODO 2: Same as Steering seek but opposite direction
        Vector3 dir = transform.position - move.target.transform.position;

        Vector3 acc = dir * move.max_mov_acceleration;
        move.AccelerateMovement(acc,priority);
    }
}
