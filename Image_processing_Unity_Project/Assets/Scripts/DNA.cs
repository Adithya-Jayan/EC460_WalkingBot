//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

using System;

public class DNA<T> //<T> defines a generic datatype as input
{
	public T[] Genes { get; private set; } 
	//Variable Genes of type T, get => can be read from anywhere, private set => Can only be set locally
	public float Fitness { get; set; }

	//Defining functions
	private Random random;
	private Func<T> getRandomGene;
	private Func<int, float> fitnessFunction;

	//Constructor
	public DNA(int size, Random random, Func<T> getRandomGene, Func<int, float> fitnessFunction, bool shouldInitGenes = true)
	{
		Genes = new T[size]; 

		//Create local copy of variables
		this.random = random;
		this.getRandomGene = getRandomGene;
		this.fitnessFunction = fitnessFunction;

		//Initialise genes
		if (shouldInitGenes)
		{
			for (int i = 0; i < Genes.Length; i++)
			{
				Genes[i] = getRandomGene();
			}
		}
	}

	//Mix two parents
	public DNA<T> Crossover(DNA<T> otherParent)
	{
		DNA<T> child = new DNA<T>(Genes.Length, random, getRandomGene, fitnessFunction, shouldInitGenes: false);

		for (int i = 0; i < Genes.Length; i++)
		{
			child.Genes[i] = random.NextDouble() < 0.5 ? Genes[i] : otherParent.Genes[i];
		}

		return child;
	}

	public void Mutate(float mutationRate)
	{
		for (int i = 0; i < Genes.Length; i++)
		{
			if (random.NextDouble() < mutationRate)
			{
				Genes[i] = getRandomGene();
			}
		}
	}
}
