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


		Console.WriteLine("Train " + agents.Count);
		DebugText.cont = "";

		for (int i = agents.Count-1; i >=0; i--)
		{
			//Console.WriteLine("Testing agent " + i);
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
		if (agents.Count < 3)
		{
			for(int i = agents.Count; i < 3; i++)
			{

				agents.Add(MainProgram.GetAgent());
			}
		}

		agents.Sort((emp1, emp2) => emp2.Score.CompareTo(emp1.Score));
		Console.WriteLine("Best score was " + agents[0].Score);
		if (agents[0].Score == 10)
		{

			Console.WriteLine("Best agent created");
			//Isfinished = true;
			//return;
		}


		if (agents.Count > 3)
		{
			agents.RemoveRange(3, agents.Count - 3);

		}
		//Console.WriteLine("Adding mutated agents from the best set of candidates");
		for (int i = agents.Count; i < 30; i++)
		{
			//Console.WriteLine((1+i) + "/10");
			var newAgent = (Agent)agents[rand.Next(0, agents.Count)].Clone();
			agents.Add(newAgent);

			int modify = rand.Next(0, 6);
			//Console.WriteLine("Modifying with option { " + modify + "}  " );
			switch (modify)
			{
				case 0:
					newAgent.brain.mutateAddNewConnection();
					break;
				case 1:
					newAgent.brain.mutateAddNewNeuron();
					break;
				case 2:
					newAgent.brain.mutateRemoveNeuron();
					break;
				case 4:
					newAgent.brain.mutateRemoveNeuron();
					newAgent.brain.mutateAddNewNeuron();
					newAgent.brain.mutateAddNewConnection();
					break;
				case 5:
					newAgent.brain.mutateAddNewNeuron();
					newAgent.brain.mutateAddNewConnection();
					break;
				case 3:
					newAgent.brain.mutateAddNewConnection();
					newAgent.brain.mutateAddNewConnection();
					newAgent.brain.mutateAddNewConnection();
					newAgent.brain.mutateAddNewConnection();
					newAgent.brain.mutateAddNewConnection();
					newAgent.brain.mutateAddNewConnection();
					newAgent.brain.mutateAddNewConnection();
					newAgent.brain.mutateAddNewConnection();
					newAgent.brain.mutateAddNewConnection();
					newAgent.brain.mutateAddNewConnection();
					break;
			}
		}
		Console.WriteLine("Train Reached end");
		//Debug.Log("Ending a thread");
	}


}
