using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[System.Serializable]
public class NeuronBridge
{
	public bool condition;
	public bool sent;
	public Neuron target;

	public NeuronBridge(Neuron target, bool condition, bool sent)
	{
		this.target = target;
		this.condition = condition;
		this.sent = sent;
	}

	public bool activate(Brain brain, bool signal, int depth)
	{
		if (signal != condition) return true;
		return target.activate(brain,sent, depth + 1);

	}
}
