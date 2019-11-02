using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Move : MonoBehaviour
{

    public GameObject target;
    public GameObject aim;
    public Slider arrow;
    public float max_mov_velocity = 5.0f;
    public float max_mov_acceleration = 0.1f;
    public float max_rot_velocity = 10.0f; // in degrees / second
    public float max_rot_acceleration = 0.1f; // in degrees

    [Header("-------- Read Only --------")]
    public Vector3 current_velocity = Vector3.zero;
    public float rotation = 0.0f; // degrees

    private Vector3[] movement_velocity;
    private float[] angular_velocity;

    public void Start()
    {
        movement_velocity = new Vector3[SteeringConfig.priority_num];
        angular_velocity = new float[SteeringConfig.priority_num];

        ResetPriorities();
    }

    // Methods for behaviours to set / add velocities
    public void SetMovementVelocity(Vector3 velocity, int priority)
    {
        movement_velocity[priority] = velocity;
    }

    public void AccelerateMovement(Vector3 velocity, int priority)
    {
        movement_velocity[priority] += velocity;
    }

    public void SetRotationVelocity(float rotation_velocity, int priority)
    {
        angular_velocity[priority] = rotation_velocity;
    }

    public void AccelerateRotation(float rotation_acceleration, int priority)
    {
        angular_velocity[priority] += rotation_acceleration;
    }

    public void ResetPriorities()
    {
        for (int i = 0; i < SteeringConfig.priority_num; ++i)
        {
            movement_velocity[i] = Vector3.zero;
            angular_velocity[i] = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 new_movement_velocity = Vector3.zero;
        float new_angular_velocity = 0.0f;

        // Pick the lowest priority level
        foreach (Vector3 v in movement_velocity)
        {
            if (Mathf.Approximately(v.magnitude, 0.0f) == false)
            {
                new_movement_velocity = v;
                break;
            }
        }


        foreach (float f in angular_velocity)
        {
            if (Mathf.Approximately(f, 0.0f) == false)
            {
                new_angular_velocity = f;
                break;
            }
        }


        // Apply
        current_velocity += new_movement_velocity;
        rotation += new_angular_velocity;

        // cap velocity
        if (current_velocity.magnitude > max_mov_velocity)
        {
            current_velocity.Normalize();
            current_velocity *= max_mov_velocity;
        }

        // cap rotation
        Mathf.Clamp(rotation, -max_rot_velocity, max_rot_velocity);

        // rotate the arrow
        float angle = Mathf.Atan2(current_velocity.x, current_velocity.z);
        aim.transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

        // strech it
        arrow.value = current_velocity.magnitude * 4;

        // final rotate
        transform.rotation *= Quaternion.AngleAxis(rotation * Time.deltaTime, Vector3.up);

        // finally move
        transform.position += current_velocity * Time.deltaTime;

        ResetPriorities();

    }
}