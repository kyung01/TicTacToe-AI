using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[System.Serializable]
public class Neuron
{
	public List<NeuronBridge> brdiges = new List<NeuronBridge>();

	public virtual bool activate(Brain brain, bool signal, int depth=0)
	{
		//Console.WriteLine("Neuron activaetd ");
		brain.Pzzz();
		if (depth > 100)
		{
			Console.WriteLine("NeuronFried 100+");
			brain.Kill();
			return false;
		}
		foreach (var bridge in brdiges)
		{
			if (!bridge.activate(brain,signal, depth))
			{
				return false;
			}
		}
		return true;

	}
}
