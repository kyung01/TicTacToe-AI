using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[System.Serializable]
public delegate bool DEL_AGENT_TRAINING_COURSE(Agent agent);
public class AgentTrainingField
{

	static System.Random rand = new System.Random();
	public static bool Isfinished = false;



	List<Agent> agents = new List<Agent>();

	public static void Train(List<Agent> agents, DEL_AGENT_TRAINING_COURSE course)
	{


		DebugText.cont = "";

		for (int i = 0; i < agents.Count; i++)
		{
			var agent = agents[i];
			agent.Score = 0;
			agent.GetReady();
			course(agent);
			if (!agent.IsAlive)
			{
				//Agent prob failed the course and died
				Console.WriteLine("Removing at " + i);
				agents.RemoveAt(i);
			}
		}
		agents.Sort((emp1, emp2) => emp2.Score.CompareTo(emp1.Score));
		Console.WriteLine("Best score was " + agents[0].Score);
		if (agents[0].Score == 3)
		{

			Console.WriteLine("Best agent created");
			Isfinished = true;
			return;
		}

		if (agents.Count > 3)
		{
			agents.RemoveRange(3, agents.Count - 3);

		}
		for (int i = 0; i < 7; i++)
		{
			var newAgent = (Agent)agents[rand.Next(0, agents.Count)].Clone();
			agents.Add(newAgent);

			int modify = rand.Next(0, 3);
			Console.WriteLine("Cloning at " + i + " modifiying " + modify);
			switch (modify)
			{
				case 0:
					newAgent.brain.mutateAddNewConnection();
					newAgent.brain.mutateAddNewConnection();
					newAgent.brain.mutateAddNewConnection();
					newAgent.brain.mutateAddNewConnection();
					newAgent.brain.mutateAddNewConnection();
					newAgent.brain.mutateAddNewConnection();
					newAgent.brain.mutateAddNewConnection();
					newAgent.brain.mutateAddNewConnection();
					break;
				case 1:
					newAgent.brain.mutateAddNewNeuron();
					break;
				case 2:
					newAgent.brain.mutateRemoveANeuron();
					break;
			}
		}
		//Debug.Log("Ending a thread");
	}


}
