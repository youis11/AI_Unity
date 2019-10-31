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
    public float max_rot_speed = 10.0f; // in degrees / second
    public float max_rot_acceleration = 0.1f; // in degrees

    [Header("-------- Read Only --------")]
    public Vector3 current_velocity = Vector3.zero;
    public float current_rotation_speed = 0.0f; // degrees

    //NEW
    Vector3[] movementVelocity = new Vector3[SteeringConfig.priority_num];
    float[] angularVelocity = new float[SteeringConfig.priority_num];

    // Methods for behaviours to set / add velocities
    public void SetMovementVelocity(Vector3 velocity)
    {
        current_velocity = velocity;
    }

    //NEW
    public void AccelerateMovement(Vector3 acceleration, int priority)
    {
        movementVelocity[priority] = acceleration;
    }

    public void SetRotationVelocity(float rotation_speed)
    {
        current_rotation_speed = rotation_speed;
    }

    //NEW
    public void AccelerateRotation(float rotation_acceleration, int priority)
    {
        angularVelocity[priority] = rotation_acceleration;
    }

    // Update is called once per frame
    void Update()
    {
        //NEW
        for (int i = 0; i < movementVelocity.Length; ++i)
        {
            if (!Mathf.Approximately(movementVelocity[i].magnitude, 0.0f))
            {
                current_velocity = movementVelocity[i];
                break;
            }
        }

        //NEW
        for (int i = 0; i < angularVelocity.Length; ++i)
        {
            if (!Mathf.Approximately(angularVelocity[i], 0.0f))
            {
                current_rotation_speed = angularVelocity[i];
                break;
            }
        }

        // cap velocity
        if (current_velocity.magnitude > max_mov_velocity)
        {
            current_velocity = current_velocity.normalized * max_mov_velocity;
        }

        // cap rotation
        current_rotation_speed = Mathf.Clamp(current_rotation_speed, -max_rot_speed, max_rot_speed);

        // rotate the arrow
        float angle = Mathf.Atan2(current_velocity.x, current_velocity.z);
        aim.transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

        // strech it
        arrow.value = current_velocity.magnitude * 4;

        // final rotate
        transform.rotation *= Quaternion.AngleAxis(current_rotation_speed * Time.deltaTime, Vector3.up);

        // finally move
        transform.position += current_velocity * Time.deltaTime;

        //NEW
        for (int i = 0; i < SteeringConfig.priority_num; ++i)
        {
            movementVelocity[i] = Vector3.zero;
            angularVelocity[i] = 0.0f;
        }
    }
}