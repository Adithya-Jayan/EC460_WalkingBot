//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

using System;

public class DNA //<T> defines a generic datatype as input
{
	public float[] Genes { get; private set; } 
	//Variable Genes of type T, get => can be read from anywhere, private set => Can only be set locally
	public float Fitness { get; set; }

	//Defining functions
	private Random random;
	private Func<float> getRandomGene;

	//Constructor
	public DNA(int size, Random random, Func<float> getRandomGene,  bool shouldInitGenes = true)
	{
		Genes = new float[size]; 

		//Create local copy of variables
		this.random = random;
		this.getRandomGene = getRandomGene;

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
	public DNA Crossover(DNA otherParent)
	{
		DNA child = new DNA(Genes.Length, random, getRandomGene, shouldInitGenes: false);

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
