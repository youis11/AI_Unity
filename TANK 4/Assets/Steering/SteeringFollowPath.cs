using UnityEngine;
using System.Collections;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;

public class SteeringFollowPath : Steering {

	Move move;
	SteeringSeek seek;

    public float ratio_increment = 0.1f;
    public float min_distance = 1.0f;
    float current_ratio = 0.0f;

    public BGCcMath path;
    Vector3 closestPoint;

    // Use this for initialization
    void Start () {
		move = GetComponent<Move>();
		seek = GetComponent<SteeringSeek>();

        // TODO 1: Calculate the closest point from the tank to the curve
        float distance;
        closestPoint = path.CalcPositionByClosestPoint(transform.position, out distance);

        current_ratio = distance / path.GetDistance();
    }
	
	// Update is called once per frame
	void Update () 
	{
        // TODO 2: Check if the tank is close enough to the desired point
        // If so, create a new point further ahead in the path
        Vector3 target = closestPoint - transform.position;

        if (target.magnitude < min_distance)
        {
            current_ratio += ratio_increment;

            if (current_ratio >= 1.0f)
                current_ratio = 0.0f;

            closestPoint = path.CalcPositionByDistanceRatio(current_ratio);
        }

        seek.Steer(closestPoint,priority);
    }

	void OnDrawGizmosSelected() 
	{

		if(isActiveAndEnabled)
		{
			// Display the explosion radius when selected
			Gizmos.color = Color.green;
			// Useful if you draw a sphere were on the closest point to the path
		}

	}
}
