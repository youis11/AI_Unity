
using UnityEngine;
using System.Collections;

public class SteeringSeparation : Steering
{
    public LayerMask mask;
    public float search_radius = 5.0f;
    public AnimationCurve falloff;

    Move move;

    // Use this for initialization
    void Start()
    {
        move = GetComponent<Move>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO 1: Agents much separate from each other:
        // 1- Find other agents in the vicinity (use a layer for all agents)
        // 2- For each of them calculate a escape vector using the AnimationCurve
        // 3- Sum up all vectors and trim down to maximum acceleration

        Collider[] colliders = Physics.OverlapSphere(transform.position, search_radius, mask);
        Vector3 escapeVectorsSum = Vector3.zero;

        foreach (Collider col in colliders)
        {
            Debug.Log("Collision!");

            Vector3 escapeVector = transform.position - col.transform.position;
            escapeVectorsSum += escapeVector;
        }

        escapeVectorsSum.y = 0;

        if (colliders.Length > 1)
            escapeVectorsSum /= colliders.Length;

        float t = escapeVectorsSum.magnitude / search_radius;
        float escapeForce = falloff.Evaluate(t);

        escapeVectorsSum.Normalize();
        escapeVectorsSum *= escapeForce;

        if (escapeVectorsSum.magnitude > move.max_mov_acceleration)
        {
            escapeVectorsSum.Normalize();
            escapeVectorsSum *= move.max_mov_acceleration;
        }

        if (escapeVectorsSum.magnitude > 0)
            move.AccelerateMovement(escapeVectorsSum,priority);
    }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, search_radius);
    }
}