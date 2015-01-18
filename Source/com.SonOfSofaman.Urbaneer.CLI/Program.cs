using System;
using System.Collections.Generic;

namespace com.SonOfSofaman.Urbaneer.CLI
{
	class Program
	{
		private static string Prompt = "]";

		static int Main(string[] args)
		{
			Console.Clear();
			Console.SetWindowSize(130, 80);
			Console.BufferWidth = 130;

			Simulator simulator = new Simulator();
			List<CommandMatcher> commandMatchers = GetCommands(simulator);

			do
			{
				Console.Write(Prompt);
				string line = Console.ReadLine();

				bool matched = false;
				foreach (CommandMatcher commandMatcher in commandMatchers)
				{
					matched = commandMatcher.ParseAndExecute(line);
					if (matched) break;
				}
				if (!matched)
				{
					Console.WriteLine("unknown command");
				}

				Console.WriteLine();
			} while (simulator.IsAcceptingCommands);

			return 0;
		}

		private static List<CommandMatcher> GetCommands(Simulator simulator)
		{
			const string PATTERN_EXIT = "^exit$";

			List<CommandMatcher> result = new List<CommandMatcher>();
			result.Add(new CommandMatcher(PATTERN_EXIT, (match) => { simulator.Exit(); }));

			return result;
		}
	}
}
