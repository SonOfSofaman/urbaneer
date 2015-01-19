using System;
using System.Collections.Generic;

namespace com.SonOfSofaman.Urbaneer.CLI
{
	class Program
	{
		private static Simulator Simulator = new Simulator();

		static int Main(string[] args)
		{
			Console.Clear();
			Console.SetWindowSize(130, 80);
			Console.BufferWidth = 130;

			List<CommandMatcher> commandMatchers = GetCommands(Simulator);

			do
			{
				Console.Write("{0:0.00}{1}", Simulator.State == null ? 0.0 : Simulator.State.ElapsedSeconds, Simulator.IsPaused ? "]" : ">");
				string line = Console.ReadLine();
				if (!String.IsNullOrWhiteSpace(line))
				{
					bool matched = false;
					foreach (CommandMatcher commandMatcher in commandMatchers)
					{
						System.Text.RegularExpressions.Match match = commandMatcher.Parse(line);
						matched = match.Success;
						if (matched)
						{
							CommandResult result = commandMatcher.Execute(match);
							if (!String.IsNullOrWhiteSpace(result.Message))
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
				}
			} while (Simulator.IsAcceptingCommands);

			return 0;
		}

		private static List<CommandMatcher> GetCommands(Simulator simulator)
		{
			const string PATTERN_RANGE = "(?<x0>[0-9]+),(?<y0>[0-9]+):(?<x1>[0-9]+),(?<y1>[0-9]+)";
			const string PATTERN_SIZE = "(?<w>[1-9][0-9]*)x(?<h>[1-9][0-9]*)";
			const string PATTERN_POSITION = "(?<x>-?[0-9]+),(?<y>-?[0-9]+)";

			const string PATTERN_EXIT = "^exit$";
			const string PATTERN_PAUSE = "^pause";
			const string PATTERN_RESUME = "^resume$";
			const string PATTERN_NEW = "^new";
			const string PATTERN_ZONE = "^zone (?<zonetype>[rci])(?<zonedensity>[012]) " + PATTERN_SIZE + " at " + PATTERN_POSITION + "$";
			const string PATTERN_MAP = "^map " + PATTERN_POSITION + "$";

			List<CommandMatcher> result = new List<CommandMatcher>();
			result.Add(new CommandMatcher(PATTERN_EXIT, new CommandDelegate(simulator.Exit)));
			result.Add(new CommandMatcher(PATTERN_PAUSE, new CommandDelegate(simulator.Pause)));
			result.Add(new CommandMatcher(PATTERN_RESUME, new CommandDelegate(simulator.Resume)));
			result.Add(new CommandMatcher(PATTERN_NEW, new CommandDelegate(simulator.New)));
			result.Add(new CommandMatcher(PATTERN_ZONE, new CommandDelegate(simulator.Zone)));
			result.Add(new CommandMatcher(PATTERN_MAP, new CommandDelegate(DrawMap)));

			return result;
		}

		private static CommandResult DrawMap(System.Text.RegularExpressions.Match match)
		{
			CommandResult result = new CommandResult();

			if (Program.Simulator.State == null)
			{
				result.Success = false;
				result.Message = "No simulation is available. Unable to map.";
			}
			else
			{
				int xcenter = Convert.ToInt32(match.Groups["x"].Value);
				int ycenter = Convert.ToInt32(match.Groups["y"].Value);
				for (int y = -32; y < 32; y++)
				{
					for (int x = -32; x < 32; x++)
					{
						Console.Write(" ");

						int xworld = xcenter + x;
						int yworld = ycenter + y;
						Zone zone = Program.Simulator.State.GetZoneAt(xworld, yworld);
						if (zone == null)
						{
							string icon = "∙";
							if (xworld % 10 == 0 && yworld % 10 == 0)
							{
								icon = "+";
							}
							Console.Write(icon);
						}
						else
						{
							switch (zone.ZoneType)
							{
								case ZoneType.Residential:
								{
									Console.Write("r");
									break;
								}
								case ZoneType.Commercial:
								{
									Console.Write("c");
									break;
								}
								case ZoneType.Industrial:
								{
									Console.Write("i");
									break;
								}
							}
						}
					}
					Console.WriteLine();
				}

				result.Success = true;
				result.Message = null;
			}

			return result;
		}
	}
}
