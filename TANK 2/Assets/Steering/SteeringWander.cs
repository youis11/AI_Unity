using UnityEngine;
using System.Collections;

public class SteeringWander : MonoBehaviour {

	public float min_distance = 0.1f;
	public float time_to_target = 0.25f;

	Move move;
    SteeringSeek seek;

    public float wanderRate = 0.1f;
    public float distanceToCircle = 4.0f;
    public float circleRadius = 1.0f;

    private float timer = 0.0f;
    private Vector3 target = Vector3.zero;


    // Use this for initialization
    void Start () {
		move = GetComponent<Move>();
        seek = GetComponent<SteeringSeek>();
        timer = wanderRate;
    }

	// Update is called once per frame
	void Update () 
	{
        if (timer >= wanderRate)
        {
            // Update the target
            Vector3 randomDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));
            randomDirection.Normalize();

            Vector3 circlePosition = transform.position + transform.forward * distanceToCircle;
            target = circlePosition + randomDirection * circleRadius;

            timer = 0.0f;
        }
        
        timer += Time.deltaTime;

        seek.Steer(move.target.transform.position);

    }

    void OnDrawGizmosSelected() 
	{
		// Display the explosion radius when selected
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position, min_distance);
	}
}
