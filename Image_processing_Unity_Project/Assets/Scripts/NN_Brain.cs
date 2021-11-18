using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NN_Brain : MonoBehaviour
{
    public bool wait = true;
    public bool Completed = false;

    public GameObject LowerLeg_L;
    public GameObject LowerLeg_R;
    public GameObject UpperLeg_L;
    public GameObject UpperLeg_R;
    public GameObject Torso;
    double Duration;

    int[] Weights_shape = {30, 25, 20, 16, 8, 4 };
    float[] Weights;
    public float score = 0;
    float TimeSinceBirth = 0f;
    public bool collided = false;
    float penalty;

    public void Init(DNA dna, double duration, float negative_score)
    {

        //Assign weights
        Weights = dna.Genes;

        penalty = negative_score;

        Duration = duration;

        //Start brain function
        wait = false;
    }


    List<float> Read_inputs()
    {
        List<float> Input = new List<float>
        {
        LowerLeg_L.GetComponent<Rigidbody>().angularVelocity.x,
        LowerLeg_L.GetComponent<Rigidbody>().angularVelocity.y,
        LowerLeg_L.GetComponent<Rigidbody>().angularVelocity.z,

        LowerLeg_R.GetComponent<Rigidbody>().angularVelocity.x,
        LowerLeg_R.GetComponent<Rigidbody>().angularVelocity.y,
        LowerLeg_R.GetComponent<Rigidbody>().angularVelocity.z,

        UpperLeg_L.GetComponent<Rigidbody>().angularVelocity.x,
        UpperLeg_L.GetComponent<Rigidbody>().angularVelocity.y,
        UpperLeg_L.GetComponent<Rigidbody>().angularVelocity.z,

        UpperLeg_R.GetComponent<Rigidbody>().angularVelocity.x,
        UpperLeg_R.GetComponent<Rigidbody>().angularVelocity.y,
        UpperLeg_R.GetComponent<Rigidbody>().angularVelocity.z,

        Torso.GetComponent<Rigidbody>().angularVelocity.x,
        Torso.GetComponent<Rigidbody>().angularVelocity.y,
        Torso.GetComponent<Rigidbody>().angularVelocity.z,

        LowerLeg_L.GetComponent<Rigidbody>().velocity.x,
        LowerLeg_L.GetComponent<Rigidbody>().velocity.y,
        LowerLeg_L.GetComponent<Rigidbody>().velocity.z,

        LowerLeg_R.GetComponent<Rigidbody>().velocity.x,
        LowerLeg_R.GetComponent<Rigidbody>().velocity.y,
        LowerLeg_R.GetComponent<Rigidbody>().velocity.z,

        UpperLeg_L.GetComponent<Rigidbody>().velocity.x,
        UpperLeg_L.GetComponent<Rigidbody>().velocity.y,
        UpperLeg_L.GetComponent<Rigidbody>().velocity.z,

        UpperLeg_R.GetComponent<Rigidbody>().velocity.x,
        UpperLeg_R.GetComponent<Rigidbody>().velocity.y,
        UpperLeg_R.GetComponent<Rigidbody>().velocity.z,

        Torso.GetComponent<Rigidbody>().velocity.x,
        Torso.GetComponent<Rigidbody>().velocity.y,
        Torso.GetComponent<Rigidbody>().velocity.z
        };

        return Input;
    }

    private void ApplyTorque(List<float> activations)
    {
        //Get motor objects
        var LowerLeg_L_motor = LowerLeg_L.GetComponent<HingeJoint>().motor;
        var LowerLeg_R_motor = LowerLeg_R.GetComponent<HingeJoint>().motor;
        var UpperLeg_L_motor = UpperLeg_L.GetComponent<HingeJoint>().motor;
        var UpperLeg_R_motor = UpperLeg_R.GetComponent<HingeJoint>().motor;

        // Apply values for activations
        LowerLeg_L_motor.targetVelocity = activations[0];
        LowerLeg_R_motor.targetVelocity = activations[1];
        UpperLeg_L_motor.targetVelocity = activations[2];
        UpperLeg_R_motor.targetVelocity = activations[3];

        //Write back motor speed
        LowerLeg_L.GetComponent<HingeJoint>().motor = LowerLeg_L_motor;
        LowerLeg_R.GetComponent<HingeJoint>().motor = LowerLeg_R_motor;
        UpperLeg_L.GetComponent<HingeJoint>().motor = UpperLeg_L_motor;
        UpperLeg_R.GetComponent<HingeJoint>().motor = UpperLeg_R_motor;

        //Debug.Log(LowerLeg_L_motor.targetVelocity);
    }

    private float Update_Score()
    {
        float height = Torso.GetComponent<Transform>().position.y;

        if (!collided)
        {
            score += height * Time.fixedDeltaTime;
            TimeSinceBirth += Time.fixedDeltaTime;
        }
        else 
        {
            score -= penalty * Time.fixedDeltaTime;
        }

        if (TimeSinceBirth >= Duration)
        {
            Completed = true;
        }
        return score; //Apply score
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Incorporate height of head into the score calculation. (Along with time obviously)

        int current = 0; //current weight
        if (wait == false)
        {
            List<float> Inputs = Read_inputs();
            Weights_shape[0] = Inputs.Count;

            List<float> Layer_input = Inputs;
            List<float> Layer_output = new List<float>();

            //Propogate Inputs through layers
            for (int i = 1; i < Weights_shape.Length; i++) //Loops through all the layers (-1 since Weights_shape includes inputs)
            {
                Layer_output.Clear();

                for (int neuron = 0; neuron < Weights_shape[i]; neuron++) //iterate through each neuron
                {
                    float neuron_output = 0;

                    for (int ip = 0; ip < Layer_input.Count; ip++) //each input
                    {
                        neuron_output += Layer_input[ip] * Weights[current]; //adds weight component
                        current++;
                    }

                    neuron_output += Weights[current]; //adds bias component
                    current++;

                    Layer_output.Add(neuron_output);
                }

                Layer_input.Clear();
                Layer_input = Layer_output;

            }

            ApplyTorque(Layer_output);

        }


        score = Update_Score();


    }
}
