using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run_Simulaton : MonoBehaviour
{
    
    public float[] Calculated_Fitness;
    private List<GameObject> ObjectList = new List<GameObject>();
    public float field_size = 10f;

    [SerializeField] GameObject WalkerPrefab;
    [SerializeField] Transform Position_reference;
    [SerializeField] double duration;

    public Run_Simulaton(List<DNA> Pop)
    {
        Vector3 current_pos;

        //Instantiate all the walker objects with their DNA
        for (int i = 0; i < Pop.Count; i++)
        {
            current_pos = new Vector3(0.0f, 0.0f, field_size * i);
            Position_reference.position = current_pos;
            ObjectList.Add(Instantiate(WalkerPrefab, Position_reference));
            ObjectList[-1].GetComponent<NN_Brain>().Init(Pop[i], duration);
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
        }
        ObjectList.Clear();

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
