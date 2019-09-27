using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Move : MonoBehaviour {

	public GameObject target;
	public GameObject aim;
	public Slider arrow;
	public float max_mov_velocity = 5.0f;

    KinematicSeek seek;

	public Vector3 mov_velocity = Vector3.zero;

    private Vector3 tank;

	// Use this for initialization
	public void SetMovementVelocity (Vector3 vel) {
		mov_velocity = vel;
	}

    private void Start()
    {
        seek = GetComponent<KinematicSeek>();
        tank = transform.position;
        
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
        float angle = Mathf.Rad2Deg * Mathf.Atan2(seek.dir.x, seek.dir.z);

        aim.transform.rotation = Quaternion.AngleAxis(angle,Vector3.up);

        // TODO 4: stretch it the arrow (arrow.value) to show how fast the tank is getting push in
        // that direction. Adjust with some factor so the arrow is visible.
        arrow.value = mov_velocity.magnitude + 12.0f;

        

        // TODO 5: update tank position based on final mov_velocity and deltatime
        transform.position = transform.position + (mov_velocity + seek.dir) * Time.deltaTime;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);

        // Reset movement to 0 to simplify things ...
        mov_velocity = Vector3.zero;
	}
}
