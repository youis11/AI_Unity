using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveDude : MonoBehaviour
{
    Animator Animator;
    NavMeshAgent Agent;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<MoveToMouseClick>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Agent.SetDestination(target.position);
        if((transform.position - target.position).magnitude < 2)
        {
            Animator.SetBool("moving", false);
        }
        else
        {
            Animator.SetBool("moving", true);
        }
    }
}
