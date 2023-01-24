using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;




[Serializable]
public class Brain
{

	public static System.Random rand = new System.Random();

	List<Neuron> inputNeurons = new List<Neuron>();
	List<Neuron> allNeurons = new List<Neuron>();



	List<Neuron> outputNeurons = new List<Neuron>();


	List<Neuron> modifiableNeurons = new List<Neuron>();



	bool isAlive = true;

	public int thoughtCount = 0;

	public List<int> probability = new List<int>();
	
	
	public bool IsAlive { get { return this.isAlive; } }
	public Brain()
	{
		mutateAddNewNeuron();
	}
	public void Pzzz()
	{
		thoughtCount++;
	}

	public void StartThinking()
	{
		thoughtCount = 0;
		for(int i = 0; i < probability.Count; i++)
		{
			probability[i] = 0;
		}

	}
	public bool ActivateInputNode(int n)
	{
		return this.inputNeurons[n].activate(this,true);
	}


	public Neuron AddInput()
	{
		var inputNeuron = new Neuron();

		inputNeurons.Add(inputNeuron);
		allNeurons.Add(inputNeuron);
		return inputNeuron;
	}
	[System.Serializable]
	public class HelperFuncClass {
		List<int> list;
		int n;
		public HelperFuncClass(List<int> list, int n)
		{
			this.list = list;
			this.n = n;

		}
		public void feedbackHdr(bool feedback)
		{
			//Console.WriteLine("Decision called ");
			if (feedback)
			{
				list[n]++;

			}
			else
			{
				list[n]--;

			}
		}
	}

	public void AddDecision()
	{
		probability.Add(0);
		 int currentDecisionIndex = probability.Count-1;
		var outputNeuron = new NeuronOutput(new HelperFuncClass(probability, currentDecisionIndex).feedbackHdr);
		
		this.outputNeurons.Add(outputNeuron);
		allNeurons.Add(outputNeuron);
	}

	public int Decide()
	{
		int selectedDecision = 0;
		int mostProb = 0;
		for(int k = 0; k < probability.Count; k++)
		{
			var prob = probability[k];
			if (prob > mostProb)
			{
				selectedDecision = k;
				mostProb = prob;
			}
		}
		return selectedDecision;
	}
	public void Print()
	{
		int selectedDecision = 0;
		int mostProb = 0;
		for (int k = 0; k < probability.Count; k++)
		{
			var prob = probability[k];
			Console.WriteLine("" + k + " : " +prob);
			if (prob > mostProb)
			{
				selectedDecision = k;
				mostProb = prob;
			}
		}
		Console.WriteLine("Chosen" + selectedDecision + " : " + mostProb);
	}
	public void Kill()
	{
		this.isAlive = false;
	}

	public void mutateAddNewNeuron()
	{
		Neuron n = new Neuron();
		allNeurons.Add(n);
		modifiableNeurons.Add(n);
	}


	public void mutateRemoveANeuron()
	{
		var n = modifiableNeurons[rand.Next(0, modifiableNeurons.Count)];
		allNeurons.Remove(n);
		modifiableNeurons.Add(n);

		for (int i = 0; i < allNeurons.Count; i++)
		{
			var neuron = allNeurons[i];
			for (int j = neuron.brdiges.Count - 1; j >= 0; j--)
			{
				if (neuron.brdiges[j].target == n)
				{
					//Bridge's target is removed
					neuron.brdiges.RemoveAt(j);
				}
			}
		}


	}

	public void mutateAddNewConnection()
	{
		var aIndex = rand.Next(0, allNeurons.Count);
		var bIndex = rand.Next(0, allNeurons.Count);
		var a = allNeurons[aIndex];
		var b = allNeurons[bIndex];
		bool condition = rand.Next(0, 2) == 0;
		bool sent = rand.Next(0, 2) == 0;
		//Console.WriteLine("mutate new line connection " + aIndex + " ->" + bIndex + " " + condition + " " + sent);
		a.brdiges.Add(new NeuronBridge(b, condition, sent));
	}


}
