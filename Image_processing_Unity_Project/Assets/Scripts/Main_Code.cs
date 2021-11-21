//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using System.Threading.Tasks;

public class Main_Code : MonoBehaviour
{
	[Header("Genetic Algorithm")]
	[SerializeField] int populationSize = 200;
	[SerializeField] int dna_size = 1803;
	[SerializeField] float mutationRate = 0.01f;
	[SerializeField] int elitism = 5; //Number of best people to carry over without changing

	[Header("Simulation Parameters")]
	[SerializeField] GameObject WalkerPrefab;
	[SerializeField] Transform Position_reference;
	[SerializeField] double duration;
	[SerializeField] float penalty = 10;

	Func<float[]> Simulate;


	private GeneticAlgorithm ga; // T is char here (We'll need to change it)
	private System.Random random;

	void Start()
	{
		Debug.Log("Started main_script");
		random = new System.Random();

		ga = GetComponentInParent<GeneticAlgorithm>();
		ga.Init(populationSize, dna_size, random, GetRandomWeight,
			WalkerPrefab, Position_reference, duration, penalty,
			elitism, mutationRate);
		Debug.Log("ga inited");
		Debug.Break();
		// Change parameters above!!!

		var tcs = new TaskCompletionSource<Vector3>();
		Task.Run(async () =>
		{
			Debug.Log("Task started");
			tcs.SetResult(await LongRunningTask());
			Debug.Log("Task stopped");
		});

	}

	void Next_gen()
	{
		Debug.Log("Started main_script update , generating new population");

		ga.NewGeneration();

		// UpdateText(ga.BestGenes, ga.BestFitness, ga.Generation, ga.Population.Count, (j) => ga.Population[j].Genes);

		if (ga.BestFitness == 1)
		{
			this.enabled = false; //Shuts down simulation. Remember to save before this
		}
		Debug.Log("Completed pop_gen");
	}

	private float GetRandomWeight() //Getrandom gene equivalent
	{
		float i = (float)random.NextDouble();
		//int i = random.Next(validCharacters.Length);
		return i;
	}

	async Task<Vector3> LongRunningTask()
	{
		var v = Vector3.zero;

		while (true)
			Next_gen();	//Wait till calculation is complete

		return v;
	}

}