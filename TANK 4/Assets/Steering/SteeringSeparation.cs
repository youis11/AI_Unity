using UnityEngine;
using System.Collections;

public class SteeringSeparation : Steering {

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

        //1 Physics.OverlapSphere 
        Collider[] colliders = Physics.OverlapSphere(transform.position, search_radius, mask);

        //2 foreach col in colliders
        Vector3 escapeVectors = Vector3.zero;

        foreach (Collider col in colliders)
        {
            Vector3 escapeVector = transform.position - col.transform.position;
            escapeVectors = escapeVectors + escapeVector;
        }

        escapeVectors.y = 0;

        if (colliders.Length > 1)
            escapeVectors = escapeVectors / colliders.Length;

        //3 call evaluate using magnitude and radius
        float time = escapeVectors.magnitude / search_radius;
        float escapeForce = strength.Evaluate(time);
        escapeVectors = escapeVectors.normalized * escapeForce;

        //4 cap and call acceleratemovement
        if (escapeVectors.magnitude > 0)
        {
            if (escapeVectors.magnitude > move.max_mov_acceleration)
                escapeVectors = escapeVectors.normalized * move.max_mov_acceleration;

            move.AccelerateMovement(escapeVectors,priority);
        }
    }

	void OnDrawGizmosSelected() 
	{
		// Display the explosion radius when selected
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, search_radius);
	}
}
