using UnityEngine;
using System.Collections;

public class SteeringSeparation : MonoBehaviour {

	public LayerMask mask;
	public float search_radius = 5.0f;
	public AnimationCurve strength;

	Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// TODO 1: Agents much separate from each other:
		// 1- Find other agents in the vicinity (use a layer for all agents)
		// 2- For each of them calculate a escape vector using the AnimationCurve
		// 3- Sum up all vectors and trim down to maximum acceleration
	}

	void OnDrawGizmosSelected() 
	{
		// Display the explosion radius when selected
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, search_radius);
	}
}
