using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;

public class FollowCurve : MonoBehaviour
{
    private float ratio;
    public BGCcMath path;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //increase distance

        if(ratio < 1.0f)
            ratio += 0.1f * Time.deltaTime;

        if (ratio >= 1.0f)
            ratio = 0.0f;

        //calculate position and tangent
        transform.position = path.CalcPositionByDistanceRatio(ratio);
        //this is a version for 3D. For 2D, comment this line and uncomment the next one
        //transform.rotation = Quaternion.LookRotation(tangent);
        //ObjectToMove.rotation = Quaternion.AngleAxis(Mathf.Atan2(tangent.y, tangent.x) * Mathf.Rad2Deg, Vector3.forward);
    }
}
