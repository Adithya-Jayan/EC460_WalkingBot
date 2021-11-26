//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

//using System.Threading.Tasks;

public class Main_Code : MonoBehaviour
{
	[Header("Genetic Algorithm")]
	[SerializeField] int populationSize = 200;
	[SerializeField] int dna_size = 1803;
	[SerializeField] float mutationRate = 0.01f;
	[SerializeField] int elitism = 5; //Number of best people to carry over without changing
	[SerializeField] bool crossover = false;

	[Header("Simulation Parameters")]
	[SerializeField] GameObject WalkerPrefab;
	[SerializeField] Transform Position_reference;
	[SerializeField] double duration;
	[SerializeField] float penalty = 10;

	[Header("Text UI")]
	[SerializeField] Text BestScore_disp;
	[SerializeField] Text Generation_disp;
	[SerializeField] bool LoadFromFile;	

	Func<float[]> Simulate;
	bool running = false;

	private GeneticAlgorithm ga; // T is char here (We'll need to change it)
	private System.Random random;

	void Start()
	{
		//Debug.Log("Started main_script");
		random = new System.Random();

		ga = GetComponentInParent<GeneticAlgorithm>();
		ga.Init(populationSize, dna_size, random, GetRandomWeight,
			WalkerPrefab, Position_reference, duration, penalty,
			elitism, mutationRate);

		if (LoadFromFile)
			ga.LoadFile(populationSize);
		//Debug.Log("ga inited");

		//Debug.Log("Reading from savefile");
		//ga.SaveToFile();

		running = false;
	}

    private void FixedUpdate()
    {
		running = Next_gen(running);
	}


    bool Next_gen(bool running)
	{
		if (!running)
		{
			//Debug.Log("Creating new generation");
			ga.NewGeneration();
			running = true;
			//Debug.Log("Started running");
		}

		else
		{
			if (ga.CheckIfCompleted(crossover))
			{
				running = false;
				//Debug.Log("Saving to file");
				ga.SaveToFile();

				BestScore_disp.text = "Best Score: "+ ga.BestFitness.ToString();
				Generation_disp.text = "Generation: " + ga.Generation.ToString();

				/*if (ga.BestFitness == 1)
				{
					this.enabled = false; //Shuts down simulation. Remember to save before this
				}*/

				//Debug.Log("Finished running");
			}
			
		}

		return running;
	}

	private float GetRandomWeight() //Getrandom gene equivalent
	{
		float i = (float)random.NextDouble();
		return (2*i - 1);
	}

	/*async Task<Vector3> LongRunningTask()
	{
		var v = Vector3.zero;

		while (true)
			Next_gen();	//Wait till calculation is complete

		return v;
	}*/

}