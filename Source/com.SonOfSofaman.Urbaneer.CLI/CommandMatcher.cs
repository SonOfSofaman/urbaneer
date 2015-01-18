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

		public bool ParseAndExecute(string line)
		{
			Match match = this.Regex.Match(line);
			if (match.Success)
			{
				if (this.CommandDelegate != null) this.CommandDelegate(match);
			}
			return match.Success;
		}
	}
}
