using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[System.Serializable]
public struct NeuronBridge
{
	public bool condition;
	public bool sent;
	public int targetIndex;

	public NeuronBridge(int target, bool condition, bool sent)
	{
		this.targetIndex = target;
		this.condition = condition;
		this.sent = sent;
	}

	public bool activate(Brain brain, bool signal, int depth)
	{
		if (signal != condition) return true;
		return brain.ActivateNeuronAt(targetIndex, sent, depth + 1);
		//return target.activate(brain,sent, depth + 1);

	}
}
