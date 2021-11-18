//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

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


	private GeneticAlgorithm ga; // T is char here (We'll need to change it)
	private System.Random random;

	void Start()
	{
		/* targetText.text = targetString;
		
		/ if (string.IsNullOrEmpty(targetString))
		{
			Debug.LogError("Target string is null or empty");
			this.enabled = false;
		}
		*/

		random = new System.Random();
		ga = new GeneticAlgorithm(populationSize, dna_size, random, GetRandomWeight,
			WalkerPrefab, Position_reference, duration, penalty, 
			elitism, mutationRate);
		// Change parameters above!!!
	}

	void Update()
	{
		ga.NewGeneration();

		// UpdateText(ga.BestGenes, ga.BestFitness, ga.Generation, ga.Population.Count, (j) => ga.Population[j].Genes);

		if (ga.BestFitness == 1)
		{
			this.enabled = false; //Shuts down simulation. Remember to save before this
		}
	}

	private float GetRandomWeight() //Getrandom gene equivalent
	{
		float i = (float)random.NextDouble();
		//int i = random.Next(validCharacters.Length);
		return i;
	}

}