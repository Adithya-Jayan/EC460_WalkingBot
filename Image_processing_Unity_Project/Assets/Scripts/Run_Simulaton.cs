using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;


public class Run_Simulaton : MonoBehaviour
{

    public List<float> Calculated_Fitness;
    private List<GameObject> ObjectList = new List<GameObject>();
    public float field_size = 10;
    List<DNA> Pop;

    public void Simulate(List<DNA> Pop, GameObject WalkerPrefab, Transform Position_reference, double duration, float penalty)
    {
        Vector3 current_pos;
        this.Pop = Pop;

        //Instantiate all the walker objects with their DNA
        for (int i = 0; i < Pop.Count; i++)
        {
            int x_pos = (i / 10);
            int z_pos = (i % 10);
            current_pos = new Vector3(field_size * x_pos , 0.0f , field_size * z_pos);
            Position_reference.position = current_pos;

            //Debug.Log("Instantiated object" + (i + 1).ToString() + " at " + current_pos.ToString());

            ObjectList.Add(Instantiate(WalkerPrefab, Position_reference,true));

            ObjectList[i].GetComponent<NN_Brain>().Init(Pop[i], duration, penalty);
        }
        //Debug.Log("Instantiated all objects");
    }

    public List<float> GetScore()
    {
        //Obtain score from brain
        //
        float sum = 0f;
        for (int i = 0; i < Pop.Count; i++)
        {
            Calculated_Fitness.Add(ObjectList[i].GetComponent<NN_Brain>().score);
            sum += Calculated_Fitness[i];
        }
        //Incorporate normalization
        //Debug.Log("Normalizing score");
        for (int i = 0; i < Calculated_Fitness.Count; i++)
        {
            Calculated_Fitness[i] = Calculated_Fitness[i] / sum;
        }

        //Delete all the objects
        for (int i = 0; i < ObjectList.Count; i++)
        {
            Destroy(ObjectList[i]);
            //Debug.Log("Destroyed an object " + i.ToString());
        }
        ObjectList.Clear();

        return Calculated_Fitness;
    }

    public bool Check()
    {
        bool result = true;

        for (int i = 0; i < ObjectList.Count; i++)
        {
            result = result && ObjectList[i].GetComponent<NN_Brain>().Completed;
        }


        return result;
    }
}
