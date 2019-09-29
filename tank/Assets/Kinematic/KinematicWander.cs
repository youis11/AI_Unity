using UnityEngine;
using System.Collections;

public class KinematicWander : MonoBehaviour {

	public float max_angle = 0.5f;
	Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
	}

    float RandomBinominal()
    {
        return Random.value * Random.value;
    }

    // Update is called once per frame
    void Update () 
	{
        // TODO 9: Generate a velocity vector in a random rotation (use RandomBinominal) and some 
        //attenuation factor
        float angle = RandomBinominal() * max_angle;
        Vector3 vel = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up) * Vector3.forward;
        vel *= move.max_mov_velocity;

        move.SetMovementVelocity(vel);

    }
}
