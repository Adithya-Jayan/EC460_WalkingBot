using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_detector : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            GetComponentInParent<NN_Brain>().collided = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            GetComponentInParent<NN_Brain>().collided = false;
        }
    }
}
