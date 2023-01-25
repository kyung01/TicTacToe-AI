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

	public List<int> decisionProb = new List<int>();
	
	
	public bool IsAlive { get { return this.isAlive; } }
	
	public Brain()
	{
		mutateAddNewNeuron();
	}

	public Brain Clone()
	{
		var newBrain = new Brain();

		newBrain.inputNeurons = new List<Neuron>(inputNeurons.Count);
		
		newBrain.outputNeurons = new List<Neuron>(outputNeurons.Count);
		newBrain.modifiableNeurons = new List<Neuron>(modifiableNeurons.Count);
		newBrain.allNeurons = new List<Neuron>(allNeurons.Count);
		//
		//newBrain.decisionProb = new List<int>(decisionProb.Count);
		int count = 0;
		int outputNeuronIndex = 0;
		int modifyIndex = 0;
		for(int i = 0; i < allNeurons.Count; i++)
		{
			if(count-- < 0)
			{
				//Console.WriteLine((i+1) + " / " + allNeurons.Count);
				count = 104122;

			}
			var neuron = allNeurons[i];
			var neuronClone = neuron.Clone();
			newBrain.allNeurons.Add(neuronClone);
			switch (neuronClone.Type)
			{
				case NueronType.INPUT:
					newBrain.inputNeurons.Add(neuronClone);
					break;
				case NueronType.OUTPUT:
					newBrain.outputNeurons.Add(neuronClone);
					break;
				case NueronType.HIDDEN:
					newBrain.modifiableNeurons.Add(neuronClone);
					break;
				default:
					throw new Exception();
			}
		}

		foreach (var decision in decisionProb)
		{
			newBrain.decisionProb.Add(0);
		}
		return newBrain;
	}

	public void Pzzz()
	{
		thoughtCount++;
	}

	public void StartThinkingNew()
	{
		thoughtCount = 0;
		for(int i = 0; i < decisionProb.Count; i++)
		{
			decisionProb[i] = 0;
		}

	}
	public bool ActivateInputNode(int n, bool signal = true, int depth = 0)
	{
		return this.inputNeurons[n].activate(this, true, depth);
	}

	public bool ActivateNeuronAt(int n, bool signal = true, int depth = 0)
	{
		return this.allNeurons[n].activate(this, true, depth);
	}


	public Neuron AddInput()
	{
		var inputNeuron = new Neuron( NueronType.INPUT);

		inputNeurons.Add(inputNeuron);
		allNeurons.Add(inputNeuron);
		return inputNeuron;
	}
	public void RecordDecision(bool feedback, int index)
	{
		//Console.WriteLine("Decision called ");
		if (feedback)
		{
			decisionProb[index]++;

		}
		else
		{
			decisionProb[index]--;

		}
	}
	public void AddDecision()
	{
		decisionProb.Add(0);
		 int currentDecisionIndex = decisionProb.Count-1;
		var outputNeuron = new NeuronOutput(currentDecisionIndex);
		
		this.outputNeurons.Add(outputNeuron);
		allNeurons.Add(outputNeuron);
	}

	public int Decide()
	{
		int selectedDecision = -1;
		int mostProb = 0;
		for(int k = 0; k < decisionProb.Count; k++)
		{
			var prob = decisionProb[k];
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
		for (int k = 0; k < decisionProb.Count; k++)
		{
			var prob = decisionProb[k];
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
		Neuron n = new Neuron(NueronType.HIDDEN);
		allNeurons.Add(n);
		modifiableNeurons.Add(n);
	}


	public void mutateRemoveNeuron()
	{
		if (modifiableNeurons.Count == 0) return;
		var indexModifable = rand.Next(0, modifiableNeurons.Count);
		var neuronToRemove = modifiableNeurons[indexModifable];
		//Console.WriteLine("Find index...");
		var index = allNeurons.FindIndex(a=>a == neuronToRemove);
		//Console.WriteLine("done...");

		allNeurons.Remove(neuronToRemove);
		modifiableNeurons.Remove(neuronToRemove);

		//Console.WriteLine("Removing all the bridges...");
		for (int i = 0; i < allNeurons.Count; i++)
		{
			var neuron = allNeurons[i];
			for (int j = neuron.bridges.Count - 1; j >= 0; j--)
			{
				if (neuron.bridges[j].targetIndex == index)
				{
					//Bridge's target is removed
					neuron.bridges.RemoveAt(j);
				}
			}

		}
		//Console.WriteLine("done...");
		//Console.WriteLine("renumbering bridge indexs");

		for (int i = 0; i < allNeurons.Count; i++)
		{

			var neuron = allNeurons[i];
			for(int j = 0; j < neuron.bridges.Count; j++)
			{
				var bridge = neuron.bridges[j];
				if (bridge.targetIndex > index)
				{
					if (bridge.targetIndex - 1 < 0)
					{
						Console.WriteLine("WTF");
					}
					neuron.bridges[j] = new NeuronBridge(bridge.targetIndex - 1, bridge.condition, bridge.sent);
				}
					
			}
		}
		//Console.WriteLine("done...");


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
		a.bridges.Add(new NeuronBridge(bIndex, condition, sent));
	}


}
