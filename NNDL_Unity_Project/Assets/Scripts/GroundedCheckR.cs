using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedCheckR : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            GetComponentInParent<NN_Brain>().grounded_r = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            GetComponentInParent<NN_Brain>().grounded_r = false;
        }
    }
}
