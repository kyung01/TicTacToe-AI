
using System;
using System.Collections.Generic;


[System.Serializable]
public delegate void DEL_ACTIVATED(bool signal);


[System.Serializable]
public class NeuronOutput : Neuron
{
	//DEL_ACTIVATED hdrBool;
	int index = 0;

	public NeuronOutput(int decision):base(NueronType.OUTPUT)
	{
		this.index = decision;
	}

	public override bool activate(Brain brain, bool signal,int depth)
	{
		brain.RecordDecision(signal, index);
		return true;
	}
	public override Neuron Clone()
	{
		NeuronOutput clone = new NeuronOutput(index);
		clone.bridges = new List<NeuronBridge>(this.bridges);
		return clone;
	}

}


