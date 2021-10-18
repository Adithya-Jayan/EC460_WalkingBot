using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker_AI : MonoBehaviour
{

    public GameObject LowerLeg_L;
    public GameObject LowerLeg_R;
    public GameObject UpperLeg_L;
    public GameObject UpperLeg_R;

    private struct Limb 
    {
        public Vector3 velocity;
        public Vector3 rotation;
    };

    Limb Upper_L;
    Limb Upper_R;
    Limb Lower_L;
    Limb Lower_R;
    float currentTime = 0f;
    int i;

    // Start is called before the first frame update
    void Start()
    {
        i = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var LowerLeg_L_motor = LowerLeg_L.GetComponent<HingeJoint>().motor;
        var LowerLeg_R_motor = LowerLeg_R.GetComponent<HingeJoint>().motor;
        var UpperLeg_L_motor = UpperLeg_L.GetComponent<HingeJoint>().motor;
        var UpperLeg_R_motor = UpperLeg_R.GetComponent<HingeJoint>().motor;

        Lower_L.velocity  = LowerLeg_L.GetComponent<Rigidbody>().angularVelocity;
        Debug.Log(Lower_L.velocity);
        
        if(Mathf.Round(Time.time)%10 == 0 & (Time.time - currentTime > 1))
        {
            Debug.Log(LowerLeg_L_motor.targetVelocity);

            LowerLeg_L_motor.targetVelocity = 15*i;
            LowerLeg_L.GetComponent<HingeJoint>().motor = LowerLeg_L_motor;
            i = -1 * i;
            currentTime = Time.time;
        }
            

    }
}
