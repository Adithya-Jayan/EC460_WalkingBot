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
	// [SerializeField] string targetString = "To be, or not to be, that is the question.";
	// [SerializeField] string validCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ,.|!#$%&/()=? ";
	[SerializeField] int populationSize = 200;
	[SerializeField] float mutationRate = 0.01f;
	[SerializeField] int elitism = 5; //Number of best people to carry over without changing

	[Header("Other")]
	//[SerializeField] int numCharsPerText = 15000;
	//[SerializeField] Text targetText;
	//[SerializeField] Text bestText;
	//[SerializeField] Text bestFitnessText;
	//[SerializeField] Text numGenerationsText;
	//[SerializeField] Transform populationTextParent;
	//[SerializeField] Text textPrefab;

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
		ga = new GeneticAlgorithm(populationSize, targetString.Length, random, GetRandomWeight, elitism, mutationRate);
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












	private int numCharsPerTextObj;
	private List<Text> textList = new List<Text>();

	void Awake()
	{
		numCharsPerTextObj = numCharsPerText / validCharacters.Length;
		if (numCharsPerTextObj > populationSize) numCharsPerTextObj = populationSize;

		int numTextObjects = Mathf.CeilToInt((float)populationSize / numCharsPerTextObj);

		for (int i = 0; i < numTextObjects; i++)
		{
			textList.Add(Instantiate(textPrefab, populationTextParent));
		}
	}

	private void UpdateText(char[] bestGenes, float bestFitness, int generation, int populationSize, Func<int, char[]> getGenes)
	{
		bestText.text = CharArrayToString(bestGenes);
		bestFitnessText.text = bestFitness.ToString();

		numGenerationsText.text = generation.ToString();

		for (int i = 0; i < textList.Count; i++)
		{
			var sb = new StringBuilder();
			int endIndex = i == textList.Count - 1 ? populationSize : (i + 1) * numCharsPerTextObj;
			for (int j = i * numCharsPerTextObj; j < endIndex; j++)
			{
				foreach (var c in getGenes(j))
				{
					sb.Append(c);
				}
				if (j < endIndex - 1) sb.AppendLine();
			}

			textList[i].text = sb.ToString();
		}
	}

	private string CharArrayToString(char[] charArray)
	{
		var sb = new StringBuilder();
		foreach (var c in charArray)
		{
			sb.Append(c);
		}

		return sb.ToString();
	}
}
