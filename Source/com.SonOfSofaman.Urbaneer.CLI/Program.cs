using System;
using System.Collections.Generic;

namespace com.SonOfSofaman.Urbaneer.CLI
{
	class Program
	{
		static int Main(string[] args)
		{
			Console.Clear();
			Console.SetWindowSize(130, 80);
			Console.BufferWidth = 130;

			Simulator simulator = new Simulator();
			List<CommandMatcher> commandMatchers = GetCommands(simulator);

			do
			{
				Console.Write("{0:0.00}{1}", simulator.State == null ? 0.0 : simulator.State.ElapsedSeconds, simulator.IsPaused ? "]" : ">");
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
			const string PATTERN_PAUSE = "^pause";
			const string PATTERN_RESUME = "^resume$";

			List<CommandMatcher> result = new List<CommandMatcher>();
			result.Add(new CommandMatcher(PATTERN_EXIT, (match) => { simulator.Exit(); }));
			result.Add(new CommandMatcher(PATTERN_PAUSE, (match) => { simulator.Pause(); }));
			result.Add(new CommandMatcher(PATTERN_RESUME, (match) => { simulator.Resume(); }));

			return result;
		}
	}
}
