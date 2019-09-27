using UnityEngine;
using System.Collections;

public class KinematicWander : MonoBehaviour {

	public float max_angle = 0.5f;

	Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
	}

	// Update is called once per frame
	void Update () 
	{
		// TODO 9: Generate a velocity vector in a random rotation (use RandomBinominal) and some attenuation factor
	}
}
