using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class MainProgram
{

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
		for (int i = 0; i < 10; i++)
		{
			var agent = new Agent();
			//Three decisions
			var brain = agent.brain;
			brain.AddInput();
			brain.AddInput();
			brain.AddInput();

			brain.AddDecision();
			brain.AddDecision();
			brain.AddDecision();


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

	public struct TestSheet
	{
		public int input;
		public int correctOutput;
	}

	static TestSheet test00 = new TestSheet() { input = 0, correctOutput = 1 };
	static TestSheet test01 = new TestSheet() { input = 1, correctOutput = 2 };
	static TestSheet test02 = new TestSheet() { input = 2, correctOutput = 0 };
	private static bool agentCourse(Agent agent)
	{
		List<TestSheet> tests = new List<TestSheet>() { test00, test01, test02 };
		foreach (var test in tests)
		{
			agent.brain.StartThinking();

			if (!agent.brain.ActivateInputNode(test.input)) return false;
			var decision = agent.brain.Decide();
			//agent.brain.Print();
			if (decision == test.correctOutput)
				agent.Score++;
		}
		return true;



	}
}
