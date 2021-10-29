using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NN_Brain : MonoBehaviour
{
    public GameObject LowerLeg_L;
    public GameObject LowerLeg_R;
    public GameObject UpperLeg_L;
    public GameObject UpperLeg_R;
    public GameObject Torso;

    int[] Weights_shape = {30, 25, 20, 16, 8, 4 };

    float[,] Weights;

    float[] Read_inputs()
    {
        List<float> Input = new List<float>();

        Input.Add(LowerLeg_L.GetComponent<Rigidbody>().angularVelocity.x);
        Input.Add(LowerLeg_L.GetComponent<Rigidbody>().angularVelocity.y);
        Input.Add(LowerLeg_L.GetComponent<Rigidbody>().angularVelocity.z);

        Input.Add(LowerLeg_R.GetComponent<Rigidbody>().angularVelocity.x);
        Input.Add(LowerLeg_R.GetComponent<Rigidbody>().angularVelocity.y);
        Input.Add(LowerLeg_R.GetComponent<Rigidbody>().angularVelocity.z);

        Input.Add(UpperLeg_L.GetComponent<Rigidbody>().angularVelocity.x);
        Input.Add(UpperLeg_L.GetComponent<Rigidbody>().angularVelocity.y);
        Input.Add(UpperLeg_L.GetComponent<Rigidbody>().angularVelocity.z);

        Input.Add(UpperLeg_R.GetComponent<Rigidbody>().angularVelocity.x);
        Input.Add(UpperLeg_R.GetComponent<Rigidbody>().angularVelocity.y);
        Input.Add(UpperLeg_R.GetComponent<Rigidbody>().angularVelocity.z);

        Input.Add(Torso.GetComponent<Rigidbody>().angularVelocity.x);
        Input.Add(Torso.GetComponent<Rigidbody>().angularVelocity.y);
        Input.Add(Torso.GetComponent<Rigidbody>().angularVelocity.z);

        Input.Add(LowerLeg_L.GetComponent<Rigidbody>().velocity.x);
        Input.Add(LowerLeg_L.GetComponent<Rigidbody>().velocity.y);
        Input.Add(LowerLeg_L.GetComponent<Rigidbody>().velocity.z);

        Input.Add(LowerLeg_R.GetComponent<Rigidbody>().velocity.x);
        Input.Add(LowerLeg_R.GetComponent<Rigidbody>().velocity.y);
        Input.Add(LowerLeg_R.GetComponent<Rigidbody>().velocity.z);

        Input.Add(UpperLeg_L.GetComponent<Rigidbody>().velocity.x);
        Input.Add(UpperLeg_L.GetComponent<Rigidbody>().velocity.y);
        Input.Add(UpperLeg_L.GetComponent<Rigidbody>().velocity.z);

        Input.Add(UpperLeg_R.GetComponent<Rigidbody>().velocity.x);
        Input.Add(UpperLeg_R.GetComponent<Rigidbody>().velocity.y);
        Input.Add(UpperLeg_R.GetComponent<Rigidbody>().velocity.z);

        Input.Add(Torso.GetComponent<Rigidbody>().velocity.x);
        Input.Add(Torso.GetComponent<Rigidbody>().velocity.y);
        Input.Add(Torso.GetComponent<Rigidbody>().velocity.z);

        return Input.ToArray();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float[] Inputs = Read_inputs();

        float[] Layer_input = Inputs;
        float[] Layer_output;
   
        //Propogate Inputs through layers
        for (int i = 1; i < Weights_shape.Length; i++)
        {
            for (int j = 0; j < Weights_shape[i-1]; j++)
            {
                for (int p = 0; p < Layer_input.Length; p++)
                {
                    
                }
            }
        }

    }
}
