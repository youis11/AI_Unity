using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringEvade : Steering
{
    public float max_seconds_prediction;

    Move move;
    SteeringFlee flee;

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Move>();
        flee = GetComponent<SteeringFlee>();
    }

    // Update is called once per frame
    void Update()
    {
        Steer(move.target.transform.position, move.target.GetComponent<Move>().current_velocity);
    }

    public void Steer(Vector3 target, Vector3 target_velocity)
    {
        // HOMEWORK: Create a fake position to represent
        // enemies predicted movement. Then call Steer()
        // on our Steering Flee

        Vector3 diff = transform.position - move.target.transform.position;

        float distance = diff.magnitude;
        float speed = move.current_velocity.magnitude;
        float fake_prediction;

        if (speed < distance / max_seconds_prediction)
            fake_prediction = max_seconds_prediction;
        else
            fake_prediction = distance / speed;

        flee.Steer(move.target.transform.position + (target_velocity * fake_prediction));
    }
}
