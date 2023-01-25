using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class MainProgram
{
	static System.Random rand = new Random();

	static List<Agent> agents = new List<Agent>();
	static Thread thread;

	public static void ThreadStart()
	{
		AgentTrainingField.Train(agents, agentCourse);
	}
	public static void startThread(List<Agent> agents)
	{

		thread = new Thread(new ThreadStart(ThreadStart));
		thread.Start();
	}
	static void Init()
	{
		agents = new List<Agent>();
		animalDataManager = new AnimalDataManager();
		for (int i = 0; i < 10; i++)
		{
			var agent = GetAgent();


			agents.Add(agent);
		}
	}
	public static void Main()
	{
		Init();
		startThread(agents);
		//ThreadBegin();

		while (true)
		{
			Console.WriteLine("Thread running...");
			while (thread.IsAlive)
			{
				//Console.WriteLine("IsAlive");

			}
			if (AgentTrainingField.Isfinished)
			{
				Console.WriteLine("Thread finished.");
				break;
			}
			else
			{
				Console.WriteLine("Thread Starting again...");
				ThreadStart();
			}

		}

	}

	public static Agent GetAgent()
	{
		var agent = new Agent();
		//Three decisions
		var brain = agent.brain;
		for (int i = 0; i < 93323 * 8; i++)
		{
			brain.AddInput();
		}

		brain.AddDecision(); //cats
		brain.AddDecision(); //dogs

		for (int i = 0; i < 93323 * 8; i++)
		{
			brain.mutateAddNewConnection();
		}

		return agent;
	}

	public struct TestSheet
	{
		public int input;
		public int correctOutput;
	}

	static TestSheet test00 = new TestSheet() { input = 0, correctOutput = 1 };
	static TestSheet test01 = new TestSheet() { input = 1, correctOutput = 2 };
	static TestSheet test02 = new TestSheet() { input = 2, correctOutput = 0 };
	static AnimalDataManager animalDataManager;
	private static bool agentCourse(Agent agent)
	{
		for(int testingCount = 0; testingCount < 10; testingCount++)
		{
			AnimalData data = (rand.Next(0, 2) == 0) ? animalDataManager.GetRandomCat() : animalDataManager.GetRandomDog();
			agent.brain.StartThinkingNew();
			//Console.WriteLine(data.binary.Length);
			for (int i = 0; i < data.binary.Length; i++)
			{
				if (data.binary[i] == '1')
				{
					agent.brain.ActivateInputNode(i);
				}
				else
				{
					agent.brain.ActivateInputNode(i, false);
				}
			}
			var decision = agent.brain.Decide();

			bool solved = true;
			if (data.type == Animal.CAT && decision == 0) agent.Score++;
			else if (data.type == Animal.DOG && decision == 1) agent.Score++;
			else solved = false;
			
			if (!solved)
			{
				break;

			}

		}

		return true;

	}
}
