//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

using System;
using System.Collections.Generic;

public class GeneticAlgorithm<T>
{
	public List<DNA<T>> Population { get; private set; }
	public int Generation { get; private set; }
	public float BestFitness { get; private set; }
	public T[] BestGenes { get; private set; }

	public int Elitism;
	public float MutationRate;

	private List<DNA<T>> newPopulation;
	private Random random;
	private float fitnessSum;
	private int dnaSize;
	private Func<T> getRandomGene;
	private Func<int, float> fitnessFunction;

	//Constructor
	public GeneticAlgorithm(int populationSize, int dnaSize, Random random, Func<T> getRandomGene, Func<int, float> fitnessFunction,
		int elitism, float mutationRate = 0.01f)
	{
		Generation = 1;
		Elitism = elitism;
		MutationRate = mutationRate;
		Population = new List<DNA<T>>(populationSize);
		newPopulation = new List<DNA<T>>(populationSize);
		this.random = random;
		this.dnaSize = dnaSize;
		this.getRandomGene = getRandomGene;
		this.fitnessFunction = fitnessFunction;

		BestGenes = new T[dnaSize];

		//Create DNA for first generation randomly
		for (int i = 0; i < populationSize; i++)
		{
			Population.Add(new DNA<T>(dnaSize, random, getRandomGene, fitnessFunction, shouldInitGenes: true));
		}
	}

	public void NewGeneration(int numNewDNA = 0, bool crossoverNewDNA = false)
	{
		int finalCount = Population.Count + numNewDNA;

		if (finalCount <= 0)
		{
			return;
		}

		if (Population.Count > 0)
		{
			CalculateFitness(); // Calculate BestGenes and BestFitness
			Population.Sort(CompareDNA); //Sorts it based on comparision operator
		}
		newPopulation.Clear();

		for (int i = 0; i < Population.Count; i++)
		{
			if (i < Elitism && i < Population.Count)
			{
				newPopulation.Add(Population[i]);
			}
			else if (i < Population.Count || crossoverNewDNA) //crossoverNewDNA decides if new non elite members are crosss over or fresh
			{
				DNA<T> parent1 = ChooseParent();
				DNA<T> parent2 = ChooseParent();

				DNA<T> child = parent1.Crossover(parent2);

				child.Mutate(MutationRate);

				newPopulation.Add(child);
			}
			else
			{
				newPopulation.Add(new DNA<T>(dnaSize, random, getRandomGene, fitnessFunction, shouldInitGenes: true));
			}
		}

		List<DNA<T>> tmpList = Population;
		Population = newPopulation;
		newPopulation = tmpList;

		Generation++;
	}

	//Compares two DNAs for fitness
	private int CompareDNA(DNA<T> a, DNA<T> b)
	{
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

	private void CalculateFitness()
	{
		fitnessSum = 0;
		DNA<T> best = Population[0];

		//Calculating fitness
		Run_Simulaton<T> sim = new Run_Simulaton<T>(Population);
		float[] Calculated_Fitness = sim.Calculated_Fitness;

		for (int i = 0; i < Population.Count; i++)
		{
			Population[i].Fitness = Calculated_Fitness[i];
			fitnessSum += Population[i].Fitness; // CalculateFitness from DNA object

			if (Population[i].Fitness > best.Fitness)
			{
				best = Population[i];
			}
		}

		BestFitness = best.Fitness;
		best.Genes.CopyTo(BestGenes, 0);
	}

	// Pick a random element
	private DNA<T> ChooseParent()
	{
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
	}
}
