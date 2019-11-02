using UnityEngine;
using System.Collections;

public class SteeringObstacleAvoidance : Steering
{
    public LayerMask mask;
    public float avoid_distance = 5.0f;

    Move move;
    SteeringSeek seek;

    [System.Serializable]
    public class MyRay
    {
        public Vector3 direction;
        public float length;
    }

    public MyRay[] rays;

    // Use this for initialization
    void Start()
    {
        move = GetComponent<Move>();
        seek = GetComponent<SteeringSeek>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO 2: Agents must avoid any collider in their way
        // 1- Create your own (serializable) class for rays and make a public array with it
        // 2- Calculate a quaternion with rotation based on movement vector
        // 3- Cast all rays. If one hit, get away from that surface using the hitpoint and normal info
        // 4- Make sure there is debug draw for all rays (below in OnDrawGizmosSelected)

        float angle = Mathf.Atan2(move.current_velocity.x, move.current_velocity.z);
        Quaternion q = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

        foreach (MyRay ray in rays)
        {
            Vector3 newRay = q * ray.direction;

            RaycastHit hitInfo;
            if (Physics.Raycast(new Vector3(transform.position.x, 1.0f, transform.position.z), newRay, out hitInfo, ray.length, mask))
            {
                Vector3 escapeTargetPosition = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z) + hitInfo.normal * avoid_distance;
                seek.Steer(escapeTargetPosition,priority);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (move && this.isActiveAndEnabled)
        {
            float angle = Mathf.Atan2(move.current_velocity.x, move.current_velocity.z);
            Quaternion q = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

            foreach (MyRay ray in rays)
            {
                Vector3 newRay = q * ray.direction;

                // TODO 2: Debug draw thoise rays (Look at Gizmos.DrawLine)
                Debug.DrawRay(transform.position, newRay * ray.length, Color.red);
            }
        }
    }
}

