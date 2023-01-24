
using System;
using System.Collections.Generic;


[System.Serializable]
public delegate void DEL_ACTIVATED(bool signal);


[System.Serializable]
public class NeuronOutput : Neuron
{
	DEL_ACTIVATED hdrBool;
	public NeuronOutput(DEL_ACTIVATED hdrBool)
	{
		this.hdrBool = hdrBool;
	}

	public override bool activate(Brain brain, bool signal,int depth)
	{
		hdrBool(signal);
		return true;
	}

}


