//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections.Generic;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GeneticAlgorithm : MonoBehaviour
{
	public List<DNA> Population { get; private set; }
	public int Generation { get; private set; }
	public float BestFitness { get; private set; }
	public float[] BestGenes { get; private set; }

	public int Elitism;
	public float MutationRate;

	private List<DNA> newPopulation;
	private System.Random random;
	private float fitnessSum;
	private int dnaSize;
	private Func<float> getRandomGene;

	//simulation related
	GameObject WalkerPrefab;
	Transform Position_reference;
	public double duration;
	float penalty;
	public string Save_destination;

	private void Awake()
    {
		//Debug.Log("Creating Save Destination (Created GA object)");
		//Save_destination = Application.persistentDataPath + "/save.dat";
		Save_destination = "save.dat";
	}


    public void Init(int populationSize, int dnaSize, System.Random random, Func<float> getRandomGene,
		 GameObject WalkerPrefab, Transform Position_reference, double duration, float penalty,
		int elitism, float mutationRate = 0.01f)
	{
		//Debug.Log("GA Initing");

		Generation = 1;
		Elitism = elitism;
		MutationRate = mutationRate;
		Population = new List<DNA>(populationSize);

		newPopulation = new List<DNA>(populationSize);

		this.random = random;
		this.dnaSize = dnaSize;
		this.getRandomGene = getRandomGene;

		this.WalkerPrefab = WalkerPrefab;
		this.Position_reference = Position_reference;
		this.duration = duration;
		this.penalty = penalty;

		BestGenes = new float[dnaSize];

		//Create DNA for first generation randomly
		for (int i = 0; i < populationSize; i++)
		{
			Population.Add(new DNA(dnaSize, random, getRandomGene, shouldInitGenes: true));
		}
	}

	public void NewGeneration()
	{
		//Debug.Log("Generating new generation");
		StartSimulation(); // Calculate BestGenes and BestFitness

	}

	public bool CheckIfCompleted(bool crossoverNewDNA = false)
	{
		bool result = PerformCheck();

		if (result)
		{
			Population.Sort(CompareDNA); //Sorts it based on comparision operator


			newPopulation.Clear();

			for (int i = 0; i < Population.Count; i++)
			{
				if (i < Elitism && i < Population.Count)
				{
					newPopulation.Add(Population[i]);
				}
				else if ((i < Population.Count) && crossoverNewDNA) //crossoverNewDNA decides if new non elite members are crosss over or fresh
				{
					DNA parent1 = ChooseParent();
					DNA parent2 = ChooseParent();

					DNA child = parent1.Crossover(parent2);

					child.Mutate(MutationRate);

					newPopulation.Add(child);
				}
				else
				{
					newPopulation.Add(new DNA(dnaSize, random, getRandomGene, shouldInitGenes: true));
				}
			}

			List<DNA> tmpList = Population;
			Population = newPopulation;
			newPopulation = tmpList;

			Generation++;
			Debug.Log("Generation: " + Generation.ToString());

		}

		return result;
	}

	//Compares two DNAs for fitness
	private int CompareDNA(DNA a, DNA b)
	{
		//Debug.Log("Comparing DNA");
		if (a.Fitness > b.Fitness)
		{
			return -1;
		}
		else if (a.Fitness < b.Fitness)
		{
			return 1;
		}
		else
		{
			return 0;
		}
	}

	private void StartSimulation()
	{
		//Debug.Log("Starting Simulation");
		GetComponentInParent<Run_Simulaton>().Simulate(Population, WalkerPrefab, Position_reference, duration, penalty);
	}

	private bool PerformCheck()
	{

		bool result = GetComponentInParent<Run_Simulaton>().Check();

		if (result)
		{
			//Debug.Log("Started fitness calc");
			fitnessSum = 0;
			DNA best = Population[0];

			//Calculating fitness
			List<float> Calculated_Fitness = GetComponentInParent<Run_Simulaton>().GetScore();

			BestFitness = 0f;
			for (int i = 0; i < Population.Count; i++)
			{
				Population[i].Fitness = Calculated_Fitness[i];
				fitnessSum += Population[i].Fitness; // Read fitness from DNA object

				if (Population[i].Fitness > best.Fitness)
				{
					best = Population[i];
				}
			}

			BestFitness = best.Fitness;
			Debug.Log(BestFitness);
			best.Genes.CopyTo(BestGenes, 0);
		}

		return result;

		
	}

	// Pick a random element
	private DNA ChooseParent()
	{

		/*
		//Debug.Log("Picking Parent");
		double randomNumber = random.NextDouble() * fitnessSum;

		for (int i = 0; i < Population.Count; i++)
		{
			if (randomNumber < Population[i].Fitness)
			{
				return Population[i];
			}

			randomNumber -= Population[i].Fitness;
		}
		return null;

		 */

		int i = random.Next(0, Elitism);
		return Population[i];
		
	}

	public void SaveToFile()
	{
		
		FileStream file;

		if (File.Exists(Save_destination)) file = File.OpenWrite(Save_destination);
		else file = File.Create(Save_destination);

		BinaryFormatter bf = new BinaryFormatter();
		bf.Serialize(file, BestGenes);
		file.Close();
	}

	public void LoadFile(int populationSize)
	{
		FileStream file;

		if (File.Exists(Save_destination)) file = File.OpenRead(Save_destination);
		else
		{
			Debug.LogError("File not found :" + Save_destination);
			return;
		}

		BinaryFormatter bf = new BinaryFormatter();
		BestGenes = (float[])bf.Deserialize(file);

		Population = new List<DNA>();
		for (int i = 0; i < populationSize; i++)
		{
			Population.Add(new DNA(dnaSize, random, getRandomGene, shouldInitGenes: false));
			Population[i].SetGene(BestGenes);
		}
		

		file.Close();
	}


}
