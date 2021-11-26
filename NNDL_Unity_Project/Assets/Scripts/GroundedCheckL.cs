using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedCheckL : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            GetComponentInParent<NN_Brain>().grounded_l = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            GetComponentInParent<NN_Brain>().grounded_l = false;
        }
    }
}
