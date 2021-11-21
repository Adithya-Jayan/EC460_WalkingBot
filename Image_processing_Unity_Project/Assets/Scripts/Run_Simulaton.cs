using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;


public class Run_Simulaton : MonoBehaviour
{

    public List<float> Calculated_Fitness;
    private List<GameObject> ObjectList = new List<GameObject>();
    public float field_size = 10;

    public List<float> Simulate(List<DNA> Pop, GameObject WalkerPrefab, Transform Position_reference, double duration, float penalty)
    {
        Vector3 current_pos;
        //Debug.Log("Instantiating objects");
        //Instantiate all the walker objects with their DNA
        for (int i = 0; i < Pop.Count; i++)
        {

            //Debug.Log(i.ToString());


            current_pos = new Vector3(0.0f, 0.0f, field_size * i);
            Position_reference.position = current_pos;

            //Debug.Break();
            ObjectList.Add(Instantiate(WalkerPrefab, Position_reference));

            //Debug.Break();
            ObjectList[i].GetComponent<NN_Brain>().Init(Pop[i], duration, penalty); //<-- something wrong here

            //Debug.Break();

        }

        //ObjectList[-1].GetComponent<NN_Brain>().Init(Pop[-1], duration, penalty); // << === Error on purpose


        //Obtain score from brain
        //

        float sum = 0f;
        for (int i = 0; i < Pop.Count; i++)
        {
            /*
            
            */
            Thread.Sleep(10000);

            Debug.Log("Reading fitness while completed :" + ObjectList[i].GetComponent<NN_Brain>().Completed.ToString());
            Calculated_Fitness.Add(ObjectList[i].GetComponent<NN_Brain>().score);
            Debug.Log(Calculated_Fitness[i].ToString());
            sum += Calculated_Fitness[i];
        }
        //Incorporate normalization

        for (int i = 0; i < Calculated_Fitness.Count; i++)
        {
            Calculated_Fitness[i] = Calculated_Fitness[i] / sum;
        }

        //Delete all the objects
        for (int i = 0; i < ObjectList.Count; i++)
        {
            Destroy(ObjectList[i]);
            Debug.Log("Destroyed an object " + i.ToString());
        }
        ObjectList.Clear();

        return Calculated_Fitness;
    }

    /*
    
    */
}
