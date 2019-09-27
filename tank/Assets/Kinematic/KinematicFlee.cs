using UnityEngine;
using System.Collections;

public class KinematicFlee : MonoBehaviour {

	Move move;
    public Vector3 dir;

    // Use this for initialization
    void Start () {
		move = GetComponent<Move>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        // TODO 6: To create flee just switch the direction to go
        dir = transform.position - move.target.transform.position;
        move.SetMovementVelocity(move.max_mov_velocity * dir.normalized);
    }
}
