using System.Text.RegularExpressions;

namespace com.SonOfSofaman.Urbaneer.CLI
{
	class CommandMatcher
	{
		private string Pattern;
		private Regex Regex;
		public CommandDelegate CommandDelegate { get; private set; }

		public CommandMatcher(string pattern, CommandDelegate commandDelegate)
		{
			this.Pattern = pattern;
			this.Regex = new Regex(pattern, RegexOptions.IgnoreCase);
			this.CommandDelegate = commandDelegate;
		}

		public Match Parse(string line)
		{
			return this.Regex.Match(line);
		}

		public CommandResult Execute(Match match)
		{
			CommandResult commandResult = null;
			if (this.CommandDelegate != null)
			{
				commandResult = this.CommandDelegate(match);
			}
			return commandResult;
		}
	}
}
