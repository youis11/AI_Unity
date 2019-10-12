using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class SteeringConfig
{
    public const int priority_num = 5;
}

abstract public class Steering : MonoBehaviour
{
    [Range(0, SteeringConfig.priority_num)]
    public int priority = 0;
}
