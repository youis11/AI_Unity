using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Move : MonoBehaviour {

	public GameObject target;
	public GameObject aim;
	public Slider arrow;
	public float max_mov_velocity = 5.0f;
    public float rotation = 0.0f; // degrees

    KinematicSeek seek;
    KinematicFlee flee;

	public Vector3 mov_velocity = Vector3.zero;

    private Vector3 tank;

    public void SetRotationVelocity(float rotation_velocity)
    {
        rotation = rotation_velocity;
    }

    // Use this for initialization
    public void SetMovementVelocity (Vector3 vel) {
		mov_velocity = vel;
	}

    private void Start()
    {
        seek = GetComponent<KinematicSeek>();
        tank = transform.position;
        flee = GetComponent<KinematicFlee>();
    }

    // Update is called once per frame
    void Update () 
	{

        transform.position = new Vector3(transform.position.x, tank.y, transform.position.z);

        // TODO 2: Make sure mov_velocity is never bigger that max_mov_velocity
        if (mov_velocity.magnitude > max_mov_velocity)
        {
            mov_velocity = mov_velocity.normalized * max_mov_velocity;
        }

        // TODO 3: rotate the arrow to point to mov_velocity direction. First find out the angle
        // then create a Quaternion with that expressed that rotation and apply it to aim.transform
        float angle = Mathf.Rad2Deg * Mathf.Atan2(mov_velocity.x, mov_velocity.z);

        aim.transform.rotation = Quaternion.AngleAxis(angle,Vector3.up);

        // TODO 4: stretch it the arrow (arrow.value) to show how fast the tank is getting push in
        // that direction. Adjust with some factor so the arrow is visible.
        arrow.value = mov_velocity.magnitude + 4.0f;

        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);

        // TODO 5: update tank position based on final mov_velocity and deltatime
        transform.position = transform.position + (mov_velocity * Time.deltaTime );

        // Reset movement to 0 to simplify things ...
        mov_velocity = Vector3.zero;
	}
}
