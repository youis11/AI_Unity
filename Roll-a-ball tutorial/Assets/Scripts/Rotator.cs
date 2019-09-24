using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public GameObject player;
    
    private bool come = false;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = new Vector3(15, 30, 45);
        transform.Rotate(rotation * Time.deltaTime);

        int velocity = 0;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (come == true)
            {
                come = false;
            }

            else
            {
                come = true;
            }
        }
       

        if (come == false)
        {
            velocity = 5;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, velocity * Time.deltaTime);
        }
        else
        {
            velocity = -5;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, velocity * Time.deltaTime);
        }
    }
}