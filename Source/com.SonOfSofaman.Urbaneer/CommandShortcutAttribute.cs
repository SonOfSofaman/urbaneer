using System;

namespace com.SonOfSofaman.Urbaneer
{
	[System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false)]
	public class CommandShortcutAttribute : Attribute
	{
		public string Shortcut { get; private set; }

		public CommandShortcutAttribute(string shortcut)
		{
			this.Shortcut = shortcut;
		}
	}
}
