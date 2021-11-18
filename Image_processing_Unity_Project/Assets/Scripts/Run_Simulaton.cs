using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run_Simulaton : MonoBehaviour
{
    
    public float[] Calculated_Fitness;
    private List<GameObject> ObjectList = new List<GameObject>();
    public float field_size = 10;
    
    public float[] Simulate(List<DNA> Pop, GameObject WalkerPrefab, Transform Position_reference, double duration, float penalty)
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
            ObjectList[i].GetComponent<NN_Brain>().Init(Pop[i], duration , penalty); //<-- something wrong here

            //Debug.Break();
            
        }

        float sum = 0f;
        //Obtain score from brain
        //Debug.Log("Is simulation complete?:");
        //Debug.Log(ObjectList[0].GetComponent<NN_Brain>().Completed.ToString());
        //Debug.Log("Answer is above");

        ObjectList[-1].GetComponent<NN_Brain>().Init(Pop[-1], duration, penalty); << === Error on purpose

        for (int i = 0; i < Pop.Count; i++)
        {
            while (ObjectList[i].GetComponent<NN_Brain>().Completed == false)
            {
                Debug.Log("Waiting for simulation to complete!!!!");
            } //Wait till calculation is complete
            
            Calculated_Fitness[i] = ObjectList[i].GetComponent<NN_Brain>().score; <<-- We havent defined a size for this
            sum += Calculated_Fitness[i];
        }
        //Incorporate normalization

        for (int i = 0; i < Calculated_Fitness.Length; i++)
        {
            Calculated_Fitness[i] = Calculated_Fitness[i]/ sum;
        }

        //Delete all the objects
        for (int i = 0; i < ObjectList.Count; i++)
        {
            Destroy(ObjectList[i]);
            Debug.Log("Destroyed an object "+ i.ToString() );
        }
        ObjectList.Clear();

        return Calculated_Fitness;
    }

}
