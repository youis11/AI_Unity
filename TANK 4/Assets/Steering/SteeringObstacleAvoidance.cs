using UnityEngine;
using System.Collections;


public class SteeringObstacleAvoidance : Steering {

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
    void Start () {
        move = GetComponent<Move>(); 
        seek = GetComponent<SteeringSeek>();
    }
    
    // Update is called once per frame
    void Update () 
    {
        // TODO 2: Agents must avoid any collider in their way
        // 1- Create your own (serializable) class for rays and make a public array with it
        // 2- Calculate a quaternion with rotation based on movement vector
        // 3- Cast all rays. If one hit, get away from that surface using the hitpoint and normal info
        // 4- Make sure there is debug draw for all rays (below in OnDrawGizmosSelected)

        //2 - Quaternion like move.cs
        float angle = Mathf.Atan2(move.mov_velocity.x, move.mov_velocity.z);
        Quaternion q = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

        //3- Cast all rays with foreach
        foreach (MyRay ray in rays)
        {
            RaycastHit hitInfo;

            if (Physics.Raycast(transform.position, q * ray.direction, out hitInfo, ray.length, mask))
            {
                //position + normal * length
                Vector3 awaySurface = hitInfo.point + hitInfo.normal * avoid_distance;
                seek.Steer(awaySurface);
            }
        }
    }

    void OnDrawGizmosSelected() 
    {
        if(move && this.isActiveAndEnabled)
        {
            Gizmos.color = Color.red;
            float angle = Mathf.Atan2(move.mov_velocity.x, move.mov_velocity.z);
            Quaternion q = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

            // TODO 2: Debug draw thoise rays (Look at Gizmos.DrawLine)

            //position + dir * length
            foreach (MyRay ray in rays)
                Gizmos.DrawLine(transform.position, transform.position + (q * ray.direction.normalized) * ray.length);
        }
    }
}

