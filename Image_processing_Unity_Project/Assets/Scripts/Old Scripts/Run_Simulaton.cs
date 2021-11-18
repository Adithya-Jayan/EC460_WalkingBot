using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run_Simulaton : MonoBehaviour
{
    
    public float[] Calculated_Fitness;
    private List<GameObject> ObjectList = new List<GameObject>();
    public float field_size = 10f;

    public Run_Simulaton(List<DNA> Pop, GameObject WalkerPrefab, Transform Position_reference, double duration, float penalty)
    {
        Vector3 current_pos;

        //Instantiate all the walker objects with their DNA
        for (int i = 0; i < Pop.Count; i++)
        {
            current_pos = new Vector3(0.0f, 0.0f, field_size * i);
            Position_reference.position = current_pos;
            ObjectList.Add(Instantiate(WalkerPrefab, Position_reference));
            ObjectList[i].GetComponent<NN_Brain>().Init(Pop[i], duration , penalty);
        }

        float sum = 0f;
        //Obtain score from brain

        for (int i = 0; i < Pop.Count; i++)
        {
            while (!ObjectList[i].GetComponent<NN_Brain>().Completed) ; //Wait till calculation is complete

            Calculated_Fitness[i] = ObjectList[i].GetComponent<NN_Brain>().score;
            sum += Calculated_Fitness[i];
        }
        //Incorporate normalization
        for (int i = 0; i < Pop.Count; i++)
        {
            Calculated_Fitness[i] = Calculated_Fitness[i]/ sum;
        }

        //Delete all the objects
        for (int i = 0; i < Pop.Count; i++)
        {
            Destroy(ObjectList[i]);
            Debug.Log("Destroyed an object "+ i.ToString() );
        }
        ObjectList.Clear();

    }

}
