using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum NueronType
{
	INPUT,OUTPUT,HIDDEN

}
[System.Serializable]
public class Neuron
{
	NueronType type;
	public List<NeuronBridge> bridges = new List<NeuronBridge>();

	public NueronType Type { get{return this.type;}}

	public Neuron(NueronType type)
	{
		this.type = type;

	}
	public virtual bool activate(Brain brain, bool signal, int depth=0)
	{
		//Console.WriteLine("Neuron activaetd ");
		brain.Pzzz();
		if (depth > 100)
		{
			//Console.WriteLine("NeuronFried 100+");
			brain.Kill();
			return false;
		}
		foreach (var bridge in bridges)
		{
			if (!bridge.activate(brain,signal, depth))
			{
				return false;
			}
		}
		return true;

	}

	public virtual Neuron Clone()
	{
		Neuron clone = new Neuron(this.type);
		clone.bridges = new List<NeuronBridge>(this.bridges);
		return clone;
	}
}
