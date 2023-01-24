using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;


public class DebugText
{
	public static string cont = "";
}


[System.Serializable]
public enum RSP { ROCK,SCISSOR,PAPER}

[Serializable]
public class Agent
{
	public Object Clone()
	{
		using (MemoryStream stream = new MemoryStream())
		{
			if (this.GetType().IsSerializable)
			{
				BinaryFormatter fmt = new BinaryFormatter();
				fmt.Serialize(stream, this);
				stream.Position = 0;
				return fmt.Deserialize(stream);
			}
			return null;
		}
	}
	public Brain brain = new Brain();

	public float Score = 0;
	public bool IsAlive { get { return this.brain.IsAlive; } }

	public void GetReady()
	{
		brain.StartThinking();
	}

	
}


