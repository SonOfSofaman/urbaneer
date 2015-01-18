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
					System.Text.RegularExpressions.Match match = commandMatcher.Parse(line);
					matched = match.Success;
					if (matched)
					{
						CommandResult result = commandMatcher.Execute(match);
						if (!result.Success)
						{
							Console.WriteLine(result.Message);
						}
						break;
					}
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
			const string PATTERN_NEW = "^new";

			List<CommandMatcher> result = new List<CommandMatcher>();
			result.Add(new CommandMatcher(PATTERN_EXIT, new CommandDelegate(simulator.Exit)));
			result.Add(new CommandMatcher(PATTERN_PAUSE, new CommandDelegate(simulator.Pause)));
			result.Add(new CommandMatcher(PATTERN_RESUME, new CommandDelegate(simulator.Resume)));
			result.Add(new CommandMatcher(PATTERN_NEW, new CommandDelegate(simulator.New)));

			return result;
		}
	}
}
